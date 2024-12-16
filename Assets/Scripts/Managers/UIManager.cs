using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoActor
{
    private Dictionary<Type, UIActor> UIDictionary;
    private GameObject uiRoot;
    private Canvas canvas;

    private void Awake()
    {
        UIDictionary = new Dictionary<Type, UIActor>();
        uiRoot = Instantiate(Resources.Load<GameObject>(ResourcePath.UI_ROOT));
        canvas = uiRoot.GetComponentInChildren<Canvas>();
        
        RegisterSignals();
    }

    private void RegisterSignals()
    {
        SignalCenter.AddListener<Type, object>(ESignalType.OPEN_UI, OpenUI);
        SignalCenter.AddListener<Type>(ESignalType.CLOSE_UI, CloseUI);
    }

    private void OpenUI(Type type, object data)
    {
        Debug.Log($"Try Open UI {type}");
        UIActor ui;
        if (UIDictionary.TryGetValue(type, out var value))
        {
            ui = value;
        }
        else
        {
            ui = Activator.CreateInstance(type) as UIActor;
            ui.rootObj = Instantiate(Resources.Load<GameObject>(ui.UIPath), canvas.transform);
            ui.OnLoad();
            UIDictionary.Add(type, ui);
        }
        
        ui.rootObj.SetActive(true);
        ui.OnOpen(data);
    }

    private void CloseUI(Type type)
    {
        UIActor ui;
        if (UIDictionary.TryGetValue(type, out var value))
        {
            ui = value;
        }
        else
        {
            Debug.LogError($"UI {type} is not opened");
            return;
        }

        ui.OnClose();
        ui.rootObj.SetActive(false);
    }
}
