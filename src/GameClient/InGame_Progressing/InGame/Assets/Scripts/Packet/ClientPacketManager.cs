using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
    //region�� ��� ������ ��Ÿ����.
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

    //Dictionary�� Key�� Value ��Ʈ�� �����迭, Key�� ����� Value ���� ���� �� �ִ�.
    //ushort ��ȣ�� ���� short
    //Delegate(��������Ʈ,�븮��) : �Լ��� ������ ��� ������ ����ϴ°��� �븮���̴�. +=, -= �� ���� �޼ҵ带 �߰� �Ǵ� �� �� �ִ�. ��������Ʈ�� �ϳ� �����ϸ� �������� �޼ҵ尡 ����
    //�� �� �ִ�.
    //Action �븮�ڴ� ���� ���� �������� ����� �����ϴ�. <> �ȿ� �Ű������� �ִ´�.
    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		
	public void Register()
	{		
        //Add�� ���� Dictionary�� ��� �߰�
		_onRecv.Add((ushort)INGAME.Connect, MakePacket<Data>);
		_handler.Add((ushort)INGAME.Connect, PacketHandler.S_ChatHandler);		
	
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);
		Action<PacketSession, IMessage> action = null;

        //������ Ű�� ����� ���� �����´�.
        //���� �����忡�� ������ �븮�ڸ� �����Ѵ�.
        if (_handler.TryGetValue(id, out action))
			action.Invoke(session, pkt);
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}