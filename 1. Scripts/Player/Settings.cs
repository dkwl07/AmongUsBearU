using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    public bool isJoyStick; //public이유: inspactor창에서 눈으로 확인하기 위해
    public Image touchBtn, joyStickBtn;
    public Color blue;
    public PlayerCtrl playerCtrl_script;

    GameObject mainView, playView;

    private void Start()
    {
        mainView = playerCtrl_script.mainView;
        playView = playerCtrl_script.playView;
    }

    //설정 버튼을 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove = true;
    }

    //게임으로 돌아가기 버튼을 누르면 호출
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerCtrl_script.isCantMove = false;
    }

    //터치이동 누르면 호출
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        joyStickBtn.color = Color.white;
    }

    //조이스틱 누르면 호출
    public void ClickJoyStick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white;
        joyStickBtn.color = blue;
    }

    //게임 나가기를 누르면 호출
    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        //캐릭터 삭제
        playerCtrl_script.DestroyPlayer();
    }

}
