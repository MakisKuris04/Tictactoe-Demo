using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EditorUIActor : UIActor
{
    public override string UIPath => ResourcePath.UI_EDIT_BOARD;

    private GameObject warningTip;
    private Transform tipGenerateTransform;
    public static ObjectPool<GameObject> tipPool;

    
    public override void OnLoad()
    {
        warningTip = rootObj.transform.Find("Text_WarningTip").gameObject;
        tipGenerateTransform = rootObj.transform.Find("TipGeneratePoint");
        tipPool = new ObjectPool<GameObject>(
            () => Object.Instantiate(warningTip),
            tip => tip.SetActive(true),
            tip => tip.SetActive(false),
            tip => Object.Destroy(tip),
            false, 5, 10);
        
        SignalCenter.AddListener(ESignalType.GENERATE_WARNING_TIP, GenerateWarningTip);
    }

    private void GenerateWarningTip()
    {
        if(tipPool.CountInactive < 0) return;
        var tip = tipPool.Get();
        tip.transform.SetParent(tipGenerateTransform);
        tip.transform.position = tipGenerateTransform.position;
        var tipComp = tip.GetComponent<TipFadeUp>();
        tipComp.OutPool();
    }
}
