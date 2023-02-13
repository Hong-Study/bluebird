#pragma once
#include <JobQueue.h>

class Room : public JobQueue
{
public:
	Room(int32 level, int32 room);
	~Room() { cout << "���� ���� " << _matchRoom << endl; }
	void MatchEnter(vector<PlayerRef> ref);
	void GameEnter(GameSessionRef ref, int64 id);
	void ObstacleEnter(Npc::LoginData pkt);

	void ReConnect(GameSessionRef ref, int64 id);
	void Disconnect(PlayerRef ref);
	void Leave(PlayerRef ref);
	int	 Start();

	void PlayerMove(Protocol::Move data);
	void ObstacleMove(int64 id, Npc::Vector3 position, Npc::Vector3 rotation, Protocol::Move data);
	void TimeSync();
	void Broadcast(SendBufferRef ref);

	// ���� �������� �ѱ��
	void PlayerGoal(Protocol::Player data);
	void NextStage();

	// �������� �� �� ����
	void RoomEnd();

	// ���߿� �ٲٱ�
	bool IsPlayer(int64 id);

public:
	atomic<bool>				_start = false;
	atomic<int32>				_stage = 0;

private:
	int32						_matchRoom;
	int32						_mapLevel;

	Protocol::Data				_startData;
	int32						_playerSize = 0;

	array<map<int64, PlayerRef>, 3>			_players;
	map<int64, ObtacleRef>					_obstacles;

	vector<pair<Npc::Vector3, Npc::Vector3>>	_spawnPosition;

	function<bool(int64 id)> CHECK;
};