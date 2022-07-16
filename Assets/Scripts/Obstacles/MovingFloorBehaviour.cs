using UnityEngine;
using Util;
using Util.Clock;
using Util.Effects;

namespace Obstacles
{
    public class MovingFloorBehaviour: MonoBehaviour, IDieEffectObserver, IClockBehaviour
    {
        [SerializeField] private int ticksToStopMoving;

        private int _remainingTicksToStop = 0;
        
        private void StartMoving()
        {
            throw new System.NotImplementedException();
        }
        
        private void StopMoving()
        {
            throw new System.NotImplementedException();
        }
        
        public void OnDieEffectActivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                StartMoving();
            }
        }

        public void OnDieEffectDeactivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                _remainingTicksToStop = ticksToStopMoving;
            }
        }

        public void OnClockTick()
        {
            if (_remainingTicksToStop > 0)
            {
                _remainingTicksToStop--;
                if (_remainingTicksToStop == 0)
                    StopMoving();
            }
        }
    }
}