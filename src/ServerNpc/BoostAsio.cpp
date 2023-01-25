#include "BoostAsio.h"
//8001�� ����, 8000�� ������
BoostAsio::~BoostAsio()
{
    delete mysql;
    delete packetManager; //���� malloc free��ų �ٸ� ��� ã�� �� ������ ���� ��
}

void BoostAsio::Connect(boost::asio::ip::tcp::endpoint& endpoint)
{
	m_Socket.async_connect(endpoint, boost::bind(&BoostAsio::handle_connect, this, boost::asio::placeholders::error)
	);
}

void BoostAsio::PostWrite(int header)
{
    if (m_Socket.is_open() == false)
    {
        return;
    }

    /*
    if (m_nSeqNumber > 7)
    {
        m_Socket.close();
        return;
    }
    */
    

    m_nSeqNumber += 1;
    //char* outputBuf;
    if (header == 0)
    {
        outputBuf = packetManager->MakePacket(0);
        //temp[3] = atoi(res[3]);
    }
    else
    {
        outputBuf = packetManager->MakePacket(1);
    }
    
    bufSize = packetManager->GetBufSize();
    //string str = outputBuf;
    //char szMessage[128] = { 0, };
    //sprintf_s(szMessage, 128 - 1, "%d - Send Message", m_nSeqNumber);
    m_WriteMessage = outputBuf;

    boost::asio::async_write(m_Socket, boost::asio::buffer(outputBuf, bufSize),
        boost::bind(&BoostAsio::handle_write, this,
            boost::asio::placeholders::error,
            boost::asio::placeholders::bytes_transferred)
    );

}

void BoostAsio::PostReceive()
{
    memset(&m_ReceiveBuffer, '\0', sizeof(m_ReceiveBuffer));

    m_Socket.async_read_some(boost::asio::buffer(m_ReceiveBuffer),
        boost::bind(&BoostAsio::handle_receive, this,
            boost::asio::placeholders::error,
            boost::asio::placeholders::bytes_transferred)
    );
}

void BoostAsio::handle_connect(const boost::system::error_code& error)
{
    if (error)
    {
        cout << "connect failed : " << error.message() << endl;
    }
    else
    {
        cout << "connected" << endl;

        mysql = new ConnectToSQL();
        //res = mysql->SQLQuery("select * from objectinfo");
        //cout << res[0] << " / " << res[1] << " / " << res[2] << " / " << res[3] << endl;
        packetManager = new PacketManager;

        //PostWrite();
        PostReceive();
    }
}

void BoostAsio::handle_write(const boost::system::error_code& error, size_t bytes_transferred)
{
    if (error)
    {
        if (error == boost::asio::error::eof)
        {
            cout << "������ ������ ���������ϴ�." << endl;
        }
        else
        {
            cout << "error No: " << error.value() << " error Message: " << error.message() << endl;
        }
    }
    else
    {
        cout << m_nSeqNumber << " ���� �Ϸ�" << endl;
        PostReceive();
    }
}

//Thread Test Class
class ThreadTest
{
public:
    ThreadTest() {}
    void operator()()
    {
        while (true)
        {
            cout << "Thread " << boost::this_thread::get_id() << endl;
            boost::this_thread::sleep(boost::posix_time::seconds(1));
        }

    }
};
//

void BoostAsio::handle_receive(const boost::system::error_code& error, size_t bytes_transferred)
{
    if (error)
    {
        if (error == boost::asio::error::eof)
        {
            cout << "������ ������ ���������ϴ�." << endl;
        }
        else
        {
            cout << "error No: " << error.value() << " error Message: " << error.message() << endl;
        }
    }
    else
    {
        //outputBuf = &m_ReceiveBuffer[0];
        inputBuf = &m_ReceiveBuffer[0];
        protobuf::io::ArrayInputStream input_array_stream(inputBuf, bytes_transferred);
        protobuf::io::CodedInputStream input_coded_stream(&input_array_stream);
        int matchRoom = packetManager->PacketProcess(input_coded_stream);
        if (matchRoom != 0)
        {
            //boost::thread* tempThread = new boost::thread(ThreadTest());
            //threadGroup.push_back(make_pair(matchRoom, tempThread));
            PostWrite(0);
        }
        else
        {
            PostReceive();
        }
        //const string strRecvMessage = m_ReceiveBuffer.data();
        //cout << "�������� ���� �޽���: " << strRecvMessage << ", ���� ũ��: " << bytes_transferred << endl;
        //PostWrite();
    }
}

void BoostAsio::Map01Thread()
{
    try
    {
        while (true)
        {
            cout << "1" << endl;
            boost::this_thread::sleep(boost::posix_time::millisec(500));
        }
    }
    catch (boost::thread_interrupted&)
    {
        cout << "Map01Thread Interrupted" << endl;
    }
}

int main(int argc, char* argv[])
{
    boost::asio::io_context io_service;
    boost::asio::ip::tcp::endpoint endpoint(boost::asio::ip::address::from_string("127.0.0.1"), 8000);
    BoostAsio client(io_service);
    client.Connect(endpoint);
    io_service.run();
    cout << "��Ʈ��ũ ���� ����" << endl;
    getchar();
    
    return 0;
}