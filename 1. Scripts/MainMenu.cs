using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenu : MonoBehaviour
{
    public GameObject missionView,killView; //public으로 missionView라는 이름의 GameObject가져오기

    // 게임종료 버튼 누르면 호출
    //public으로 하는 이유: 함수를 호출하는게 이 스크립트 내에서 하는것이 아니고 외부에서 버튼을 누르면 호출될 것이기 때문
    public void ClickQuit()
    {
        //유니티 에디터
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //안드로이드
#else
Application.Quit();

#endif

    }

    //미션 버튼 누르면 호출
    public void ClickMission()
    {
        gameObject.SetActive(false); //Main Menu의 SetActive를 false
        missionView.SetActive(true); //미션화면의 SetActive를 true

        GameObject player = Instantiate(Resources.Load("Character"),new Vector3(0,-2,0),Quaternion.identity) as GameObject;  //Resources에 있는 Character를 복제해서 사용
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = missionView; //위의 코드에서 선언한 missionView
        //player.GetComponent<PlayerCtrl>().missionView => PlayerCtrl스크립트에서 받아온 missionView
        player.GetComponent<PlayerCtrl>().isMission = true;

        missionView.SendMessage("MissionReset");

    }

    //킬 버튼 누르면 호출
    public void ClickKill()
    {
        gameObject.SetActive(false); //Main Menu의 SetActive를 false
        killView.SetActive(true); //미션화면의 SetActive를 true

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0, -2, 0), Quaternion.identity) as GameObject;  //Resources에 있는 Character를 복제해서 사용
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = killView;
        player.GetComponent<PlayerCtrl>().isMission = false;


        killView.SendMessage("KillReset");

    }

}
