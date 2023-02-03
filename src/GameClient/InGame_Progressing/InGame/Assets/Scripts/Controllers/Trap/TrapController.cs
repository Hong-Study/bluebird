using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrapController : ObstacleController
{
    // Start is called before the first frame update

    public float speed;
    Vector position = new Vector();

    public Int64 id { get; set; }
    public Vector PosInfo
    {
        get { return position; }
        set { position = value; }
    }

    // Update is called once per frame

    protected override void UpdateController()
    {
        Vector3 pos = new Vector3(PosInfo.X, PosInfo.Y, PosInfo.Z);
        Vector3 moveDir = pos - transform.position;

        float dist = moveDir.magnitude;
        if(dist < speed * Time.deltaTime)
        {
           transform.position = pos;
        }
        else
        {
            transform.position += moveDir.normalized * speed * Time.deltaTime;
        }
    }
}
