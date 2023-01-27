using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Protocol;

//�������̶� �� ��ü�� �޸𸮿����� ǥ������� ���� �Ǵ� ���ۿ� ������ �ٸ� ������ �������� ��ȯ�ϴ� �����̴�.
//����ȭ�� �����ϴ�. ������ ����� �ܼ�ȭ�Ͽ� ���� �����͸� �ְ� �ް� �ϱ� ����.

//C#���� �ۼ��Ǵ� �ڵ�� ManagedCode, �������� C/C++ �����Ϸ��� ���� �����ϵǴ� �ڵ�� UnManagedCode
//Managed Code�� GC(Garbage Collector)�� ���� ������ �Ǵ� �޸� �ݴ�� ���α׷� �ڵ峪 �ü���� ���� ������ �ȴ�.


class PacketHandler
{
    public static PacketHandler Instance = new PacketHandler();


    public static byte[] Make_login_handler(Data pkt, INGAME type)
    {
        Pkt_Head head = new Pkt_Head();
        head.type = type;

        //�������� �ʿ��� ��ŭ�� ��ü ũ�⸦ ��ȯ
        int size = Marshal.SizeOf<Pkt_Head>();
        
        //��Ŷ ������ = pkt �� ������
        head.size = (uint)pkt.CalculateSize();
        byte[] send_buffer = new byte[head.size + size];

        //������ ����Ʈ ���� ����Ͽ� ���μ����� �������� �ʴ� �޸𸮿��� �޷θ��� �Ҵ� ��, buffsize��ŭ Unmanaged �޸� �Ҵ�
        //�����Ǵ� ��ü�� �����͸� �������� �ʴ� �޸� ������� �������մϴ�.
        //ptr �����Ϳ� ����Ǿ� �ִ� �ּ��� ��ġ�� ����, �̸� size��ŭ �����Ͽ� send_buffer�� ���� �ִ´�.
        //�Ҵ�� IntPtr unmanaged �޸𸮸� �����Ѵ�.

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(head, ptr, true);
        Marshal.Copy(ptr, send_buffer, 0, size);
        Marshal.FreeHGlobal(ptr);

        //pkt���� send_buffer�� ����?
        Array.Copy(pkt.ToByteArray(), 0, send_buffer, size, head.size);
        
        return send_buffer;
    }

    //https://technodori.tistory.com/entry/C-byte-%EA%B5%AC%EC%A1%B0%EC%B2%B4-%EA%B5%AC%EC%A1%B0%EC%B2%B4-byte
    //���ۿ����� �ѹ���Ʈ�� �����ͷ� �̵��ؼ� ������ �Ѵ�.
    //receieve�����͸� ������, ��� ����� ã�ƾ���.
    public static Pkt_Head HandlerPacket<Pkt_Head>(byte[] data, int len) where Pkt_Head : struct
    {
        //�迭�� ũ�⸸ŭ ����� �޸� ������ �޸� �Ҵ�
        //�迭�� ����� �����͸� ������ �Ҵ��� �޸� ������ �����Ѵ�.
        //������ �����͸� ����ü ��ü�� ��ȯ
        //����� �޸� ������ �Ҵ��ߴ� �޸𸮸� ����
        int size = Marshal.SizeOf<Pkt_Head>();
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(data, 0, ptr, size);
        Pkt_Head head = (Pkt_Head)Marshal.PtrToStructure(ptr, typeof(Pkt_Head));
        Marshal.FreeHGlobal(ptr);

        return head;
    }
}
struct Pkt_Head
{
    //����� ������ ������
    public uint size;
    public INGAME type;
};