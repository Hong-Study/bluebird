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
    case Match::C_LOGIN:
        HandlerLogin(ref, ParsingPacket<Match::DATA>(buffer, (int32)head->size));
        break;
    case Match::C_CANCLE:
        HandlerCancle(ref, ParsingPacket<Match::DATA>(buffer, (int32)head->size));
        break;
    default:
        break;
    }
}

void PacketHandler::HandlerLogin(PacketSessionRef& ref, Match::DATA&& pkt)
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
    _ref->Send(MakeSendBuffer(pkt, Match::S_LOGIN));
}

void PacketHandler::HandlerCancle(PacketSessionRef& ref, Match::DATA&& pkt)
{
    MatchSessionRef _ref = static_pointer_cast<MatchSession>(ref);

    PlayerRef player = make_shared<Player>();
    player->ownerSession = _ref;
    player->playerId = pkt.id();
    player->mapLevel = pkt.maplevel();
    int32 index = pkt.maplevel();
    GMatch->DoAsync(&MatchManager::MatchLeave, player, index);
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::DATA pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::Users pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}
