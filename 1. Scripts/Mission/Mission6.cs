using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission6 : MonoBehaviour
{
    public bool[] isColor = new bool[4]; //4개의 선이 있으니 4개의 bool값 생성
    public RectTransform[] rights; // 오른쪽 선들의 위치
    public LineRenderer[] lines;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_script;

    Vector2 clickPos;
    LineRenderer line;
    Color leftC, rightC;

    bool isDrag;
    float leftY, rightY;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_script = FindObjectOfType<MissionCtrl>();

    }

    private void Update()
    {
        //드래그
        if (isDrag)
        {
            //지정해줄 포지션 중 0과 1에서 1만 조정                          1920f : 타깃 레퍼런스의 x부분(float값),Screen.width: 실제 해상도의 넓이
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickPos.x)*1920f/Screen.width, (Input.mousePosition.y - clickPos.y)*1080f/Screen.height,-10)); //포지션을 지정해줄 수 있다.

            //드래그 끝났을 때
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition); //화면에서 마우스포지션 위치로 Raycast를 쏜다
                //Raycast? 레이저 같은 것

                RaycastHit hit;

                //Raycast를 쏴서 맞은 것이 있다는 뜻(오른쪽 선에 맞았다면)
                if (Physics.Raycast(ray,out hit))
                {
                    GameObject rightline = hit.transform.gameObject; //hit에 저장된 맞은 물체를 저장
                    rightY = rightline.GetComponent<RectTransform>().anchoredPosition.y; //오른쪽 선 y값
                    //오른쪽 선 색상
                    rightC = rightline.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    //색 비교
                    if(leftC == rightC) //맞았을때
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = true; break;
                            case 75: isColor[1] = true; break;
                            case -75: isColor[2] = true; break;
                            case -255: isColor[3] = true; break;
                        }
                    }
                    else //틀렸을 때
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = false; break;
                            case 75: isColor[1] = false; break;
                            case -75: isColor[2] = false; break;
                            case -255: isColor[3] = false; break;
                        }
                    }

                    //성공여부 체크
                    if(isColor[0]&& isColor[1]&& isColor[2]&& isColor[3])
                    {
                        Invoke("MissionSuccess", 0.2f);
                    }
                }
                //닿지않았다면
                else
                {
                    line.SetPosition(1, new Vector3(0, 0, -10)); //원위치
                }

                isDrag = false;
            }
        }       
    }

    //미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //초기화
        for(int i = 0; i < 4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        //랜덤
        for(int i = 0; i < rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0, 4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;

            rights[rand].anchoredPosition = temp;
        }

    }

    //엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //선 누르면 호출
    public void ClickLine(LineRenderer click)
    {
        clickPos = Input.mousePosition; // 마우스 포지션 벡터값 저장
        line = click; //LineRenderer 가져오기


        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y; //클릭한 왼쪽 선의 y값

        //왼쪽 선 색상
        leftC = click.transform.parent.GetComponent<Image>().color;

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
