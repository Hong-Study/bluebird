#include "pch.h"
#include "PacketSession.h"

int32 PacketSession::OnRecv(BYTE* buffer, int32 len)
{
	int32 processLen = 0;

	while (true)
	{
		int32 dataSize = len - processLen;
		// �ּ��� ����� �Ľ��� �� �־�� �Ѵ�
		if (dataSize < sizeof(Pkt_Header))
			break;

		Pkt_Header header = *(reinterpret_cast<Pkt_Header*>(&buffer[processLen]));

		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - sizeof(Pkt_Header)) < header.size)
			break;

		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], header.size);

		processLen += (header.size + sizeof(Pkt_Header));
	}

	return processLen;
}
