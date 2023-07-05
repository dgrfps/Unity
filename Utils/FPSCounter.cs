using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [Header("FPS Counter")]
    [SerializeField] float updateInterval = 0.5f;
    [SerializeField] List<float> oldvalues = new List<float>();
    
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI t_fps;
    
    void Update()
    {
        time += Time.deltaTime;
        oldvalues.Add(1 / Time.deltaTime);

        if (time >= updateInterval)
        {
            time = 0;

            float avg = 0;
            for (int i = 0; i < oldvalues.Count; i++)
                avg += oldvalues[i];

            t_fps.text = "FPS:" + (avg / oldvalues.Count).ToString("F0");
            oldvalues.Clear();
        }

    }

    float time;
}
