#include "pch.h"
#include "PacketHandler.h"
#include "GameSession.h"
#include "Players.h"

void PacketHandler::HandlerPacket(PacketSessionRef& ref, BYTE* buffer, int32 len)
{
    PacketHeader* head = reinterpret_cast<PacketHeader*>(buffer);
    switch (head->type)
    {
    case Match::S_MATCH:
        HandlerMatch(ref, ParsingPacket<Match::Users>(buffer, (int32)head->size));
        break;
    default:
        break;
    }
}

void PacketHandler::HandlerMatch(PacketSessionRef& ref, Match::Users&& pkt)
{
    GameSessionRef _ref = static_pointer_cast<GameSession>(ref);

    cout << "Input" << endl;
    // TODO ����üũ : �� ����� ����� ��ġ����ŷ �Ǿ��ִ��� Ȯ���� �ʿ� ����
    // Redis�� �Ǻ��ص� ������ ��
    vector<PlayerRef> players;

    for (int i = 0; i < pkt.usersize(); i++) {
        auto data = pkt.users(i);
        players.emplace_back(make_shared<Player>(data.id(), data.maplevel(), _ref));
        cout << data.id() << " " << data.maplevel() << endl;
    }
    GPlayers->DoAsync(&Players::Enter, &players);
}

SendBufferRef PacketHandler::MakeSendBuffer(Match::DATA pkt, Match::STATE type)
{
    return _MakeSendBuffer(pkt, type);
}
