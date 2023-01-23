#include "pch.h"
#include <CoreGlobal.h>
#include <Service.h>
#include <ThreadManager.h>
#include "MatchSession.h"
#include "PacketSession.h"

enum
{
	WORKER_TICK = 64
};

void DoWorkerJob(ClientServiceRef& service)
{
	while (true)
	{
		LEndTickCount = ::GetTickCount64() + WORKER_TICK;

		//��Ʈ��ũ ����� ó�� -> �ΰ��� �������� (��Ŷ �ڵ鷯�� ���ؼ�)
		service->GetIocpCore()->Dispatch(10);

		ThreadManager::DoGlobalQueueWork();
	}

}
int main() {
	ClientServiceRef service = MakeShared<ClientService>(
		NetAddress(L"127.0.0.1", 6000),
		MakeShared<IocpCore>(),
		MakeShared<MatchSession>, 20);

	ASSERT_CRASH(service->Start());

	if (!service->Start())
		ASSERT_CRASH("Start Error");

	for (int i = 0; i < THREAD_SIZE; i++) {
		GThreadManager->Launch([&service]()
			{
				while (true)
				{
					DoWorkerJob(service);
				}
			});
	}

	DoWorkerJob(service);

	GThreadManager->Join();
}