#include "pch.h"
#include "PacketHandler.h"
#include "GameSession.h"
#include "MatchSession.h"
#include "Room.h"

void PacketHandler::HandlerPacket(PacketSessionRef& ref, BYTE* buffer, int32 len)
{
    PacketHeader* head = reinterpret_cast<PacketHeader*>(buffer);
    switch (head->type)
    {
    case Match::S_MATCH:
        HandlerMatch(ref, ParsingPacket<Match::Users>(buffer, (int32)head->size));
        break;
    case Protocol::CONNECT:
        HandlerConnect(ref, ParsingPacket<Protocol::Data>(buffer, (int32)head->size));
    case Protocol::MOVE:

    default:
        break;
    }
}

void PacketHandler::HandlerMatch(PacketSessionRef& ref, Match::Users&& pkt)
{
    cout << "Input" << endl;
    // TODO ����üũ : �� ����� ����� ��ġ����ŷ �Ǿ��ִ��� Ȯ���� �ʿ� ����
    // Redis�� �Ǻ��ص� ������ ��
    vector<PlayerRef> players;

    for (int i = 0; i < pkt.usersize(); i++) {
        auto data = pkt.ids(i);
        players.emplace_back(make_shared<Player>(data, pkt.level(), Vector3{ 0.0f, 0.0f, 0.0f }));
        cout << data << " " << pkt.level() << endl;
    }
    GRoom->DoAsync(&Room::MatchEnter, &players);
}

void PacketHandler::HandlerConnect(PacketSessionRef& ref, Protocol::Data&& pkt)
{
    PlayerRef player = make_shared<Player>(pkt.id(), pkt.maplevel());
    cout << "Player Inside = " << pkt.id() << " " << pkt.maplevel() << " " << pkt.matchroom();
    GRoom->DoAsync(&Room::GameEnter, static_pointer_cast<GameSession>(ref), player);
}

void PacketHandler::HandlerMove(PacketSessionRef& ref, Protocol::Data&& pkt)
{
    Protocol::Player point = pkt.player(0);

    cout << "whitch : " << point.x() << " " << point.y() << " " << point.z();
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::Data pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}
