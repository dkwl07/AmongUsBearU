using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission3 : MonoBehaviour
{
    public Text inputText,keyCode;

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

        //�ʱ�ȭ
        inputText.text = "";
        keyCode.text = "";

        //Ű�ڵ� ����
        for (int i = 0; i < 5; i++)
        {
            keyCode.text += Random.Range(0, 10);
        }

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
        if(inputText.text.Length <= 4) //�ִ� ���� 5��
        {
            inputText.text += EventSystem.current.currentSelectedGameObject.name;
        }
        
    }

    //������ư ������ ȣ��
    public void ClickDelete()
    {
        if(inputText.text != "") //������� ���� ��
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1); //�ڸ� ��Ʈ�� text�� inputText.text�� �־��ش�.
        }
    }

    //üũ��ư ������ ȣ��
    public void ClickCheck()
    {
        if(inputText.text == keyCode.text)
        {
            MissionSuccess();
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
