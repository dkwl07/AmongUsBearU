using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{ 
    public GameObject JoyStick,mainView,playView; //게임오브젝트 조이스틱,메인,미션 가져옴 (메인,미션은 MainMenu스크립트에서 지정해줌)
    public Settings settings_script;  // Settings 스크립트 가져오기
    public Button btn;
    public Sprite use, kill;
    public Text text_cool; // 쿨타임 시간 표시

    Animator anim; //Animator 선언
    GameObject coll;
    KillCtrl killCtrl_script;

    public float speed;

    public bool isCantMove, isMission;

    float timer;
    bool isCool, isAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Camera.main.transform.parent = transform; //캐릭터의 transform
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        //미션이라면
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;

            text_cool.text = ""; // 미션이라면 쿨타임 필요없음
        }
        // 킬 퀘스트라면
        else
        {
            killCtrl_script = FindObjectOfType<KillCtrl>();
            btn.GetComponent<Image>().sprite = kill;

            timer = 5;
            isCool = true;
        }
    }

    //화면을 클릭했는지 판단하는 일 -> 매프레임마다 확인
    private void Update()
    {
        //쿨타임
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString(); //소숫점까지 말고 올림으로 출력

            if(text_cool.text == "0") //음수가 되지않게 0이 되면 ""으로 아무것도 출력되지 않게 함
            {
                text_cool.text = "";
                isCool = false;
            }
        }

        if (isCantMove)
        {
            JoyStick.SetActive(false);
        }
        else
        {
           Move();
        }

        //애니메이션이 끝났다면
        if (isAnim && killCtrl_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killCtrl_script.kill_anim.SetActive(false);
            killCtrl_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    
    }

    //캐릭터 움직임 관리 : 화면의 상하좌우를 클릭해서 캐릭터를 이동시키는 기능
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            JoyStick.SetActive(true); //조이스틱 보이게
        }
        else
        {
            JoyStick.SetActive(false); //조이스틱 안보이게

            //클릭했는지 판단
            if (Input.GetMouseButton(0))   
            {
#if UNITY_EDITOR
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //클릭한 포지션 -화면 정중앙 의 방향벡터 좌표
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    //캐릭터 이동(원하는 속도로 이동) Time.deltaTime: 실제 시간
                    transform.position += dir * speed * Time.deltaTime;

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
                }
#else
if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                //Input.GetTouch(0): Input.GetMouseButton처럼 터치가 입력이 되는지 확인하는 부분
                //마우스는 1개지만 터치는 여러개가 입력 될 수 있기 때문에 그 중 0번째 터치 아이디를 입력한다는 뜻(유니티 에디터에선 작동 x)
                {
                    //클릭한 포지션 -화면 정중앙 의 방향벡터 좌표
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    //캐릭터 이동(원하는 속도로 이동) Time.deltaTime: 실제 시간
                    transform.position += dir * speed * Time.deltaTime;

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
                }
#endif

            }
            else //클릭하지 않았을 때
            {
                anim.SetBool("isWalk", false); //애니메이션 조건 부울값
            }
        }
    }

    //캐릭터 삭제
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null; //카메리의 부모가 캐릭터로 되어 있었기 때문에 캐릭터가 삭제되면 null로 지정해준다.

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mission" && isMission)
        {
            coll = collision.gameObject;

            btn.interactable = true;
        }

        if(collision.tag == "NPC" && !isMission && !isCool)
        {
            coll = collision.gameObject;

            btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mission")
        {
            coll = null;
            btn.interactable = false;
        }

        if (collision.tag == "NPC" && !isMission)
        {
            coll = null;

            btn.interactable = false;
        }
    }

    //버튼 누르면 호출
    public void ClickButton()
    {
        //미션일때
        if (isMission)
        {
            //MissionStart를 호출
            coll.SendMessage("MissionStart");
        }
        //킬 퀘스트일때
        else
        {
            Kill();
        }

        isCantMove = true; //캐릭터가 움직이면 안됨
        btn.interactable = false; //버튼을 이미 눌렀기 때문에 비활성화
    }

    void Kill()
    {
        //죽이는 애니메이션
        killCtrl_script.kill_anim.SetActive(true);
        isAnim = true;

        //죽은 이미지 변경
        coll.SendMessage("Dead");

        //죽은 NPC는 다시 죽일 수 없게
        coll.GetComponent<CircleCollider2D>().enabled = false;

    }

    //미션 종료하면 호출
    public void MissionEnd()
    {
        isCantMove = false; //캐릭터 다시 움직임

    }
}
