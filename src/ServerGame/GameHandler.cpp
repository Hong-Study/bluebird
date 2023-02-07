#include "pch.h"
#include "Gamehandler.h"
#include "GameSession.h"
#include "MatchSession.h"
#include "Games.h"
#include "GameObject.h"

void GameHandler::HandlerPacket(GameSessionRef ref, BYTE* buffer, int32 len)
{
    GameHeader* head = reinterpret_cast<GameHeader*>(buffer);
    switch (head->type)
    {
    case Protocol::CONNECT:
        HConnect(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::PLAYER_MOVE:
        HPlayerMove(ref, ParsingPacket<Protocol::Move, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::NO_MOVE:
        HNoMove(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::GAME_COMPLTE:
        HGameComplete(ref, ParsingPacket<Protocol::Player, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::GAME_DROP:
        HGameDrop(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::GAME_FAIL:
        HGameFail(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::PLAYER_CRASH:
        HPlayerCrash(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::OBSTACLE_CRASH:
        HObstacleCrash(ref, ParsingPacket<Protocol::Data, GameHeader>(buffer, (int32)head->size));
        break;
    case Protocol::TIME:
        HTime(ref, ParsingPacket<Protocol::Times, GameHeader>(buffer, (int32)head->size));
    default:
        break;
    }
}

void GameHandler::HConnect(GameSessionRef& ref, Protocol::Data&& pkt)
{
    cout << "Player Inside = " << pkt.id() << " " << pkt.maplevel() << " " << pkt.matchroom() << endl;
    if ((ref->_mySelf == nullptr) && ref->_room.expired())
        Ggames->DoAsync(&Games::EnterGame, ref, pkt.id(), pkt.matchroom());
    //���� �� ��ü ���� ���� ���� -> ��� �̽�
}

void GameHandler::HPlayerMove(GameSessionRef& ref, Protocol::Move&& pkt)
{
    if (ref->_mySelf != nullptr) {
        if (auto room = ref->_room.lock()) {
            if (room->_start)
                room->DoAsync(&Room::PlayerMove, pkt);
        }
    }
}

void GameHandler::HNoMove(GameSessionRef& ref, Protocol::Data&& pkt)
{

}

void GameHandler::HGameComplete(GameSessionRef& ref, Protocol::Player&& pkt)
{
    if (ref->_mySelf != nullptr) {
        if (auto room = ref->_room.lock()) {
            if (room->_start && pkt.id() == pkt.id())
                room->DoAsync(&Room::PlayerGoal, std::move(pkt));
        }
    }
}

void GameHandler::HGameFail(GameSessionRef& ref, Protocol::Data&& pkt)
{
    
}

void GameHandler::HGameDrop(GameSessionRef& ref, Protocol::Data&& pkt)
{
    
}

void GameHandler::HPlayerCrash(GameSessionRef& ref, Protocol::Data&& pkt)
{

}

void GameHandler::HObstacleCrash(GameSessionRef& ref, Protocol::Data&& pkt)
{
    if (auto room = ref->_room.lock())
    {
        
    }
}

void GameHandler::HTime(GameSessionRef& ref, Protocol::Times&& pkt)
{
    ref->Send(GameHandler::MakeSendBuffer(pkt, Protocol::TIME));
}

SendBufferRef GameHandler::MakeSendBuffer(Protocol::Data pkt, Protocol::INGAME type)
{
    return _MakeSendBuffer<Protocol::Data, GameHeader, Protocol::INGAME>(pkt, type);
}
SendBufferRef GameHandler::MakeSendBuffer(Protocol::Move pkt, Protocol::INGAME type)
{
    return _MakeSendBuffer<Protocol::Move, GameHeader, Protocol::INGAME>(pkt, type);
} 

SendBufferRef GameHandler::MakeSendBuffer(Protocol::Player pkt, Protocol::INGAME type)
{
    return _MakeSendBuffer<Protocol::Player, GameHeader, Protocol::INGAME>(pkt, type);
}

SendBufferRef GameHandler::MakeSendBuffer(Protocol::Times pkt, Protocol::INGAME type)
{
    return _MakeSendBuffer<Protocol::Times, GameHeader, Protocol::INGAME>(pkt, type);
}

SendBufferRef GameHandler::MakeSendBuffer(Protocol::GameCompleteData pkt, Protocol::INGAME type)
{
    return _MakeSendBuffer<Protocol::GameCompleteData, GameHeader, Protocol::INGAME>(pkt, type);
}
