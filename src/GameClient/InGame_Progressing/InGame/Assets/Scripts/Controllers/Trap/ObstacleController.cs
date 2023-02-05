﻿using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleController : MonoBehaviour
{
    Rigidbody rigidbody;

    public float speed;
    protected Vector3 direction;
    Vector position = new Vector();
    Vector rotation = new Vector();
    bool isRecv = false;
    public Int64 id { get; set; }
    public Vector PosInfo
    {
        get { return position; }
        set { position = value; }
    }
    //public Vector RotInfo
    //{
    //    get { return rotation; }
    //    set { rotation = value; }
    //}
    public bool PacketRecv
    {
        get { return isRecv; }
        set { isRecv = value; }
    }
    void Start()
    {
        rigidbody = new Rigidbody();
        transform.position = new Vector3(position.X, position.Y, position.Z);
        
        //x,y,z 축마다 이동 테스트를 위한 임시 코드
        switch(id)
        {
            case 0:
                direction = new Vector3(1, 0, 0);
                break;
            case 1:
                direction = new Vector3(0, 1, 0);
                break;
            case 2:
                direction = new Vector3(0, 0, 1);
                break;
            default:
                direction = new Vector3(0, 0, 0);
                break;
        }

    }
    void Update()
    {
        UpdateController();
    }
    protected virtual void UpdateController() { }
}
