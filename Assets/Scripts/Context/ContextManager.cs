using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextManager : MonoBehaviour
{
    public static ContextManager Instance { get; private set; }
    private bool isInitialized;
    private SignalCenter signalCenter;

    private ContextManager()
    {
        Instance = this;
        signalCenter = new SignalCenter();
        isInitialized = true;
    }

    public void ActorRegister(Actor actor)
    {
        if (!isInitialized) return;
        actor.SignalCenter = signalCenter;
    }
    
    public void MonoActorRegister(MonoActor actor)
    {
        if (!isInitialized) return;
        actor.SignalCenter = signalCenter;
    }
}
