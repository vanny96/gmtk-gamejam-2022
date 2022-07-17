using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Effects;

public class FireTrapBehaviour : KillerTrap, IDieEffectObserver
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;

    private bool _isActive;

    void Awake()
    {
        FindObjectOfType<EffectStateManager>().RegisterObserver(this);
    }

    private void TrapOn()
    {
        _isActive = true;
        spriteRenderer.sprite = sprites[1];
        KillTarget();
    }

    private void TrapOff()
    {
        _isActive = false;
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

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if(_isActive)
            KillTarget();
    }
}