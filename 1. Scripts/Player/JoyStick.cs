using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.��ƽ �巡�� + ����
//2. �巡���Ѹ�ŭ ĳ���� �̵�

public class JoyStick : MonoBehaviour
{

    Animator anim;
    public RectTransform stick,backGround; //��ƽ�� ��ġ, ��ƽ�� ū ��

    PlayerCtrl playerCtrl_script; //PlayerCtrl.cs ���� ����

    bool isDrag;
    float limit;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerCtrl_script = GetComponent<PlayerCtrl>(); //�����Ҷ� ������Ʈ ��������
        limit = backGround.rect.width * 0.5f; //���Ѱ�
    }

    private void Update()
    {
        //�巡���ϴ� ����
        if (isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position; //���콺�� ��ġ - ū ���׶�� �߾���ġ
            stick.localPosition = Vector2.ClampMagnitude(vec, limit); //������ ���� �ٽ� ��ƽ ���������ǿ� �־���

            Vector3 dir = (stick.position - backGround.position).normalized;
            transform.position += dir * playerCtrl_script.speed * Time.deltaTime; //playerCtrl_script�� �ȿ� �ִ� ���ǵ尪
            //Player�� ��ġ �̵� ���� ���� ���̽�ƽ���� �̵����� �� �ӵ��� �������� �ȴ�. 
           
            anim.SetBool("isWalk", true); //�ִϸ��̼� ���� �οﰪ

            //�������� �̵�
            if (dir.x < 0)   //dir.x�� ���� ���� ��ǥ(x,y,z)�� x��ǥ
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //���������� �̵�
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //�巡�� ������
            if (Input.GetMouseButtonUp(0))  // ��ƽ�� ��ġ�� ������ ��
            {
                stick.localPosition = new Vector3(0,0,0);
                anim.SetBool("isWalk", false);

                isDrag = false; //������� ���ƿ�
            }
        }
    }

    //��ƽ�� ������ ȣ��
    public void ClickStick()
    {
        isDrag = true;
    }
}
