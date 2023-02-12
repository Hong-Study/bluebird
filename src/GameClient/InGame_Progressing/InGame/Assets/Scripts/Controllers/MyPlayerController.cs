using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Define;


//���ڸ� �����Ҷ� Idle�ε� isJumping�� True�� ���°� ���ӵǼ� ���濡�� �Ⱥ��δ�.
public class MyPlayerController : PlayerController
{

    //public  bool serverCommunication = false;

    GameScene gamescene;


    protected override void Init()
    {
        
        base.Init();
        gamescene = GameObject.Find("GameScene").GetComponent<GameScene>();


    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case BirdState.Idle:
                GetInput();
                break;
            case BirdState.Moving:
                GetInput();
                break;
            case BirdState.Jumping:
                GetInput();
                break;
        }
        base.UpdateController();
    }

    void GetInput()
    {
        if (gamescene.CheckStartGame())
        {
            if (transform.position.y < -1)
            {
                transform.position = new Vector3(0.1f, 0.2f, 29f);
                transform.rotation = Quaternion.Euler(0, 180f, 0f);
            }

            if (State == BirdState.Jumping && isJumping == false)
            {
                State = BirdState.Idle;
            }


            float h = 0.0f;
            float v = 0.0f;


            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");


            pressedJump = Input.GetKeyDown(KeyCode.Space);
            moveVec = new Vector3(h, 0f, v).normalized;


            if (pressedJump && !isJumping)
            {
                State = BirdState.Jumping;
            }

            if(isJumping)
            {
                bool SlideBtn = Input.GetMouseButtonDown(1);

                if (SlideBtn)
                    isSliding = true;

            }
        }

        Debug.Log("State : " + State + " isJumping: " + isJumping + " moveVec: " + moveVec + " pressedJump: " + pressedJump + "isSliding" + isSliding) ; 

    }

    //Idle�� ��� ������, �ٸ� ���·� �Ѿ���� �Ǵ�.
    protected override void UpdateIdle()
    {
      

        if (moveVec.x != 0 || moveVec.z != 0 )
        {
            State = BirdState.Moving;
            return;
        }


    }

    //�÷��̾ ���� �̵��ϰ� ��ǥ�� ����, �÷��̾��� ���󿡼��� �������� �����Ѵ�.
    protected override void UpdateMoving()
    {

       
     
            prevVec = transform.position;
     
 
            Vector3 movementDirection = Quaternion.AngleAxis(cam.transform.eulerAngles.y, Vector3.up) * moveVec;
       
            movementDirection.Normalize();

            transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
            transform.position += movementDirection * speed * Time.deltaTime;

            UpdateAnimation();

        if (prevVec == transform.position)
            {

                State = BirdState.Idle;
                UpdateAnimation();

            }
            else if (prevVec != transform.position)
            {

                    Move playerMove = new Move()
                    {
                        Id = playerId,
                        Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                        Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                        State = PlayerState.Move,

                    };

                    Managers.Network.Send(playerMove, INGAME.PlayerMove);
 
           }

   
        
    }


    //������ �ؼ� �����Ҷ����� ����ؼ� ��Ŷ�� ��������Ѵ�.
    //������ �ϰ�, ������ �ϸ鼭�� ���� ������ �� �־���Ѵ�.
    //������ �ϰ�, Moving���� �ٲ��ֱ�
    protected override void UpdateJumping()
    {

        prevVec = transform.position;

        Vector3 movementDirection = Quaternion.AngleAxis(cam.transform.eulerAngles.y, Vector3.up) * moveVec;

        movementDirection.Normalize();

        transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);

        //�ٴڿ� �������ִ� ���¶�� ���� ����
        if (!isJumping && State == BirdState.Jumping)
        {
            Jump();
            isJumping = true;
        }

        //�������� ���߿� ���ִٸ� ����ؼ� ��Ŷ ����
        if (isJumping && State == BirdState.Jumping)
        {
            UpdateAnimation();

            if (isSliding)
                animator.SetBool("isSlide",true);

            transform.position += movementDirection * speed * Time.deltaTime;
        }


        Move playerMove = new Move()
        {
            Id = playerId,
            Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
            Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
            State = PlayerState.Jump,
        };


        Managers.Network.Send(playerMove, INGAME.PlayerMove);

    }

    void Jump()
    {
        if (!isJumping && State == BirdState.Jumping )
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            animator.SetTrigger("doJump");
           
        }
        else
            return;
    }

    void UpdateAnimation()
    {
        switch (State)
        {
            case BirdState.Idle:
                animator.SetBool("MoveForward", false);
                animator.SetBool("inAir", false);
                animator.SetBool("isSlide", false);
                break;
            case BirdState.Moving:
                animator.SetBool("MoveForward", true);
                animator.SetBool("inAir", false);
                animator.SetBool("isSlide", false);
                break;
            case BirdState.Jumping:
                animator.SetBool("MoveForward", false);
                animator.SetBool("inAir", true);
                break;
            

        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Victory Ground"))
        {
            isJumping = false;

            /*
            Player pkt = new Player()
            {
                Id = playerId,
                Position = playerInfo.Position,
                Rotation = playerInfo.Rotation
            };
            */

            PlayerGoalData pkt = new PlayerGoalData
            {
                Id = playerId,
                Success = true,
            };
            Managers.Network.Send(pkt, INGAME.PlayerGoal);
            Debug.Log("GameComplete Packet Sent");

            Destroy(this.gameObject);
          
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                Debug.Log("collisionGround");
                State = BirdState.Idle;
                isJumping = false;
                isSliding = false;

                UpdateAnimation();



            }
        }
    }

    protected override void OnCollisionStay(Collision collision)
    {
        
    }




}


/*
 //������ ��ǥ�� �����ָ� �̵��ϴ� ����
        if (serverCommunication)
        {
            PlayerState prevState = State;
            prevVec = transform.position;


        
            //moveVec = transform.position + (moveVec * speed * Time.deltaTime);




            if (prevState != State || prevVec != moveVec)
            {

                Move playerMove = new Move()
                {
                    Id = playerId,
                    Position = new Vector { X = moveVec.x, Y = moveVec.y, Z = moveVec.z },
                    Rotation = new Vector { X = moveVec.x, Y = moveVec.y, Z = moveVec.z },
                };

                Managers.Network.Send(playerMove, INGAME.PlayerMove);

            }

            if (playerInfo.Position.X == prevVec.x && playerInfo.Position.Z == prevVec.z)
            {
                State = PlayerState.Idle;
                return;
            }
            else
            {
                State = PlayerState.Moving;
                moveVec = new Vector3(playerInfo.Position.X, playerInfo.Position.Y, playerInfo.Position.Z);
                transform.position = moveVec;

                Debug.Log("Player:UpdateMoving : moveVec    " + moveVec + "State :" + State);
            }
        }
        */
