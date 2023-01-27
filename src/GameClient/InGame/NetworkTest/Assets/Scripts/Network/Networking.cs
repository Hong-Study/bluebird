using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;
using Google.Protobuf;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

public class Networking : MonoBehaviour
{
    //private TcpClient socketConnection;
    //private Socket socket;
    private TcpClient socket;
    private Thread clientReceiveThread;

    //���� IP �� ��Ʈ��ȣ
    static private string mIp = "127.0.0.1";
    static private IPAddress ipAddress = IPAddress.Parse(mIp);
    static private int mPort = 5000;

    
    IPEndPoint endPoint = new IPEndPoint(ipAddress, mPort);



    //������ (���������� �����)
    private IPEndPoint mIpEndPoint;
   

    public void Start()
    {
        ConnectToTcpServer();
    }



    private void ConnectToTcpServer()
    {
        try
        {

            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            //�����尡 ��� ������������ ��Ÿ���� ���� �������ų� ����, true�� ��׶��� ������� �����ڴ�
            //���� �����尡 ����Ǹ� ��׶��� �����嵵 �����. �׷��� ���׶��� ������� ���� �����尡 ����ŵ� �۵�
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("On Client connection exception " + e);
        }
    }

    public void ListenForData()
    {
        try
        {
            //ip �ּҿ� port�� �����Ͽ� �ڵ����� TCP ���� 
            socket = new TcpClient(mIp, mPort);
           

            UnityEngine.Debug.Log("Connected...");
            byte[] bytes = new byte[1024];
            

            while (true)
            {
                //using���� ����ϸ� ���ҽ��� ������ ����� �ڵ����� dispose�Ͽ� ������ ���� ������.
                //getstream �޼���� tcp ��Ʈ��ũ ��Ʈ���� ����, �� ��Ʈ��ũ ��Ʈ���� �̿��� ��Ʈ��ũ�� ������ �ۼ���

                using (NetworkStream stream = socket.GetStream())
                {
                    int len;


                    //read �޼ҵ带 ���� �������� ����Ʈ �����͸� �о�´�.
                    while ((len = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        UnityEngine.Debug.Log("something came");
                        byte[] incommingdata = new byte[len];

                        Pkt_Head head = PacketHandler.HandlerPacket<Pkt_Head>(incommingdata, len);

                        UnityEngine.Debug.Log("%d", head.size);
                        Array.Copy(bytes, 0, incommingdata, 0, len);


                    }
                }
            }

        }
        catch (SocketException socketException)
        {
            UnityEngine.Debug.Log("Socket Exception " + socketException);
        }
    }

    public void SendMessage()
    {
        //���� ������ �ȵ� ����
        if (!socket.Connected)
            return;
        try
        {
            if (socket.Connected)
            {
                NetworkStream stream = socket.GetStream();
                if (stream.CanWrite)
                {

                    Data dataPkt = new Data()
                    {
                        Id = 1,
                        MapLevel = 2,
                        MatchRoom = 0,
                        //Player = {new Player {X = 0,Y=0,Z=0 } }

                    };
                  

                    byte[] datas = PacketHandler.Make_login_handler(dataPkt, INGAME.Connect);

                    //����Ʈ �迭�� �־� ����
                    stream.Write(datas);
                    UnityEngine.Debug.Log("Client Sent Map Join Message");
                }
            }
        }
        catch (SocketException socketException)
        {
            UnityEngine.Debug.Log("SendMessage Exception: " + socketException);
        }
    }

    public void SendPlayerMessage()
    {
        //���� ������ �ȵ� ����
        if (!socket.Connected)
            return;
        try
        {
            NetworkStream stream = socket.GetStream();
            if (stream.CanWrite)
            {

                Data dataPkt = new Data()
                {
                    Id = 1,
                    MapLevel = 2,
                    MatchRoom = 0,
                    Player = { new Player { X = 0.0f, Y = 0.0f, Z = 0.0f } }

                };

                byte[] datas = PacketHandler.Make_login_handler(dataPkt, INGAME.Move);

                //����Ʈ �迭�� �־� ����
                stream.Write(datas);
                UnityEngine.Debug.Log("Client Sent Player Message");
            }
        }
        catch (SocketException socketException)
        {
            UnityEngine.Debug.Log("Socket exception: " + socketException);
        }
    }
}
