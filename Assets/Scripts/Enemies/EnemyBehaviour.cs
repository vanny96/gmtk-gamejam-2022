using System;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Clock;
using Util.Effects;

namespace Enemies
{
    public class EnemyBehaviour: MonoBehaviour, IDieEffectObserver, IClockBehaviour, IKillable
    {
        [SerializeField] private ClockItineraryBehaviour clockItineraryBehaviour;
        [SerializeField] private int ticksToUnfreeze;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> sprites;

        private bool melting;
        private int _ticksRemainingToUnfreeze = 0; 
        private bool _destroyOnNextTick;

        void Awake()
        {
            FindObjectOfType<EffectStateManager>().RegisterObserver(this);
            FindObjectOfType<ClockManager>().RegisterObserver(this);
        }
        
        private void OnDestroy()
        {
            FindObjectOfType<ClockManager>()?.UnRegisterObserver(this);
            FindObjectOfType<EffectStateManager>()?.UnRegisterObserver(this);
        }

        public void OnDieEffectActivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Ice)
            {
                Freeze();
            }
        }

        public void OnDieEffectDeactivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Ice)
            {
                melting = true;
                _ticksRemainingToUnfreeze = ticksToUnfreeze;
            }
        }

        private void Freeze()
        {
            clockItineraryBehaviour.active = false;
            spriteRenderer.sprite = sprites[1];
            melting = false;
        }
        
        private void Unfreeze()
        {
            clockItineraryBehaviour.active = true;
            spriteRenderer.sprite = sprites[0];
        }

        public void OnClockTick()
        {
            if (melting && _ticksRemainingToUnfreeze > 0)
            {
                _ticksRemainingToUnfreeze--;
                if(_ticksRemainingToUnfreeze == 0)
                    Unfreeze();
            }

            if (_destroyOnNextTick)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            IKillable killable = col.gameObject.GetComponent<IKillable>();
            if(killable != null)
                killable.Kill();
        }

        public void Kill()
        {
            Destroy(clockItineraryBehaviour);
            Destroy(gameObject.GetComponent<Collider2D>());
            spriteRenderer.sprite = sprites[2];
            _destroyOnNextTick = true;
        }
    }
}