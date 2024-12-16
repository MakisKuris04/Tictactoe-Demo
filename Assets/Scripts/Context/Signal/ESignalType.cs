using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESignalType
{
    //Round
    ROUND_STATE_CHANGE,

    //Editor
    EDIT_BOARD_START,
    GENERATE_WARNING_TIP,

    //Board
    GENERATE_BOARD,
    SET_PIECE,
    AI_TURN,
    PLAYER_TURN,
    CLEAR_BOARD,
    CLEAR_PIECES,
    
    //UI
    OPEN_UI,
    CLOSE_UI,
}

public static class SignalCenterExtensions
{
    public static Signal GetSignal(this SignalCenter signalCenter, ESignalType type)
    {
        return signalCenter.GetSignal((int)type);
    }

    public static void Dispatch(this SignalCenter signalCenter, ESignalType type)
    {
        signalCenter.Dispatch((int)type);
    }

    public static void AddListener(this SignalCenter signalCenter, ESignalType type, Action callback)
    {
        signalCenter.AddListener((int)type, callback);
    }

    public static void AddListenerOnce(this SignalCenter signalCenter, ESignalType type, Action callback)
    {
        signalCenter.AddListenerOnce((int)type, callback);
    }

    public static void RemoveListener(this SignalCenter signalCenter, ESignalType type, Action callback)
    {
        signalCenter.RemoveListener((int)type, callback);
    }

    public static Signal<T> GetSignal<T>(this SignalCenter signalCenter, ESignalType type)
    {
        return signalCenter.GetSignal<T>((int)type);
    }

    public static void Dispatch<T>(this SignalCenter signalCenter, ESignalType type, T param1)
    {
        signalCenter.Dispatch<T>((int)type, param1);
    }

    public static void AddListener<T>(this SignalCenter signalCenter, ESignalType type, Action<T> callback)
    {
        signalCenter.AddListener<T>((int)type, callback);
    }

    public static void AddListenerOnce<T>(this SignalCenter signalCenter, ESignalType type, Action<T> callback)
    {
        signalCenter.AddListenerOnce<T>((int)type, callback);
    }

    public static void RemoveListener<T>(this SignalCenter signalCenter, ESignalType type, Action<T> callback)
    {
        signalCenter.RemoveListener<T>((int)type, callback);
    }

    public static Signal<T, U> GetSignal<T, U>(this SignalCenter signalCenter, ESignalType type)
    {
        return signalCenter.GetSignal<T, U>((int)type);
    }

    public static void Dispatch<T, U>(this SignalCenter signalCenter, ESignalType type, T param1, U param2)
    {
        signalCenter.Dispatch<T, U>((int)type, param1, param2);
    }

    public static void AddListener<T, U>(this SignalCenter signalCenter, ESignalType type, Action<T, U> callback)
    {
        signalCenter.AddListener<T, U>((int)type, callback);
    }

    public static void AddListenerOnce<T, U>(this SignalCenter signalCenter, ESignalType type, Action<T, U> callback)
    {
        signalCenter.AddListenerOnce<T, U>((int)type, callback);
    }

    public static void RemoveListener<T, U>(this SignalCenter signalCenter, ESignalType type, Action<T, U> callback)
    {
        signalCenter.RemoveListener<T, U>((int)type, callback);
    }

    public static Signal<T, U, V> GetSignal<T, U, V>(this SignalCenter signalCenter, ESignalType type)
    {
        return signalCenter.GetSignal<T, U, V>((int)type);
    }

    public static void Dispatch<T, U, V>(this SignalCenter signalCenter, ESignalType type, T param1, U param2, V param3)
    {
        signalCenter.Dispatch<T, U, V>((int)type, param1, param2, param3);
    }

    public static void AddListener<T, U, V>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V> callback)
    {
        signalCenter.AddListener<T, U, V>((int)type, callback);
    }

    public static void AddListenerOnce<T, U, V>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V> callback)
    {
        signalCenter.AddListenerOnce<T, U, V>((int)type, callback);
    }

    public static void RemoveListener<T, U, V>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V> callback)
    {
        signalCenter.RemoveListener<T, U, V>((int)type, callback);
    }

    public static Signal<T, U, V, W> GetSignal<T, U, V, W>(this SignalCenter signalCenter, ESignalType type)
    {
        return signalCenter.GetSignal<T, U, V, W>((int)type);
    }

    public static void Dispatch<T, U, V, W>(this SignalCenter signalCenter, ESignalType type, T param1, U param2, V param3, W param4)
    {
        signalCenter.Dispatch<T, U, V, W>((int)type, param1, param2, param3, param4);
    }

    public static void AddListener<T, U, V, W>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V, W> callback)
    {
        signalCenter.AddListener<T, U, V, W>((int)type, callback);
    }

    public static void AddListenerOnce<T, U, V, W>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V, W> callback)
    {
        signalCenter.AddListenerOnce<T, U, V, W>((int)type, callback);
    }

    public static void RemoveListener<T, U, V, W>(this SignalCenter signalCenter, ESignalType type, Action<T, U, V, W> callback)
    {
        signalCenter.RemoveListener<T, U, V, W>((int)type, callback);
    }
}
