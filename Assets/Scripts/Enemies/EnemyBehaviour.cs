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

        private bool melting;
        private int _ticksRemainingToUnfreeze = 0;

        void Awake()
        {
            FindObjectOfType<EffectStateManager>().RegisterObserver(this);
            FindObjectOfType<ClockManager>().RegisterObserver(this);
        }
        
        private void OnDestroy()
        {
            FindObjectOfType<ClockManager>()?.UnRegisterObserver(this);
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
            melting = false;
        }
        
        private void Unfreeze()
        {
            clockItineraryBehaviour.active = true;
        }

        public void OnClockTick()
        {
            if (melting && _ticksRemainingToUnfreeze > 0)
            {
                _ticksRemainingToUnfreeze--;
                if(_ticksRemainingToUnfreeze == 0)
                    Unfreeze();
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
            //Change sprite
            Destroy(this);
        }
    }
}