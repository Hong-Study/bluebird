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
        HandlerLogin(ref, ParsingPacket<Match::Data>(buffer, (int32)head->size));
        break;
    case Match::C_CANCLE:
        HandlerCancle(ref, ParsingPacket<Match::Data>(buffer, (int32)head->size));
        break;
    default:
        break;
    }
}

void PacketHandler::HandlerLogin(PacketSessionRef& ref, Match::Data&& pkt)
{
    MatchSessionRef _ref = static_pointer_cast<MatchSession>(ref);

    // TODO ����üũ : �� ����� ����� ��ġ����ŷ �Ǿ��ִ��� Ȯ���� �ʿ� ����
    // Redis�� �Ǻ��ص� ������ ��
    PlayerRef player = make_shared<Player>();
    player->ownerSession = _ref;
    player->playerId = pkt.id();
    player->mapLevel = pkt.level();

    GMatch->DoAsync(&MatchManager::MatchEnter, _ref, std::move(pkt), player, pkt.level());
}

void PacketHandler::HandlerCancle(PacketSessionRef& ref, Match::Data&& pkt)
{
    MatchSessionRef _ref = static_pointer_cast<MatchSession>(ref);

    GMatch->DoAsync(&MatchManager::MatchLeave, pkt.id(), pkt.level(), pkt.room());
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::Data pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::Users pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::Success pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}