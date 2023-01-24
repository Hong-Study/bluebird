using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform player;
    public Vector3 offset; //ī�޶�� �÷��̾��� ����
    public float dampTrace = 20.0f; //�ε巯�� ������ ���� ����

    private Vector3 targetPos;


    void LateUpdate()
    {

        targetPos = player.position + offset;
        //lerp�� ���� ������, ���� a ���� ���� b�� ����� �� �ѹ��� ������� �ʰ�, �ε巴�� �����ϱ� ���ؼ� ��� 
        //������ �Ű������� 0�̸� a ��ȯ 1�̸� b ��ȯ 0~1������ �Ǽ��� ������ �Ű������� ��
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * dampTrace);
    }
}
