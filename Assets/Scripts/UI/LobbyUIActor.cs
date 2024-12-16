using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIActor : UIActor
{
    public override string UIPath => ResourcePath.UI_LOBBY;

    private Button startGameBtn;

    public override void OnLoad()
    {
        startGameBtn = rootObj.transform.Find("Btn_StartGame").GetComponent<Button>();
        startGameBtn.onClick.AddListener(() =>
        {
            SignalCenter.Dispatch<ERoundState>(ESignalType.ROUND_STATE_CHANGE, ERoundState.BOARD_EDITING);
            SignalCenter.Dispatch<Type>(ESignalType.CLOSE_UI, typeof(LobbyUIActor));
        });
    }
}
