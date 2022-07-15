using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Effects;
using Util.Effects.Impl;

public class EffectStateManager : MonoBehaviour
{
    private IDieEffectBehaviour _currentEffect = new NoneDieEffect();
    private readonly HashSet<IDieEffectObserver> _observers = new HashSet<IDieEffectObserver>();

    public void RegisterObserver(IDieEffectObserver observer)
    {
        _observers.Add(observer);
    }

    public void ChangeEffect(DieEffect dieEffect)
    {
        AlertEffectDeactivation(_currentEffect.GetDieEffect());
        _currentEffect.OnDeactivate();
        
        _currentEffect = CreateDieEffectBehaviour(dieEffect);
        Debug.Log(dieEffect);
        
        AlertEffectActivation(_currentEffect.GetDieEffect());
        _currentEffect.OnActivate();
    }

    private IDieEffectBehaviour CreateDieEffectBehaviour(DieEffect dieEffect)
    {
        switch (dieEffect)
        {
            case DieEffect.None:
                return new NoneDieEffect();
            case DieEffect.Electricity:
                return new ElectricityDieEffect();
            case DieEffect.Ice:
                return new IceDieEffect();
            case DieEffect.Fire:
                throw new NotImplementedException();
            case DieEffect.Wind:
                throw new NotImplementedException();
            case DieEffect.Earth:
                throw new NotImplementedException();
            case DieEffect.Light:
                throw new NotImplementedException();
            case DieEffect.Darkness:
                throw new NotImplementedException();
            default:
                throw new ArgumentOutOfRangeException(nameof(dieEffect), dieEffect, null);
        }
    }

    private void AlertEffectActivation(DieEffect dieEffect)
    {
        foreach (var observer in _observers)
        {
            observer.OnDieEffectActivation(dieEffect);
        }
    }
    
    private void AlertEffectDeactivation(DieEffect dieEffect)
    {
        foreach (var observer in _observers)
        {
            observer.OnDieEffectDeactivation(dieEffect);
        }
    }
}
