using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission6 : MonoBehaviour
{
    public bool[] isColor = new bool[4]; //4���� ���� ������ 4���� bool�� ����
    public RectTransform[] rights; // ������ ������ ��ġ
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
        //�巡��
        if (isDrag)
        {
            //�������� ������ �� 0�� 1���� 1�� ����                          1920f : Ÿ�� ���۷����� x�κ�(float��),Screen.width: ���� �ػ��� ����
            line.SetPosition(1, new Vector3((Input.mousePosition.x - clickPos.x)*1920f/Screen.width, (Input.mousePosition.y - clickPos.y)*1080f/Screen.height,-10)); //�������� �������� �� �ִ�.

            //�巡�� ������ ��
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition); //ȭ�鿡�� ���콺������ ��ġ�� Raycast�� ���
                //Raycast? ������ ���� ��

                RaycastHit hit;

                //Raycast�� ���� ���� ���� �ִٴ� ��(������ ���� �¾Ҵٸ�)
                if (Physics.Raycast(ray,out hit))
                {
                    GameObject rightline = hit.transform.gameObject; //hit�� ����� ���� ��ü�� ����
                    rightY = rightline.GetComponent<RectTransform>().anchoredPosition.y; //������ �� y��
                    //������ �� ����
                    rightC = rightline.GetComponent<Image>().color;

                    line.SetPosition(1, new Vector3(500, rightY - leftY, -10));

                    //�� ��
                    if(leftC == rightC) //�¾�����
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = true; break;
                            case 75: isColor[1] = true; break;
                            case -75: isColor[2] = true; break;
                            case -255: isColor[3] = true; break;
                        }
                    }
                    else //Ʋ���� ��
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = false; break;
                            case 75: isColor[1] = false; break;
                            case -75: isColor[2] = false; break;
                            case -255: isColor[3] = false; break;
                        }
                    }

                    //�������� üũ
                    if(isColor[0]&& isColor[1]&& isColor[2]&& isColor[3])
                    {
                        Invoke("MissionSuccess", 0.2f);
                    }
                }
                //�����ʾҴٸ�
                else
                {
                    line.SetPosition(1, new Vector3(0, 0, -10)); //����ġ
                }

                isDrag = false;
            }
        }       
    }

    //�̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        //�ʱ�ȭ
        for(int i = 0; i < 4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        //����
        for(int i = 0; i < rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0, 4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;

            rights[rand].anchoredPosition = temp;
        }

    }

    //���� ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    //�� ������ ȣ��
    public void ClickLine(LineRenderer click)
    {
        clickPos = Input.mousePosition; // ���콺 ������ ���Ͱ� ����
        line = click; //LineRenderer ��������


        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y; //Ŭ���� ���� ���� y��

        //���� �� ����
        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true; //�巡�װ� ���۵ƴٴ� �� �˷���
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
