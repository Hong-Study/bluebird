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


    public static byte[] Make_login_handler(Data pkt)
    {
        Pkt_Head head = new Pkt_Head();

        //�������� �ʿ��� ��ŭ�� ��ü ũ�⸦ ��ȯ
        int size = Marshal.SizeOf<Pkt_Head>();
        
        //��Ŷ ������ = pkt �� ������
        head.size = (uint)pkt.CalculateSize();
        byte[] send_buffer = new byte[head.size + size];

        //������ ����Ʈ ���� ����Ͽ� ���μ����� �������� �ʴ� �޸𸮿��� �޷θ��� �Ҵ� ��, buffsize��ŭ Unmanaged �޸� �Ҵ�
        IntPtr ptr = Marshal.AllocHGlobal(size);
        //�����Ǵ� ��ü�� �����͸� �������� �ʴ� �޸� ������� �������մϴ�.
        Marshal.StructureToPtr(head, ptr, true);
        //ptr �����Ϳ� ����Ǿ� �ִ� �ּ��� ��ġ�� ����, �̸� size��ŭ �����Ͽ� send_buffer�� ���� �ִ´�.
        Marshal.Copy(ptr, send_buffer, 0, size);
        //�Ҵ�� IntPtr unmanaged �޸𸮸� �����Ѵ�.
        Marshal.FreeHGlobal(ptr);

        //pkt���� send_buffer�� ����?
        Array.Copy(pkt.ToByteArray(), 0, send_buffer, size, head.size);

        return send_buffer;
    }

    public static byte[] Make_login_handler(Player pkt)
    {
        Pkt_Head head = new Pkt_Head();

        //�������� �ʿ��� ��ŭ�� ��ü ũ�⸦ ��ȯ
        int size = Marshal.SizeOf<Pkt_Head>();
        //��Ŷ ������?
        head.size = (uint)pkt.CalculateSize();
        byte[] send_buffer = new byte[head.size + size];

        //������ ����Ʈ ���� ����Ͽ� ���μ����� �������� �ʴ� �޸𸮿��� �޷θ��� �Ҵ� ��, buffsize��ŭ Unmanaged �޸� �Ҵ�
        IntPtr ptr = Marshal.AllocHGlobal(size);
        //�����Ǵ� ��ü�� �����͸� �������� �ʴ� �޸� ������� �������մϴ�.
        Marshal.StructureToPtr(head, ptr, true);
        //ptr �����Ϳ� ����Ǿ� �ִ� �ּ��� ��ġ�� ����, �̸� size��ŭ �����Ͽ� send_buffer�� ���� �ִ´�.
        Marshal.Copy(ptr, send_buffer, 0, size);
        //�Ҵ�� IntPtr unmanaged �޸𸮸� �����Ѵ�.
        Marshal.FreeHGlobal(ptr);

        //pkt���� send_buffer�� ����?
        Array.Copy(pkt.ToByteArray(), 0, send_buffer, size, head.size);

        return send_buffer;
    }
}
struct Pkt_Head
{
    public uint size;
    public INGAME type;
};