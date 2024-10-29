using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCtrl : MonoBehaviour
{ 
    public Slider guage;
    public CircleCollider2D[] colls;
    public GameObject text_anim,mainView;

    int MissionCount;

    //�̼��ʱ�ȭ
    public void MissionReset()
    {
        guage.value = 0;
        MissionCount = 0;

        for(int i=0; i< colls.Length; i++)
        {
            colls[i].enabled = true; //Ȱ��ȭ
        }

        text_anim.SetActive(false);
    }
    
    //�̼� �����ϸ� ȣ��
    public void MissionSuccess(CircleCollider2D coll)
    {
         MissionCount++;

         guage.value = MissionCount / 7f;

         //������ �̼��� �ٽ� �÷��� X
         coll.enabled = false;

        //�������� üũ
        if(guage.value == 1)
        {
            text_anim.SetActive(true);

            Invoke("Change", 1f);
        }
    }

    //ȭ�� ��ȯ
    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        //ĳ���� ����
        FindObjectOfType<PlayerCtrl>().DestroyPlayer();
    }
}
