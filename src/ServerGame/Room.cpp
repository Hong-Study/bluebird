#include "pch.h"
#include "Room.h"
#include "GameObject.h"
#include "GameSession.h"
#include "PacketSession.h"

Room::Room(int32 level, int32 room) : _mapLevel(level), _matchRoom(room)
{
	for (int i = 1; i <= 15; i++)
	{
		_spawnPosition.push_back(Vector3{ (float)i, 1, (float)i });
	}
	_startData.set_matchroom(room);
	_startData.set_maplevel(level);
}

// �� ����
void Room::MatchEnter(vector<PlayerRef> ref)
{
	for (auto _ref : ref) {
		int64 id = _ref->GetId();
		_players[id] = _ref;
		_players[id]->SetPosition(Vector3{ 0.1f, 0.2f, 29.f });
	}
}

// ���� �� ���� ����
void Room::GameEnter(GameSessionRef ref, int64 id)
{
	//Ȯ�� �۾� �ʿ�
	auto player = _startData.add_player();
	
	if (TEST) {
		PlayerRef player = make_shared<Player>(id, _matchRoom, Vector3{ 0.1f, 0.2f, 29.f });
		player->SetOwner(ref);

		_players[id] = player;
		ref->_mySelf = player;
		_playerSize += 1;
	}
	else if (_players.find(id) != _players.end()) {
		_players[id]->SetOwner(ref);
		ref->_mySelf = _players[id];
		_playerSize += 1;
	}

	// ���ο��� �� ����
	_players[id]->SetPlayer(player);

	{
		Protocol::Player data;
		data.set_id(id);
		GameUtils::SetVector3(data.mutable_position(), _players[id]->GetPosition());
		GameUtils::SetVector3(data.mutable_rotation(), _players[id]->GetRotation());

		ref->Send(GameHandler::MakeSendBuffer(data, Protocol::CONNECT));
	}
}

void Room::ObstacleEnter(map<int64, ObtacleRef>* obtacles)
{
	_obstacles = *obtacles;

	//��ü �÷��̾�� ���� ���� �ʿ�
	for(auto& obta : _obstacles){
		auto ob = _startData.add_obtacle();
		obta.second->SetObstacle(ob);
		ob->set_direction(obta.second->GetDirection());
	}
}

void Room::ReConnect(GameSessionRef ref, int64 id)
{
	// ���� ��ü ����
	// ���߿� ��ġ��
	cout << "ReConnect" << endl;
	_players[id]->SetOwner(ref);
	ref->_mySelf = _players[id];
	ref->_start = true;
	for (int i = 0; i < _startData.player_size(); i++) {
		auto player = _startData.player(i);
		cout << player.position().x() << " " << player.position().y() << " " << player.position().z();
	}
	ref->Send(GameHandler::MakeSendBuffer(_startData, Protocol::RECONNECT));
}

void Room::Disconnect(PlayerRef ref)
{
	cout << "Disconncet" << endl;
	_players[ref->GetId()]->SetOwner(nullptr);
}

void Room::Leave(PlayerRef ref)
{
	_players[ref->GetId()]->SetOwner(nullptr);
	_players.erase(ref->GetId());

	Protocol::Data data;
	data.set_id(ref->GetId());
	data.set_matchroom(_matchRoom);
	data.set_maplevel(_mapLevel);

	_playerSize.fetch_sub(1);
	Broadcast(GameHandler::MakeSendBuffer(data, Protocol::LEAVE));
}


int Room::Start()
{
	/*if (_playerSize < START_COUNT || _startData.obtacle_size() == 0) {
		return -1;
	}*/
	//�׽�Ʈ �ڵ�
	if (_playerSize < START_COUNT) {
		return -1;
	}
	vector<int64> keys;
	for (auto& player : _players)
	{
		if (player.second->GetOwner() == nullptr) {
			keys.push_back(player.first);
			continue;
		}
		else {
			player.second->GetOwner()->_start = true;
		}
	}
	for (auto key : keys)
	{
		_players.erase(key);
	}
	Broadcast(GameHandler::MakeSendBuffer(_startData, Protocol::START));

	_start.store(true);
	TimeSync();

	return _players.size();
}

void Room::PlayerMove(Protocol::Move data)
{
	//������ ����
	//�ùķ��̼� �����ؾߵ�.
	auto point = data.position();

	cout << "Move(" << data.id() << ") : " << point.x() << " " << point.y() << " " << point.z() << endl;

	PlayerRef player = _players[data.id()];

	player->Move(data.position(), data.rotation());
	
	Broadcast(GameHandler::MakeSendBuffer(data, Protocol::PLAYER_MOVE));
}

void Room::ObstacleMove(int64 id, Npc::Vector3 position, Npc::Vector3 rotation, Protocol::Move data)
{
	if (_obstacles.find(id) != _obstacles.end()) {
		_obstacles[id]->Move(position, rotation);
		cout << "Object �̵�" << endl;
		GameUtils::SetVector3(data.mutable_position(), _obstacles[id]->GetPosition());
		GameUtils::SetVector3(data.mutable_rotation(), _obstacles[id]->GetRotation());
	}
	
	Broadcast(GameHandler::MakeSendBuffer(data, Protocol::OBSTACLE_MOVE));
}

void Room::PlayerGoal(Protocol::Player data)
{
	cout << "Input goal" << endl;
	_winnerId.push_back(data.id());
	//TODO Ȯ�� �۾� �ʿ�
	//if (_winner.fetch_add(1) == WINNER1(_playerSize))
	_winner.fetch_add(1);
	if (_winner == GOAL_COUNT)
	{
		_start.store(false);
		//TODO �ѱ�� �۾� �ʿ�
		GameEnd();
	}
	else {
		Protocol::GameCompleteData player;
		player.set_id(data.id());
		player.set_success(true);
		Broadcast(GameHandler::MakeSendBuffer(player, Protocol::PLAYER_GOAL));
	}
}

void Room::TimeSync()
{
	Protocol::Times time;
	time.set_time(GetTickCount64());
	cout << time.time() << endl;

	Broadcast(GameHandler::MakeSendBuffer(time, Protocol::GET_TICK));

	DoTimer(60000, &Room::TimeSync);
}

void Room::Broadcast(SendBufferRef ref)
{
	for (auto& _ref : _players) {
		if(_ref.second->GetOwner() != nullptr)
			_ref.second->GetOwner()->Send(ref);
	}
}

void Room::GameEnd()
{
	Protocol::GameCompleteData data;
	for (auto& _ref : _players) {
		data.set_id(_ref.first);
		if (find(_winnerId.begin(), _winnerId.end(), _ref.first) != _winnerId.end())
			data.set_success(true);
		else
			data.set_success(false);
		_ref.second->GetOwner()->Send(GameHandler::MakeSendBuffer(data, Protocol::GAME_COMPLTE));

		_ref.second->GetOwner()->_mySelf = nullptr;
		_ref.second->SetOwner(nullptr);
	}

	//TODO �Ѱ��ֱ�

	_players.clear();
}

bool Room::IsPlayer(int64 id)
{
	if (_players.find(id) != _players.end()) 
	{
		if (_players[id]->GetOwner() == nullptr) 
		{
			return true;
		}
	}
	return false;
}
