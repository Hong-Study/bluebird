#include "pch.h"
#include "PacketSession.h"

PacketSession::PacketSession()
{

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
		if (dataSize < sizeof(Match::Header))
			break;

		Match::Header header = *(reinterpret_cast<Match::Header*>(&buffer[processLen]));

		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - sizeof(Match::Header)) < header.size())
			break;

		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], header.size());

		processLen += (header.size() + sizeof(Match::Header));
	}

	return processLen;
}