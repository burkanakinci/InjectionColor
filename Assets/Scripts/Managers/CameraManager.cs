using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class CameraManager : CustomBehaviour
{
    [SerializeField] private Camera m_CurrentCamera;

    public Camera CurrentCamera => m_CurrentCamera;
    public override void Initialize()
    {
    }
}
