using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Effects;

public class FireTrapBehaviour : MonoBehaviour, IDieEffectObserver
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;

    private IKillable target;
    private bool isActive;

    void Start()
    {
        FindObjectOfType<EffectStateManager>().RegisterObserver(this);
    }

    private void TrapOn()
    {
        isActive = true;
        spriteRenderer.sprite = sprites[1];
        StartCoroutine(KillTarget());
    }

    private void TrapOff()
    {
        isActive = false;
        spriteRenderer.sprite = sprites[0];
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
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }

        target?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        target = col.gameObject.GetComponent<IKillable>();
        if (isActive)
            StartCoroutine(KillTarget());
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<IKillable>() == target)
            target = null;
    }
}