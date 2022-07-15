using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Effects;

public class FireTrapBehaviour : MonoBehaviour, IDieEffectObserver
{
    void Start()
    {
        FindObjectOfType<EffectStateManager>().RegisterObserver(this);
    }

    private void TrapOn()
    {
        Debug.Log("Trap On!");
    }

    private void TrapOff()
    {
        Debug.Log("Trap Off!");
    }
    
    public void OnDieEffectActivation(DieEffect dieEffect)
    {
        if (dieEffect == DieEffect.Fire)
        {
            TrapOn();
        }
    }

    public void OnDieEffectDeactivation(DieEffect dieEffect)
    {
        if (dieEffect == DieEffect.Fire)
        {
            TrapOff();
        }
    }
}
