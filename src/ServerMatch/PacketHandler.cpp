#include "pch.h"
#include "PacketHandler.h"
#include "MatchManager.h"
#include "MatchSession.h"
#include "Player.h"

void PacketHandler::HandlerPacket(PacketSessionRef& ref, BYTE* buffer, int32 len)
{
    PacketHeader* head = reinterpret_cast<PacketHeader*>(buffer);
    switch (head->type)
    {
    case Protocol::C_LOGIN:
        HandlerLogin(ref, ParsingPacket<Protocol::DATA>(buffer, (int32)head->size));
        break;
    case Protocol::C_CANCLE:
        HandlerCancle(ref, ParsingPacket<Protocol::DATA>(buffer, (int32)head->size));
        break;
    default:
        break;
    }
}

void PacketHandler::HandlerLogin(PacketSessionRef& ref, Protocol::DATA&& pkt)
{
    MatchSessionRef _ref = static_pointer_cast<MatchSession>(ref);

    // TODO ����üũ : �� ����� ����� ��ġ����ŷ �Ǿ��ִ��� Ȯ���� �ʿ� ����
    // Redis�� �Ǻ��ص� ������ ��
    PlayerRef player = make_shared<Player>();
    player->ownerSession = _ref;
    player->playerId = pkt.id();
    player->mapLevel = pkt.maplevel();
    int32 index = pkt.maplevel();

    GMatch->DoAsync(&MatchManager::MatchEnter, player, index);

    pkt.set_state(true);
    _ref->Send(MakeSendBuffer(pkt, Protocol::S_LOGIN));
}

void PacketHandler::HandlerCancle(PacketSessionRef& ref, Protocol::DATA&& pkt)
{
    MatchSessionRef _ref = static_pointer_cast<MatchSession>(ref);

    PlayerRef player = make_shared<Player>();
    player->ownerSession = _ref;
    player->playerId = pkt.id();
    player->mapLevel = pkt.maplevel();
    int32 index = pkt.maplevel();
    GMatch->DoAsync(&MatchManager::MatchLeave, player, index);
}

SendBufferRef PacketHandler::MakeSendBuffer(Protocol::DATA pkt, Protocol::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}
