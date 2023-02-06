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
		cout << buffer << endl;
		int32 dataSize = len - processLen;
		// �ּ��� ����� �Ľ��� �� �־�� �Ѵ�
		if (dataSize < 4)
			break;

		Match::Header header = *(reinterpret_cast<Match::Header*>(&buffer[processLen]));

		// ����� ��ϵ� ��Ŷ ũ�⸦ �Ľ��� �� �־�� �Ѵ�
		if ((dataSize - 4) < header.size())
			break;

		// ��Ŷ ���� ����
		OnRecvPacket(&buffer[processLen], header.size());

		processLen += (header.size() + 4);
	}

	return processLen;
}