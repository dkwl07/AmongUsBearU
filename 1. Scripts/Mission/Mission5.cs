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
            //드래그
            if (isDrag)
            {
                handle.position = Input.mousePosition; //핸들의 위치를 마우스 위치로 옮겨준다
                rect_handle.anchoredPosition = new Vector2(184, Mathf.Clamp(rect_handle.anchoredPosition.y, -195, 195));
                //핸들을 x포지션은 184로 고정, y는 -195~195까지



                //드래그 끝났을 때
                if (Input.GetMouseButtonUp(0))
                {
                    //성공여부 체크
                    if(rect_handle.anchoredPosition.y > -5&&rect_handle.anchoredPosition.y < 5)
                    {
                        Invoke("MissionSuccess", 0.2f);
                        isPlay = false; //한번만 작업하게 하기
                    }

                    isDrag = false;
                }
            }
            //z는 일단 90까지 이동할 것이기 때문에 90을 곱해준다.
            rotate.eulerAngles = new Vector3(0, 0, 90 * rect_handle.anchoredPosition.y / 195);
            //195를 기준으로 y가 얼만큼 이동했는지에 따라서 그 비율대로 Rotate의 z회전값이 변경이 된다.

            //성공기준이 되면 색 변경
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

    //미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //초기화
        rand = 0;

        //Rotate z 랜덤
        rand = Random.Range(-195, 195);

        while(rand >= -10 &&rand <= 10) // Rotate의 z 랜덤이 -10~10 사이면 다시 랜덤 작업해주기(미션 성공과 너무 가까움)
        {
            rand = Random.Range(-195, 195);
        }

        rect_handle.anchoredPosition = new Vector2(184, rand); // 랜덤으로 핸들에 y값이 지정된다.

        isPlay = true;
    }

    //엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //손잡이 누르면 호출
    public void ClickHandle()
    {
        isDrag = true; //드래그가 시작됐다는 걸 알려줌
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
