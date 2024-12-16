using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndUIActor : UIActor
{
    public override string UIPath => ResourcePath.UI_ROUND_END;
    
    private GameObject playerWinObj;
    private GameObject aiWinObj;
    private GameObject drawObj;
    
    private Button editBtn;
    private Button repeatBtn;
    private Button quitBtn;

    public override void OnLoad()
    {
        UIElementsInit();
    }

    public override void OnOpen(object data)
    {
        base.OnOpen(data);
        if (data == null)
        {
            drawObj.SetActive(true);
        }
        else
        {
            var roundRes = (EPieceType)data;
            switch (roundRes)
            {
                case EPieceType.PIECE_AI:
                    aiWinObj.SetActive(true);
                    break;
                case EPieceType.PIECE_PLAYER:
                    playerWinObj.SetActive(true);
                    break;
            }
        }
    }

    public override void OnClose()
    {
        rootObj.SetActive(false);
        playerWinObj.SetActive(false);
        aiWinObj.SetActive(false);
        drawObj.SetActive(false);
    }

    private void UIElementsInit()
    {
        playerWinObj = rootObj.transform.Find("Text_PlayerWin").gameObject;
        playerWinObj.SetActive(false);
        aiWinObj = rootObj.transform.Find("Text_AIWin").gameObject;
        aiWinObj.SetActive(false);
        drawObj = rootObj.transform.Find("Text_Draw").gameObject;
        drawObj.SetActive(false);

        editBtn = rootObj.transform.Find("Btns/Btn_Edit").GetComponent<Button>();
        editBtn.onClick.AddListener(() =>
        {
            SignalCenter.Dispatch<ERoundState>(ESignalType.ROUND_STATE_CHANGE, ERoundState.BOARD_EDITING); 
            OnClose();
        });
        
        repeatBtn = rootObj.transform.Find("Btns/Btn_Repeat").GetComponent<Button>();
        repeatBtn.onClick.AddListener(() =>
        {
            SignalCenter.Dispatch<ERoundState>(ESignalType.ROUND_STATE_CHANGE, ERoundState.DRAW_LOTS);
            OnClose();
        });
        
        quitBtn = rootObj.transform.Find("Btns/Btn_Quit").GetComponent<Button>();
        quitBtn.onClick.AddListener(Application.Quit);
    }
}
