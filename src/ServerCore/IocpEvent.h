#pragma once

class Session;

enum class EventType : uint8 {
	Connect
	,Disconnect
	,Accept
	,Recv
	,Send
};

// �⺻ IocpEvent ��ü
class IocpEvent : public OVERLAPPED
{
public:
	IocpEvent(EventType type);

	void Init();
	EventType GetType() { return _type; }

public:
	EventType _type;
	IocpObjectRef owner;
};

// ���ӽ� IocpEvent ��ü
class AcceptEvent : public IocpEvent
{
public:
	AcceptEvent() : IocpEvent(EventType::Accept) { }

public:
	SessionRef	_session = nullptr;
};


class DisconnectEvent : public IocpEvent
{
public:
	DisconnectEvent() : IocpEvent(EventType::Disconnect) { }
};

class ConnectEvent : public IocpEvent 
{
public:
	ConnectEvent() : IocpEvent(EventType::Connect) { }
};
class RecvEvent : public IocpEvent
{
public:
	RecvEvent() : IocpEvent(EventType::Recv) { }
};

class SendEvent : public IocpEvent
{
public:
	SendEvent() : IocpEvent(EventType::Send) { }

public:
	vector<SendBufferRef> send_buffers;
};