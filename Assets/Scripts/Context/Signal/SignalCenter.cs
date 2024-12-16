using System;
using System.Collections.Generic;

public class SignalCenter
{
    private Dictionary<int, BaseSignal> signalMap = new Dictionary<int, BaseSignal>();

    #region Signal
    
    public Signal GetSignal(int type)
    {
        if (!signalMap.ContainsKey(type))
        {
            Signal signal = new Signal();
            signalMap[type] = signal;
        }
        
        return signalMap[type] as Signal;
    }

    public void Dispatch(int type)
    {
        GetSignal(type).Dispatch();
    }
    
    public void AddListener(int type, Action callback)
    {
        GetSignal(type).AddListener(callback);
    }

    public void AddListenerOnce(int type, Action callback)
    {
        GetSignal(type).AddOnce(callback);
    }

    public void RemoveListener(int type, Action callback)
    {
        GetSignal(type).RemoveListener(callback);
    }

    #endregion

    #region Signal<T>

    public Signal<T> GetSignal<T>(int type)
    {
        if (!signalMap.ContainsKey(type))
        {
            Signal<T> signal = new Signal<T>();
            signalMap[type] = signal;
        }
        
        return signalMap[type] as Signal<T>;
    }
    
    public void Dispatch<T>(int type, T param1)
    {
        
        GetSignal<T>(type).Dispatch(param1);
    }
    
    public void AddListener<T>(int type, Action<T> callback)
    {
        GetSignal<T>(type).AddListener(callback);
    }

    public void AddListenerOnce<T>(int type, Action<T> callback)
    {
        GetSignal<T>(type).AddOnce(callback);
    }

    public void RemoveListener<T>(int type, Action<T> callback)
    {
        GetSignal<T>(type).RemoveListener(callback);
    }

    #endregion
    
    #region Signal<T, U>

    public Signal<T, U> GetSignal<T, U>(int type)
    {
        if (!signalMap.ContainsKey(type))
        {
            Signal<T, U> signal = new Signal<T, U>();
            signalMap[type] = signal;
        }
        
        return signalMap[type] as Signal<T, U>;
    }

    public void Dispatch<T, U>(int type, T param1, U param2)
    {
        
        GetSignal<T, U>(type).Dispatch(param1, param2);
    }
    
    public void AddListener<T, U>(int type, Action<T, U> callback)
    {
        GetSignal<T, U>(type).AddListener(callback);
    }

    public void AddListenerOnce<T, U>(int type, Action<T, U> callback)
    {
        GetSignal<T, U>(type).AddOnce(callback);
    }

    public void RemoveListener<T, U>(int type, Action<T, U> callback)
    {
        GetSignal<T, U>(type).RemoveListener(callback);
    }

    #endregion
    
    #region Signal<T, U, V>

    public Signal<T, U, V> GetSignal<T, U, V>(int type)
    {
        if (!signalMap.ContainsKey(type))
        {
            Signal<T, U, V> signal = new Signal<T, U, V>();
            signalMap[type] = signal;
        }
        
        return signalMap[type] as Signal<T, U, V>;
    }

    public void Dispatch<T, U, V>(int type, T param1, U param2, V param3)
    {
        
        GetSignal<T, U, V>(type).Dispatch(param1, param2, param3);
    }
    
    public void AddListener<T, U, V>(int type, Action<T, U, V> callback)
    {
        GetSignal<T, U, V>(type).AddListener(callback);
    }

    public void AddListenerOnce<T, U, V>(int type, Action<T, U, V> callback)
    {
        GetSignal<T, U, V>(type).AddOnce(callback);
    }

    public void RemoveListener<T, U, V>(int type, Action<T, U, V> callback)
    {
        GetSignal<T, U, V>(type).RemoveListener(callback);
    }

    #endregion
    
    #region Signal<T, U, V, W>

    public Signal<T, U, V, W> GetSignal<T, U, V, W>(int type)
    {
        if (!signalMap.ContainsKey(type))
        {
            Signal<T, U, V, W> signal = new Signal<T, U, V, W>();
            signalMap[type] = signal;
        }
        
        return signalMap[type] as Signal<T, U, V, W>;
    }

    public void Dispatch<T, U, V, W>(int type, T param1, U param2, V param3, W param4)
    {
        
        GetSignal<T, U, V, W>(type).Dispatch(param1, param2, param3, param4);
    }
    
    public void AddListener<T, U, V, W>(int type, Action<T, U, V, W> callback)
    {
        GetSignal<T, U, V, W>(type).AddListener(callback);
    }

    public void AddListenerOnce<T, U, V, W>(int type, Action<T, U, V, W> callback)
    {
        GetSignal<T, U, V, W>(type).AddOnce(callback);
    }

    public void RemoveListener<T, U, V, W>(int type, Action<T, U, V, W> callback)
    {
        GetSignal<T, U, V, W>(type).RemoveListener(callback);
    }

    #endregion
}
