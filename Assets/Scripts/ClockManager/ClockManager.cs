using System;
using System.Collections;
using System.Collections.Generic;
using Score;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util.Clock;

public class ClockManager : MonoBehaviour
{
    private readonly List<IClockBehaviour> _observers = new List<IClockBehaviour>();
    private int ticks = 0;

    public void Tick()
    {
        ticks++;
        foreach (var observer in _observers)
        {
            observer.OnClockTick();
        }
        foreach (var observer in _observers)
        {
            observer.OnLateClockTick();
        }
    }

    public void RegisterObserver(IClockBehaviour observer)
    {
        _observers.Add(observer);
    }

    public void UnRegisterObserver(IClockBehaviour observer)
    {
        _observers.Remove(observer);
    }

    public void SaveScore()
    {
        string level = SceneManager.GetActiveScene().name;
        FindObjectOfType<ScoreTracker>().SetLevelSteps(level, ticks);
    }
}
