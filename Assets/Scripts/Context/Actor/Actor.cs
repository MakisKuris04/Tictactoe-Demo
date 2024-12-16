using UnityEngine;

public class Actor
{
    private SignalCenter signalCenter;

    protected Actor()
    {
        ContextManager.Instance.ActorRegister(this);
    }
    
    public SignalCenter SignalCenter
    {
        get => signalCenter;
        set => signalCenter = value;
    }
}
