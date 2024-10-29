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

    //미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //초기화
        for(int i = 0; i < numbers.parent.childCount; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white; //색상 초기화
            numbers.GetChild(i).GetComponent<Button>().enabled = true; //버튼 활성화 초기화

        }

        //숫자 랜덤 배치
        for (int i = 0; i < 10; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite = numbers.GetChild(rand).GetComponent<Image>().sprite;

            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;
        }

        count = 1;
    }

    //엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //숫자버튼 누르면 호출
    public void ClickNumber()
    {
        //count.ToString() => int에서 string으로 자료형 변경
        if (count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)
        {
            //색변경
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;

            //버튼 비활성화
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
            count++;

            //성공여부 체크
            if(count == 11)
            {
                Invoke("MissionSuccess", 0.2f);
            }
        }
    }

    //미션 성공하면 호출
    public void MissionSuccess()
    {
        //미션화면이 내려가는 기능
        //ClickCancle과 비슷하기 때문에 그대로 사용
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());
    }
}
