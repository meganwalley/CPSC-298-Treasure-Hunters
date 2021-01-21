using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float time = 2F;
    private bool completed = false;
    private float currenttime = 0F;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (completed)
            return;

        if (currenttime >= time)
        {
            completed = true;
        } else
        {
            currenttime += Time.deltaTime;
        }
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public void ChangeMaxTime(float toAdd)
    {
        time += toAdd;
    }

    public float GetCurrentTime()
    {
        return currenttime;
    }
}
