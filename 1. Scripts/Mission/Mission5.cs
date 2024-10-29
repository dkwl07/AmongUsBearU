using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission5 : MonoBehaviour
{
    public Transform rotate,handle;
    public Color blue,red;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    RectTransform rect_handle;
    MissionCtrl missionCtrl_script;

    bool isDrag, isPlay;
    float rand;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rect_handle = handle.GetComponent<RectTransform>();
        missionCtrl_script = FindObjectOfType<MissionCtrl>();

    }

    private void Update()
    {
        if (isPlay)
        {
            //�巡��
            if (isDrag)
            {
                handle.position = Input.mousePosition; //�ڵ��� ��ġ�� ���콺 ��ġ�� �Ű��ش�
                rect_handle.anchoredPosition = new Vector2(184, Mathf.Clamp(rect_handle.anchoredPosition.y, -195, 195));
                //�ڵ��� x�������� 184�� ����, y�� -195~195����



                //�巡�� ������ ��
                if (Input.GetMouseButtonUp(0))
                {
                    //�������� üũ
                    if(rect_handle.anchoredPosition.y > -5&&rect_handle.anchoredPosition.y < 5)
                    {
                        Invoke("MissionSuccess", 0.2f);
                        isPlay = false; //�ѹ��� �۾��ϰ� �ϱ�
                    }

                    isDrag = false;
                }
            }
            //z�� �ϴ� 90���� �̵��� ���̱� ������ 90�� �����ش�.
            rotate.eulerAngles = new Vector3(0, 0, 90 * rect_handle.anchoredPosition.y / 195);
            //195�� �������� y�� ��ŭ �̵��ߴ����� ���� �� ������� Rotate�� zȸ������ ������ �ȴ�.

            //���������� �Ǹ� �� ����
            if (rect_handle.anchoredPosition.y > -5 && rect_handle.anchoredPosition.y < 5)
            {
                rotate.GetComponent<Image>().color = blue;

            }
            else
            {
                rotate.GetComponent<Image>().color = red;
            }
        }
       
    }

    //�̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //�ʱ�ȭ
        rand = 0;

        //Rotate z ����
        rand = Random.Range(-195, 195);

        while(rand >= -10 &&rand <= 10) // Rotate�� z ������ -10~10 ���̸� �ٽ� ���� �۾����ֱ�(�̼� ������ �ʹ� �����)
        {
            rand = Random.Range(-195, 195);
        }

        rect_handle.anchoredPosition = new Vector2(184, rand); // �������� �ڵ鿡 y���� �����ȴ�.

        isPlay = true;
    }

    //���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //������ ������ ȣ��
    public void ClickHandle()
    {
        isDrag = true; //�巡�װ� ���۵ƴٴ� �� �˷���
    }

    //�̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        //�̼�ȭ���� �������� ���
        //ClickCancle�� ����ϱ� ������ �״�� ���
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
