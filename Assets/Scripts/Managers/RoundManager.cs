using System;
using UnityEngine;

public class RoundManager : MonoActor
{
    public ERoundState roundState = ERoundState.AFTER_ROUND;

    public void Awake()
    {
        RegisterSignals();
        ChangeRoundState(ERoundState.BEFORE_ROUND);
    }

    private void RegisterSignals()
    {
        SignalCenter.AddListener<ERoundState>(ESignalType.ROUND_STATE_CHANGE, ChangeRoundState);
    }

    private void ChangeRoundState(ERoundState state)
    {
        if(roundState == state) return;
        roundState = state;
        switch (state)
        {
            case ERoundState.BEFORE_ROUND:
                Debug.Log("----------------Before Round-----------------");
                SignalCenter.Dispatch<Type, object>(ESignalType.OPEN_UI, typeof(LobbyUIActor), null);
                break;
            case ERoundState.BOARD_EDITING://该阶段，跳转决定权在EditorBoard(需要检查棋盘可用）
                Debug.Log("----------------Board Editing-----------------");
                SignalCenter.Dispatch(ESignalType.EDIT_BOARD_START);
                break;
            case ERoundState.DRAW_LOTS:
                DrawLots();
                break;
            case ERoundState.PLAYER_TURN:
                Debug.Log("-------------------Player Turn-------------------");
                SignalCenter.Dispatch(ESignalType.PLAYER_TURN);
                break;
            case ERoundState.AI_TURN:
                Debug.Log("---------------------AI Turn---------------------");
                SignalCenter.Dispatch(ESignalType.AI_TURN);
                break;
            case ERoundState.PLAYER_WIN:
                PlayerWin();
                break;
            case ERoundState.AI_WIN:
                AIWin();
                break;
            case ERoundState.AFTER_ROUND:
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void DrawLots()
    {
        int result = UnityEngine.Random.Range(0, 2);
        SignalCenter.Dispatch(ESignalType.CLEAR_PIECES);
        if (result == 0)
        {
            ChangeRoundState(ERoundState.PLAYER_TURN);
        }
        else
        {
            ChangeRoundState(ERoundState.AI_TURN);
        }
        SignalCenter.Dispatch<Type, object>(ESignalType.OPEN_UI, typeof(InGameUIActor), null);
    }

    private void PlayerWin()
    {
        
    }

    private void AIWin()
    {
        
    }
}

public enum ERoundState
{
    BEFORE_ROUND,
    BOARD_EDITING,
    DRAW_LOTS,
    PLAYER_TURN,AI_TURN,
    PLAYER_WIN,AI_WIN,
    AFTER_ROUND
}
