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
		if (dataSize < HEAD_SIZE)
			break;

		GameHeader header = *(reinterpret_cast<GameHeader*>(&buffer[processLen]));

		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - HEAD_SIZE) < header.size)
			break;

		int32 total = HEAD_SIZE + header.size;

		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], total);

		processLen += total;
	}

	return processLen;
}