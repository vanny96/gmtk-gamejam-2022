using UnityEngine;
using Util;
using Util.Effects;

namespace Obstacles
{
    public class DoorBehaviour : MonoBehaviour, IDieEffectObserver
    {
        public void OnDieEffectActivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                //cambia sprite
                //disabilita il collider
            }
        }

        public void OnDieEffectDeactivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                //cambia sprite
                //abilita il collider
            }
        }
    }
}