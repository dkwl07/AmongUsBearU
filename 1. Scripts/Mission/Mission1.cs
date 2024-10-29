using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission1 : MonoBehaviour
{
    public Color red;
    public Image[] images; //�̹��� �迭

    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_script;
  
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_script = FindObjectOfType<MissionCtrl>();
    }

    //�̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //�ʱ�ȭ: �̼��� ���۵� ������ �ʱ�ȭ�Ǿ� ���� ������ �۾��� �����ϰ� ��
        for(int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }

        //����
        for(int i = 0; i < 4; i++) //�ִ� 4�������� ���������� ����
        {
            int rand = Random.Range(0, 7);
            //0~6������ �ϳ��� �������� int������ ���� �ȴ�.

            images[rand].color = red;
            //�ߺ��� ������� �ʾұ� ������ 4���� 4���� �� �ϳ��� �̹����θ� �������� �� �� �ְ�, �ִ� 4������ �������� �� �� �ִ�.
        }
    }

    //���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //������ ��ư ������ ȣ��
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        //currentSelectedGameObject�� ���� Ŭ���� �� GameObject ������ �����ͼ� GetComponent�� �̹����� �����´�.

        //������ ������ �̹����� �Ͼ���̶��
        if(img.color == Color.white)
        {
            //����������
            img.color = red;
        }
        //�������̶��
        else
        {
            //�Ͼ������
            img.color = Color.white;
        }

        //�������� üũ : ��ư�� Ŭ���� ������ Ȯ���ؾ��ϱ� ������

        int count = 0;

        for(int i = 0; i < images.Length; i++)
        {
            if (images[i].color == Color.white)
            {
                count++; //ȭ��Ʈ ��ư�� ���� ī��Ʈ
            }
        }

        if (count == images.Length)
        {
            //����
            Invoke("MissionSuccess", 0.2f); //�����Ŀ� ȣ���ϰ�ͱ� ������ Invoke("ȣ���ϰ���� �Լ� �̸�",���ϴ� ������ �ð�)��� ���

        }
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
