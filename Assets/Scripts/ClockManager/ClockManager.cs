using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Clock;

public class ClockManager : MonoBehaviour
{
    private readonly List<IClockBehaviour> _observers = new List<IClockBehaviour>();

    public void Tick()
    {
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
}
