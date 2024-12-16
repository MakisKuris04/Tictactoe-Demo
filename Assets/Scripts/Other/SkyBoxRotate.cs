using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class RotateSkyBox : MonoBehaviour
{
    public float RotateSpeed = 10.2f;

    private void Awake()
    {
        RenderSettings.skybox.SetVector("_RotationAxis", new Vector4(0, 1, 0, 0));
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*RotateSpeed);
    }
 
}