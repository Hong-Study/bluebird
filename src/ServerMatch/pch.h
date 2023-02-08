 #pragma once

#define WIN32_LEAN_AND_MEAN // ���� ������ �ʴ� ������ Windows ������� �����մϴ�.

#ifdef _DEBUG
#pragma comment(lib, "ServerCore\\Debug\\ServerCore.lib")
#pragma comment(lib, "Protobuf\\Debug\\libprotobufd.lib")
#pragma comment(lib, "hiredis\\Debug\\hiredisd.lib")
#else
#pragma comment(lib, "ServerCore\\Release\\ServerCore.lib")
#pragma comment(lib, "Protobuf\\Release\\libprotobufd.lib")
#pragma comment(lib, "hiredis\\Release\\hiredis.lib")
#endif

#include <CorePch.h>
#include <Service.h>

#include "ProtocolMatch.pb.h"
#include "PacketSession.h"
#include "Constant.h"

using PacketSessionRef		= shared_ptr<class PacketSession>;
using MatchSessionRef		= shared_ptr<class MatchSession>;
using ConnectSessionRef		= shared_ptr<class ConnectSession>;
using PlayerRef				= shared_ptr<class Player>;
using MatchRoomRef			= shared_ptr<class MatchRoom>;

#include "PacketHandler.h"
#include "Redis.h"