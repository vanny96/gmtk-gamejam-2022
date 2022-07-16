using System;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Util.Clock;
using Util.Effects;

namespace Obstacles
{
    public class DoorBehaviour : MonoBehaviour, IDieEffectObserver, IClockBehaviour
    {
        [SerializeField] private Collider2D doorCollider;
        [SerializeField] private int ticksToCloseDoor;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<Sprite> sprites;

        private int _remainingTicksToClose = 0;

        void Start()
        {
            FindObjectOfType<EffectStateManager>().RegisterObserver(this);
            FindObjectOfType<ClockManager>().RegisterObserver(this);
        }

        private void OpenDoor()
        {
            spriteRenderer.sprite = sprites[1];
            doorCollider.enabled = false;
        }
        
        private void CloseDoor()
        {
            spriteRenderer.sprite = sprites[0];
            doorCollider.enabled = true;
        }
        
        public void OnDieEffectActivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                OpenDoor();
            }
        }

        public void OnDieEffectDeactivation(DieEffect dieEffect)
        {
            if (dieEffect == DieEffect.Electricity)
            {
                _remainingTicksToClose = ticksToCloseDoor;
            }
        }

        public void OnClockTick()
        {
            if (_remainingTicksToClose > 0)
            {
                _remainingTicksToClose--;
                if (_remainingTicksToClose == 0)
                {
                    CloseDoor();
                }
            }
        }
    }
}