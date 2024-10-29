using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{ 
    public GameObject JoyStick,mainView,playView; //���ӿ�����Ʈ ���̽�ƽ,����,�̼� ������ (����,�̼��� MainMenu��ũ��Ʈ���� ��������)
    public Settings settings_script;  // Settings ��ũ��Ʈ ��������
    public Button btn;
    public Sprite use, kill;
    public Text text_cool; // ��Ÿ�� �ð� ǥ��

    Animator anim; //Animator ����
    GameObject coll;
    KillCtrl killCtrl_script;

    public float speed;

    public bool isCantMove, isMission;

    float timer;
    bool isCool, isAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Camera.main.transform.parent = transform; //ĳ������ transform
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        //�̼��̶��
        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;

            text_cool.text = ""; // �̼��̶�� ��Ÿ�� �ʿ����
        }
        // ų ����Ʈ���
        else
        {
            killCtrl_script = FindObjectOfType<KillCtrl>();
            btn.GetComponent<Image>().sprite = kill;

            timer = 5;
            isCool = true;
        }
    }

    //ȭ���� Ŭ���ߴ��� �Ǵ��ϴ� �� -> �������Ӹ��� Ȯ��
    private void Update()
    {
        //��Ÿ��
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString(); //�Ҽ������� ���� �ø����� ���

            if(text_cool.text == "0") //������ �����ʰ� 0�� �Ǹ� ""���� �ƹ��͵� ��µ��� �ʰ� ��
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

        //�ִϸ��̼��� �����ٸ�
        if (isAnim && killCtrl_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killCtrl_script.kill_anim.SetActive(false);
            killCtrl_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    
    }

    //ĳ���� ������ ���� : ȭ���� �����¿츦 Ŭ���ؼ� ĳ���͸� �̵���Ű�� ���
    void Move()
    {
        if (settings_script.isJoyStick)
        {
            JoyStick.SetActive(true); //���̽�ƽ ���̰�
        }
        else
        {
            JoyStick.SetActive(false); //���̽�ƽ �Ⱥ��̰�

            //Ŭ���ߴ��� �Ǵ�
            if (Input.GetMouseButton(0))   
            {
#if UNITY_EDITOR
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //Ŭ���� ������ -ȭ�� ���߾� �� ���⺤�� ��ǥ
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    //ĳ���� �̵�(���ϴ� �ӵ��� �̵�) Time.deltaTime: ���� �ð�
                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true); //�ִϸ��̼� ���� �οﰪ

                    //�������� �̵�
                    if (dir.x < 0)   //dir.x�� ���� ���� ��ǥ(x,y,z)�� x��ǥ
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    //���������� �̵�
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
#else
if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                //Input.GetTouch(0): Input.GetMouseButtonó�� ��ġ�� �Է��� �Ǵ��� Ȯ���ϴ� �κ�
                //���콺�� 1������ ��ġ�� �������� �Է� �� �� �ֱ� ������ �� �� 0��° ��ġ ���̵� �Է��Ѵٴ� ��(����Ƽ �����Ϳ��� �۵� x)
                {
                    //Ŭ���� ������ -ȭ�� ���߾� �� ���⺤�� ��ǥ
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    //ĳ���� �̵�(���ϴ� �ӵ��� �̵�) Time.deltaTime: ���� �ð�
                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true); //�ִϸ��̼� ���� �οﰪ

                    //�������� �̵�
                    if (dir.x < 0)   //dir.x�� ���� ���� ��ǥ(x,y,z)�� x��ǥ
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    //���������� �̵�
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
#endif

            }
            else //Ŭ������ �ʾ��� ��
            {
                anim.SetBool("isWalk", false); //�ִϸ��̼� ���� �οﰪ
            }
        }
    }

    //ĳ���� ����
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null; //ī�޸��� �θ� ĳ���ͷ� �Ǿ� �־��� ������ ĳ���Ͱ� �����Ǹ� null�� �������ش�.

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

    //��ư ������ ȣ��
    public void ClickButton()
    {
        //�̼��϶�
        if (isMission)
        {
            //MissionStart�� ȣ��
            coll.SendMessage("MissionStart");
        }
        //ų ����Ʈ�϶�
        else
        {
            Kill();
        }

        isCantMove = true; //ĳ���Ͱ� �����̸� �ȵ�
        btn.interactable = false; //��ư�� �̹� ������ ������ ��Ȱ��ȭ
    }

    void Kill()
    {
        //���̴� �ִϸ��̼�
        killCtrl_script.kill_anim.SetActive(true);
        isAnim = true;

        //���� �̹��� ����
        coll.SendMessage("Dead");

        //���� NPC�� �ٽ� ���� �� ����
        coll.GetComponent<CircleCollider2D>().enabled = false;

    }

    //�̼� �����ϸ� ȣ��
    public void MissionEnd()
    {
        isCantMove = false; //ĳ���� �ٽ� ������

    }
}
