using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIActor
{
    void OnLoad();

    void OnOpen(object data);

    void OnOpen();

    void OnClose();
}
