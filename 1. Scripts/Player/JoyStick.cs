using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.스틱 드래그 + 제한
//2. 드래그한만큼 캐릭터 이동

public class JoyStick : MonoBehaviour
{

    Animator anim;
    public RectTransform stick,backGround; //스틱의 위치, 스틱의 큰 원

    PlayerCtrl playerCtrl_script; //PlayerCtrl.cs 파일 선언

    bool isDrag;
    float limit;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerCtrl_script = GetComponent<PlayerCtrl>(); //시작할때 컴포넌트 가져오기
        limit = backGround.rect.width * 0.5f; //제한값
    }

    private void Update()
    {
        //드래그하는 동안
        if (isDrag)
        {
            Vector2 vec = Input.mousePosition - backGround.position; //마우스의 위치 - 큰 동그라미 중앙위치
            stick.localPosition = Vector2.ClampMagnitude(vec, limit); //제한한 값을 다시 스틱 로컬포지션에 넣어줌

            Vector3 dir = (stick.position - backGround.position).normalized;
            transform.position += dir * playerCtrl_script.speed * Time.deltaTime; //playerCtrl_script의 안에 있는 스피드값
            //Player가 터치 이동 했을 때와 조이스틱으로 이동했을 때 속도가 같아지게 된다. 
           
            anim.SetBool("isWalk", true); //애니메이션 조건 부울값

            //왼쪽으로 이동
            if (dir.x < 0)   //dir.x는 위의 벡터 좌표(x,y,z)중 x좌표
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //오른쪽으로 이동
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            //드래그 끝나면
            if (Input.GetMouseButtonUp(0))  // 스틱의 터치가 끝났을 때
            {
                stick.localPosition = new Vector3(0,0,0);
                anim.SetBool("isWalk", false);

                isDrag = false; //원래대로 돌아옴
            }
        }
    }

    //스틱을 누르면 호출
    public void ClickStick()
    {
        isDrag = true;
    }
}
