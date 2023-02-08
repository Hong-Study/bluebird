#include "pch.h"
#include "MatchRoom.h"
#include "MatchSession.h"
#include "Player.h"

int32 MatchRoom::Enter(PlayerRef player)
{
	_players[player->playerId] = player;

	return static_cast<int32>(_players.size());
}

void MatchRoom::Leave(int64 id)
{
	//üũ �ʿ�
	_players.erase(id);
}

void MatchRoom::Broadcast(Match::S_Match& users, int32 matchRoom)
{
	users.set_usersize(static_cast<uint32>(_players.size()));
	for (auto& p : _players)
	{
		users.add_ids(p.second->playerId);
	}
}

