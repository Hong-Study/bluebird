#pragma once

#define WIN32_LEAN_AND_MEAN // ���� ������ �ʴ� ������ Windows ������� �����մϴ�.

#ifdef _DEBUG
#pragma comment(lib, "ServerCore\\Debug\\ServerCore.lib")
#pragma comment(lib, "Protobuf\\Debug\\libprotobufd.lib")
#else
#pragma comment(lib, "ServerCore\\Release\\ServerCore.lib")
#pragma comment(lib, "Protobuf\\Release\\libprotobufd.lib")
#endif

#include <pch.h>
#include <Service.h>

#include "ProtocolMatch.pb.h"
#include "ProtocolServer.pb.h"

#include "PacketSession.h"
#define THREAD_SIZE 10

using PacketSessionRef = shared_ptr<class PacketSession>;
using MatchSessionRef = shared_ptr<class MatchSession>;
using PlayerRef = shared_ptr<class Player>;
using MatchRoomRef = shared_ptr<class MatchRoom>;
using GameSessionRef = shared_ptr<class GameSession>;

#include "PacketHandler.h"