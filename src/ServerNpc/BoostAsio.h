#pragma once
// EchoClient.cpp : �� ���Ͽ��� 'main' �Լ��� ���Ե˴ϴ�. �ű⼭ ���α׷� ������ ���۵ǰ� ����˴ϴ�.
//

#include <iostream>
#include <boost/asio.hpp>
#include <boost/bind.hpp>
#include <boost/thread.hpp>
#include <boost/thread/mutex.hpp>
#include "ConnectToSQL.h"
#include "PacketManager.h"
#include <algorithm>
#include <list>

using namespace std;

class ObstacleThread;

class BoostAsio
{
public:
    BoostAsio(boost::asio::io_context& io_service) :
        m_io_service(io_service), m_Socket(io_service), m_nSeqNumber(0) {}
    ~BoostAsio();

    void Connect(boost::asio::ip::tcp::endpoint& endpoint);
    void PostWrite(int header = 1);
private:
    

    void PostReceive();

    void handle_connect(const boost::system::error_code& error);
    void handle_write(const boost::system::error_code& error, size_t bytes_transferred);
    void handle_receive(const boost::system::error_code& error, size_t bytes_transferred);

    void Map01Thread();

    boost::asio::io_context& m_io_service;
    boost::asio::ip::tcp::socket m_Socket;
    int m_nSeqNumber;
    char* outputBuf;
    char* inputBuf;
    int bufSize;
    array<char, 128> m_ReceiveBuffer;
    array<float, 4> temp;
    string m_WriteMessage;

    ConnectToSQL* mysql;
    MYSQL_ROW res;

    PacketManager* packetManager;

    list<pair<int, boost::thread*>> threadGroup;
    list<pair<int, boost::thread*>>::iterator iter;

    boost::mutex mutex;
};

class ObstacleThread
{
public:

    ObstacleThread(LoginData loginData, BoostAsio& npcServer) : loginData(loginData), npcServer(npcServer)
    {
    }

    
    void operator()() const
    {
        while (true)
        {
            cout << "MapLevel: " << loginData.mapLevel << " / MatchRoom: " << loginData.matchRoom << endl;
            npcServer.PostWrite();
            boost::this_thread::sleep(boost::posix_time::seconds(1));
        }
    }
    

private:
    BoostAsio& npcServer;
    LoginData loginData;
    GameData gameData;

};
