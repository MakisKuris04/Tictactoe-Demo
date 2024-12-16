using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActor : Actor, IUIActor
{
    public virtual string UIPath { get; }

    public GameObject rootObj;

    public virtual void OnLoad(){}
    public virtual void OnOpen(object data)
    {
        OnOpen();
    }

    public virtual void OnOpen(){}
    
    public virtual void OnClose(){}
}
