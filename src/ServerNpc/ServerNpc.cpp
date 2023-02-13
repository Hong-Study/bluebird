#include "ServerNpc.h"

ServerNpc::~ServerNpc()
{
    m_Socket.close();
    delete packetManager; //���� malloc free��ų �ٸ� ��� ã�� �� ������ ���� ��
}

void ServerNpc::Connect(boost::asio::ip::tcp::endpoint& endpoint)
{
	m_Socket.async_connect(endpoint, boost::bind(&ServerNpc::handle_connect, this, boost::asio::placeholders::error)
	);
}

void ServerNpc::PostWrite(LoginData loginData)
{
    boost::mutex::scoped_lock(mutex);
    if (m_Socket.is_open() == false)
    {
        return;
    }

    m_nSeqNumber += 1;
    //outputBuf = const_cast<char*>((packetManager->MakeLoginPacket(loginData)).c_str());
    outputBuf = packetManager->MakeLoginPacket(loginData);
    bufSize = packetManager->GetBufSize();

    boost::asio::async_write(m_Socket, boost::asio::buffer(outputBuf, bufSize),
        boost::bind(&ServerNpc::handle_write, this,
            boost::asio::placeholders::error,
            boost::asio::placeholders::bytes_transferred)
    );
    
    PostReceive();
}

void ServerNpc::PostWrite(GameData gameData)
{
    boost::mutex::scoped_lock(mutex);
    if (m_Socket.is_open() == false)
    {
        return;
    }

    m_nSeqNumber += 1;
    //outputBuf = const_cast<char*>((packetManager->MakeGamePacket(gameData)).c_str());
    outputBuf = packetManager->MakeGamePacket(gameData);

    bufSize = packetManager->GetBufSize();

    boost::asio::async_write(m_Socket, boost::asio::buffer(outputBuf, bufSize),
        boost::bind(&ServerNpc::handle_write, this,
            boost::asio::placeholders::error,
            boost::asio::placeholders::bytes_transferred)
    );

}

list<StartData>* ServerNpc::GetStartData()
{
    return &roomGroup;
}

void ServerNpc::PostReceive()
{
    memset(&m_ReceiveBuffer, '\0', sizeof(m_ReceiveBuffer));

    m_Socket.async_read_some(boost::asio::buffer(m_ReceiveBuffer),
        boost::bind(&ServerNpc::handle_receive, this,
            boost::asio::placeholders::error,
            boost::asio::placeholders::bytes_transferred)
    );
}

void ServerNpc::handle_connect(const boost::system::error_code& error)
{
    if (error)
    {
        cout << "connect failed : " << error.message() << endl;
    }
    else
    {
        cout << "connected" << endl;
        packetManager = new PacketManager;
        PostReceive();
    }
}

void ServerNpc::handle_write(const boost::system::error_code& error, size_t bytes_transferred)
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
        //free(outputBuf);
        //PostReceive();
    }
}

void ServerNpc::handle_receive(const boost::system::error_code& error, size_t bytes_transferred)
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
        //cout << "���� �����κ��� ��Ŷ�� ���Ź���" << endl;
        inputBuf = &m_ReceiveBuffer[0];
        LoginData loginData;
        StartData startData;
        protobuf::io::ArrayInputStream input_array_stream(inputBuf, bytes_transferred);
        protobuf::io::CodedInputStream input_coded_stream(&input_array_stream);
        int status = packetManager->PacketProcess(&loginData, &startData, input_coded_stream);
        if (status == 1)
        {
            for (int i = 0; i < loginData.obstacle.size(); i++)
            {
                boost::thread* tempThread = new boost::thread(ObstacleThread(loginData, i, *this));
                threadGroup.push_back(make_pair(loginData.matchRoom, tempThread));
            }
            
            PostWrite(loginData);
        }
        else if (status == 2)
        {
            cout << "StartData ���� | Room: " << startData.room << " | State: " << startData.game << endl;
            roomGroup.push_back(startData);
            PostReceive();
        }
        else if (status == 3)
        {
            ThreadInterrupt(loginData.matchRoom);
            cout << "NextStage ���� | Room: " << loginData.matchRoom << " �ʱ�ȭ ��.." << endl;
            for (int i = 0; i < loginData.obstacle.size(); i++)
            {
                boost::thread* tempThread = new boost::thread(ObstacleThread(loginData, i, *this));
                threadGroup.push_back(make_pair(loginData.matchRoom, tempThread));
            }

            PostWrite(loginData);
        }
        else if (status == 4)
        {
            cout << "EndGame ���� | Room: " << loginData.matchRoom << " ���� ��.." << endl;
            ThreadInterrupt(loginData.matchRoom);
            PostReceive();
        }
        else
        {
            PostReceive();
        }
    }
}

void ServerNpc::ThreadInterrupt(int matchRoom)
{
    for (iter = threadGroup.begin(); iter != threadGroup.end(); iter++)
    {
        //�ش� matchRoom ������ ��
        if (iter->first == matchRoom)
        {
            iter->second->interrupt();
            delete iter->second;
            threadGroup.erase(iter);
            //break;
        }
    }

    for (iterRoom = roomGroup.begin(); iterRoom != roomGroup.end(); iterRoom++)
    {
        if (iterRoom->room == matchRoom)
        {
            roomGroup.erase(iterRoom);
            break;
        }
    }
}
