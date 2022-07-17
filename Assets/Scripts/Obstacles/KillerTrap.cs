using System.Collections;
using UnityEngine;

namespace Util
{
    public abstract class KillerTrap: MonoBehaviour
    {
        protected IKillable Target;

        protected void KillTarget()
        {
            StartCoroutine(KillTargetCoroutine());
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            Target = col.gameObject.GetComponent<IKillable>();
        }

        protected virtual void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<IKillable>() == Target)
                Target = null;
        }

        private IEnumerator KillTargetCoroutine()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return null;
            }

            Target?.Kill();
        }
    }
}