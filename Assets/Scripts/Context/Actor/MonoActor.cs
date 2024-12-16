using UnityEngine;

public class MonoActor : MonoBehaviour
{
    private SignalCenter signalCenter;

    public MonoActor()
    {
        ContextManager.Instance.MonoActorRegister(this);
    }
    
    public SignalCenter SignalCenter
    {
        get => signalCenter;
        set => signalCenter = value;
    }
}
