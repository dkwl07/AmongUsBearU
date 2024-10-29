using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission1 : MonoBehaviour
{
    public Color red;
    public Image[] images; //이미지 배열

    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_script;
  
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

        //초기화: 미션이 시작될 때마다 초기화되어 밑의 랜덤한 작업을 실행하게 함
        for(int i = 0; i < images.Length; i++)
        {
            images[i].color = Color.white;
        }

        //랜덤
        for(int i = 0; i < 4; i++) //최대 4개까지만 빨간색으로 시작
        {
            int rand = Random.Range(0, 7);
            //0~6까지중 하나가 랜덤으로 int변수에 들어가게 된다.

            images[rand].color = red;
            //중복을 고려하지 않았기 때문에 4개중 4개가 다 하나의 이미지로만 빨간색이 될 수 있고, 최대 4개까지 빨간색이 될 수 있다.
        }
    }

    //엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //육각형 버튼 누르면 호출
    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        //currentSelectedGameObject로 지금 클릭한 이 GameObject 정보를 가져와서 GetComponent로 이미지를 가져온다.

        //위에서 가져온 이미지가 하얀색이라면
        if(img.color == Color.white)
        {
            //빨간색으로
            img.color = red;
        }
        //빨간색이라면
        else
        {
            //하얀색으로
            img.color = Color.white;
        }

        //성공여부 체크 : 버튼을 클릭할 때마다 확인해야하기 때문에

        int count = 0;

        for(int i = 0; i < images.Length; i++)
        {
            if (images[i].color == Color.white)
            {
                count++; //화이트 버튼의 개수 카운트
            }
        }

        if (count == images.Length)
        {
            //성공
            Invoke("MissionSuccess", 0.2f); //몇초후에 호출하고싶기 때문에 Invoke("호출하고싶은 함수 이름",원하는 딜레이 시간)기능 사용

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
