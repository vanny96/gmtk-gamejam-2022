using System.Collections;
using UnityEngine;
using Util;
using Util.Effects;

public class FireTrapBehaviour : MonoBehaviour, IDieEffectObserver
{
    private IKillable target;
    private bool isActive;

    void Start()
    {
        FindObjectOfType<EffectStateManager>().RegisterObserver(this);
    }

    private void TrapOn()
    {
        isActive = true;
        StartCoroutine(KillTarget());
    }

    private void TrapOff()
    {
        isActive = false;
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

    private IEnumerator KillTarget()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }
        target?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        target = col.gameObject.GetComponent<IKillable>();
        if(isActive)
            StartCoroutine(KillTarget());
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        target = null;
    }
}