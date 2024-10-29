using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission4 : MonoBehaviour
{
    public Transform numbers;
    public Color blue;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_script;

    int count;
  
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

        //�ʱ�ȭ
        for(int i = 0; i < numbers.parent.childCount; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white; //���� �ʱ�ȭ
            numbers.GetChild(i).GetComponent<Button>().enabled = true; //��ư Ȱ��ȭ �ʱ�ȭ

        }

        //���� ���� ��ġ
        for (int i = 0; i < 10; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite = numbers.GetChild(rand).GetComponent<Image>().sprite;

            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;
        }

        count = 1;
    }

    //���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //���ڹ�ư ������ ȣ��
    public void ClickNumber()
    {
        //count.ToString() => int���� string���� �ڷ��� ����
        if (count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)
        {
            //������
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;

            //��ư ��Ȱ��ȭ
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
            count++;

            //�������� üũ
            if(count == 11)
            {
                Invoke("MissionSuccess", 0.2f);
            }
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
