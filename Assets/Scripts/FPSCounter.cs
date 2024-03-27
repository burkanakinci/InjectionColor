using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField]private float time = 0.0f;
    [SerializeField]private int frames = 0;
    private float fps;

    private void Start()
    {
        time = 0.0f;
        frames = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        ++frames;
        fps = frames / time;

        txt.text = "FPS : " + ((int)fps).ToString();
        if (time >= 30.0f)
        {
            time = 0.0f;
            frames = 0;
        }
    }
}