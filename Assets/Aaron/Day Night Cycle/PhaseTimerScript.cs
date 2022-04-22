using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseTimerScript : MonoBehaviour
{
    public event Action PhaseEndEvent;

    public GameObject sunMoon;
    
    private Transform p1Sunrise;
    private Transform p4Sunset;
    private Transform p2;
    private Transform p3;
    private Vector2 point;

    private float timeElapsed;
    public float phaseLength;
    
    void Update()
    {
        if (timeElapsed < phaseLength)
        { 
            Vector2 lerpA = Vector2.Lerp(p1Sunrise.position, p2.position, (timeElapsed/phaseLength));
            Vector2 lerpB = Vector2.Lerp(p2.position, p3.position, (timeElapsed/phaseLength));
            Vector2 lerpC = Vector2.Lerp(p3.position, p4Sunset.position, (timeElapsed/phaseLength));
            Vector2 lerpD = Vector2.Lerp(lerpA, lerpB, (timeElapsed/phaseLength));
            Vector2 lerpE = Vector2.Lerp(lerpB, lerpC, (timeElapsed/phaseLength));
    
            point = Vector2.Lerp(lerpD, lerpE, (timeElapsed/phaseLength));
            sunMoon.transform.position = point;

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= phaseLength)
            {
                PhaseEndEvent?.Invoke();
            }
        }
    }
}
