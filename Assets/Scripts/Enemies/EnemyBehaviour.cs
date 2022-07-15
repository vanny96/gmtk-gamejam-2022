using System;
using UnityEngine;
using Util;
using Util.Clock;
using Util.Effects;

namespace Enemies
{
    public class EnemyBehaviour: MonoBehaviour, IDieEffectObserver, IClockBehaviour
    {
        [SerializeField] private ClockItineraryBehaviour clockItineraryBehaviour;
        [SerializeField] private int ticksToUnfreeze;

        private int _ticksRemainingToUnfreeze = 0;

        void Start()
        {
            FindObjectOfType<EffectStateManager>().RegisterObserver(this);
            FindObjectOfType<ClockManager>().RegisterObserver(this);
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
                _ticksRemainingToUnfreeze = ticksToUnfreeze;
            }
        }

        private void Freeze()
        {
            clockItineraryBehaviour.active = false;
        }
        
        private void Unfreeze()
        {
            clockItineraryBehaviour.active = true;
        }

        public void OnClockTick()
        {
            if (_ticksRemainingToUnfreeze > 0)
            {
                _ticksRemainingToUnfreeze--;
                if(_ticksRemainingToUnfreeze == 0)
                    Unfreeze();
            }
        }
    }
}