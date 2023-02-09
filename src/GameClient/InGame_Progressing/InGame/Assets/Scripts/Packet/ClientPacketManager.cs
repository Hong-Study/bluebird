using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Google.Protobuf;
using Google.Protobuf.Protocol;

/*
�������̶� �� ��ü�� �޸𸮿����� ǥ������� ���� �Ǵ� ���ۿ� ������ �ٸ� ������ �������� ��ȯ�ϴ� �����̴�.
����ȭ�� �����ϴ�. ������ ����� �ܼ�ȭ�Ͽ� ���� �����͸� �ְ� �ް� �ϱ� ����.

C#���� �ۼ��Ǵ� �ڵ�� ManagedCode, �������� C/C++ �����Ϸ��� ���� �����ϵǴ� �ڵ�� UnManagedCode
Managed Code�� GC(Garbage Collector)�� ���� ������ �Ǵ� �޸� �ݴ�� ���α׷� �ڵ峪 �ü���� ���� ������ �ȴ�.

*/

class PacketManager
{
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }

    Dictionary<ushort, Action<byte[], uint, ushort>> recv = new Dictionary<ushort, Action<byte[], uint, ushort>>();
    Dictionary<ushort, Action<IMessage>> handler = new Dictionary<ushort, Action<IMessage>>();

    int time = (ushort)INGAME.Time;

    //Action<ushort, IMessage> customHandler = (ushort id, IMessage message) => { PacketQueue.Instance.Push(id, message); };
    public Action<ushort, IMessage> customHandler { get; set; }

    PacketManager()
    {
        recv.Add((ushort)INGAME.Time, MakePacket<Times>);
        recv.Add((ushort)INGAME.Start, MakePacket<Data>);
        handler.Add((ushort)INGAME.Start, PacketHandler.GameStart);
        recv.Add((ushort)INGAME.PlayerMove, MakePacket<Move>);
        handler.Add((ushort)INGAME.PlayerMove, PacketHandler.PlayerMove);
        recv.Add((ushort)INGAME.Connect, MakePacket<Player>);
        handler.Add((ushort)INGAME.Connect, PacketHandler.GameConnect);
        recv.Add((ushort)INGAME.ObstacleMove, MakePacket<Move>);
        handler.Add((ushort)INGAME.ObstacleMove, PacketHandler.ObtacleMove);
        recv.Add((ushort)INGAME.GetTick, MakePacket<Times>);
        handler.Add((ushort)INGAME.GetTick, PacketHandler.TimeSync);
        recv.Add((ushort)INGAME.ConnectFail, MakePacket<Data>);
        handler.Add((ushort)INGAME.ConnectFail, PacketHandler.CnnectFail);
        recv.Add((ushort)INGAME.GameComplte, MakePacket<GameCompleteData>);
        handler.Add((ushort)INGAME.GameComplte, PacketHandler.GameComplete);
        recv.Add((ushort)INGAME.PlayerGoal, MakePacket<GameCompleteData>);
        handler.Add((ushort)INGAME.PlayerGoal, PacketHandler.PlayerGoal);
        recv.Add((ushort)INGAME.Reconnect, MakePacket<Data>);
        handler.Add((ushort)INGAME.Reconnect, PacketHandler.ReConnect);
        recv.Add((ushort)INGAME.PlayerDrop, MakePacket<Player>);
        handler.Add((ushort)INGAME.PlayerDrop, PacketHandler.PlayerFail);
    }

    public void OnReceievePacket(byte[] buffer, Pkt_Head head)
    {
        //UnityEngine.Debug.Log("Head size: " + head.size + "Head type:" + head.type);

        Action<byte[], uint, ushort> action = null;
        if (recv.TryGetValue((ushort)head.type, out action))
            action.Invoke(buffer, head.size, (ushort)head.type);
    }

    void MakePacket<T>(byte[] data, uint len, ushort id) where T : IMessage, new()
    {
        int size = Marshal.SizeOf<Pkt_Head>();

        T pkt = new T();
        pkt.MergeFrom(data, size, (int)len);
        if(id == time)
        {
            PacketHandler.GetTickCount(pkt);
        }
        else if(customHandler != null)
        {
            customHandler.Invoke(id, pkt);
        }

        //customHandler.Invoke(id, pkt);
    }

    public Action<IMessage> GetHandler(ushort id)
    {
        Action<IMessage> action = null;
        if (handler.TryGetValue(id, out action))
            return action;
        return null;
    }
}
public struct Pkt_Head
{
    //����� ������ ������
    public uint size;
    public INGAME type;
};