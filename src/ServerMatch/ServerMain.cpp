#include "pch.h"
#include "MatchSession.h"
#include <CoreGlobal.h>
#include <ThreadManager.h>

enum
{
	WORKER_TICK = 64
};

void DoWorkerJob(ServerServiceRef& service)
{
	while (true)
	{
		LEndTickCount = ::GetTickCount64() + WORKER_TICK;

		//��Ʈ��ũ ����� ó�� -> �ΰ��� �������� (��Ŷ �ڵ鷯�� ���ؼ�)
		service->GetIocpCore()->Dispatch(10);

		ThreadManager::DoGlobalQueueWork();
	}
}

int main()
{
	ServerServiceRef service = MakeShared<ServerService>(
		NetAddress(L"127.0.0.1", 8000),
		MakeShared<IocpCore>(),
		MakeShared<MatchSession>, 10);

	ASSERT_CRASH(service->Start());

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