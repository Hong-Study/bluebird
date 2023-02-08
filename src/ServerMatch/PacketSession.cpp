#include "pch.h"
#include "PacketSession.h"

PacketSession::PacketSession()
{
	str.resize(4);
}

PacketSession::~PacketSession()
{

}
int32 PacketSession::OnRecv(BYTE* buffer, int32 len)
{
	int32 processLen = 0;

	while (true)
	{
		int32 dataSize = len - processLen;
		// �ּ��� ����� �Ľ��� �� �־�� �Ѵ�
		if (dataSize < 4)
			break;

		memcpy(str.data(), buffer, 4);
		Match::Header header;
		header.ParseFromString(str);
		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - 4) < header.size())
			break;
		
		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], std::move(header));

		processLen += (header.size() + 4);
	}

	return processLen;
}

Sessions::Sessions()
{

}

Sessions::~Sessions()
{

}

int32 Sessions::OnRecv(BYTE* buffer, int32 len)
{
	int32 processLen = 0;

	while (true)
	{
		int32 dataSize = len - processLen;
		// �ּ��� ����� �Ľ��� �� �־�� �Ѵ�
		if (dataSize < sizeof(MatchHeader))
			break;

		MatchHeader header = *(reinterpret_cast<MatchHeader*>(&buffer[processLen]));

		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - sizeof(MatchHeader)) < header.size)
			break;

		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], header.size);

		processLen += (header.size + sizeof(MatchHeader));
	}

	return processLen;
}