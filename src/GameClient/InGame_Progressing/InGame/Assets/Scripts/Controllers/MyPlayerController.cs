using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Define;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//�÷��̾ goal �ϸ� stagenum�� �˻��Ѵ�. ���� ���� ���������� �ƴϸ� don't destory on load. �������������� destroy
//goal ���ϸ� �ٷ� destory. don't destory on load �Ѱ� ���� ������������ ������ġ�� ��ȯ
public class MyPlayerController : PlayerController
{
    //public  bool serverCommunication = false;
    GameScene gamescene;
    GameManager gamemanager;
    bool inMenu = false;
    bool inGoal = false;
    protected override void Init()
    {
        base.Init();
        gamescene = GameObject.Find("GameScene").GetComponent<GameScene>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (gamescene.CheckStartGame() && !inGoal)
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
            if (isJumping && State == BirdState.Jumping)
            {
                bool SlideBtn = Input.GetMouseButtonDown(1);
                if (SlideBtn)
                    isSliding = true;
                else
                    isSliding = false;
            }
            //esc�� ������ menupanel�� Ȱ��ȭ�ǰ� ���� Ű���� �ý����� �����ȴ�.
            //����ϱ⸦ ������ menupaneel�� ��Ȱ��ȭ�ǰ�, ���� Ű���� �ý����� �ٽ� ���۵ȴ�.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inMenu = true;
                gamemanager.ActiveMenu();
            }
        }
        //  Debug.Log("State : " + State + " isJumping: " + isJumping + " moveVec: " + moveVec + " pressedJump: " + pressedJump + "isSliding" + isSliding) ;
    }
    //Idle�� ��� ������, �ٸ� ���·� �Ѿ���� �Ǵ�.
    protected override void UpdateIdle()
    {
        if (moveVec.x != 0 || moveVec.z != 0)
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
            Move playerMove = new Move()
            {
                Id = playerId,
                Time = Managers.Network.TICK,
                Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                State = PlayerState.Idle,
            };
            Managers.Network.Send(playerMove, INGAME.PlayerMove);
        }
        else if (prevVec != transform.position)
        {
            Move playerMove = new Move()
            {
                Id = playerId,
                Time = Managers.Network.TICK,
                Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                State = PlayerState.Move,
            };
            Managers.Network.Send(playerMove, INGAME.PlayerMove);
        }
    }
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
                animator.SetBool("isSlide", true);
            transform.position += movementDirection * speed * Time.deltaTime;
        }
        if (!isSliding)
        {
            Move playerMove = new Move()
            {
                Id = playerId,
                Time = Managers.Network.TICK,
                Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                State = PlayerState.Jump,
            };
            Managers.Network.Send(playerMove, INGAME.PlayerMove);
        }
        else
        {
            Move playerMove = new Move()
            {
                Id = playerId,
                Time = Managers.Network.TICK,
                Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                State = PlayerState.Slide,
            };
            Managers.Network.Send(playerMove, INGAME.PlayerMove);
        }
    }
    void Jump()
    {
        if (!isJumping && State == BirdState.Jumping)
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
    //Goal �ϸ� invisible, ī�޶� ������ �� �ְ� �����.
    //���� �ð� �ʰ� or ��� �ο��� ��¼��� ����� ������ ����Ǹ� ����� �÷��̾ ���� Scene���� �ű��.
    //���� Scene���� �Ű��� Player���� Random �� ��ݼ����� Random Position�� �����ȴ�.
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Victory Ground") && !inGoal)
        {
            State = BirdState.Idle;
            isJumping = false;
            isSliding = false;
            inGoal = true;
            UpdateAnimation();
            PlayerGoalData pkt = new PlayerGoalData
            {
                Id = playerId,
                Success = true,
            };
            Managers.Network.Send(pkt, INGAME.PlayerGoal);
            clearStageNum++;
            inGoal = true;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isJumping)
            {
                //Debug.Log("collisionGround");
                State = BirdState.Idle;
                isJumping = false;
                isSliding = false;
                UpdateAnimation();
                Move playerMove = new Move()
                {
                    Id = playerId,
                    Time = Managers.Network.TICK,
                    Position = new Vector { X = transform.position.x, Y = transform.position.y, Z = transform.position.z },
                    Rotation = new Vector { X = transform.eulerAngles.x, Y = transform.eulerAngles.y, Z = transform.eulerAngles.z },
                    State = PlayerState.Idle,
                };
                Managers.Network.Send(playerMove, INGAME.PlayerMove);
            }
        }
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