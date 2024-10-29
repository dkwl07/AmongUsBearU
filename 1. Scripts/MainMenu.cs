using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public GameObject missionView,killView; //public���� missionView��� �̸��� GameObject��������

    // �������� ��ư ������ ȣ��
    //public���� �ϴ� ����: �Լ��� ȣ���ϴ°� �� ��ũ��Ʈ ������ �ϴ°��� �ƴϰ� �ܺο��� ��ư�� ������ ȣ��� ���̱� ����
    public void ClickQuit()
    {
        //����Ƽ ������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //�ȵ���̵�
#else
Application.Quit();

#endif

    }

    //�̼� ��ư ������ ȣ��
    public void ClickMission()
    {
        gameObject.SetActive(false); //Main Menu�� SetActive�� false
        missionView.SetActive(true); //�̼�ȭ���� SetActive�� true

        GameObject player = Instantiate(Resources.Load("Character"),new Vector3(0,-2,0),Quaternion.identity) as GameObject;  //Resources�� �ִ� Character�� �����ؼ� ���
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = missionView; //���� �ڵ忡�� ������ missionView
        //player.GetComponent<PlayerCtrl>().missionView => PlayerCtrl��ũ��Ʈ���� �޾ƿ� missionView
        player.GetComponent<PlayerCtrl>().isMission = true;

        missionView.SendMessage("MissionReset");

    }

    //ų ��ư ������ ȣ��
    public void ClickKill()
    {
        gameObject.SetActive(false); //Main Menu�� SetActive�� false
        killView.SetActive(true); //�̼�ȭ���� SetActive�� true

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0, -2, 0), Quaternion.identity) as GameObject;  //Resources�� �ִ� Character�� �����ؼ� ���
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = killView;
        player.GetComponent<PlayerCtrl>().isMission = false;


        killView.SendMessage("KillReset");

    }

}
