namespace Util.Effects.Impl
{
    public class FireDieEffect : IDieEffectBehaviour
    {
        public void OnActivate()
        {
            //Do something maybe
        }

        public void OnDeactivate()
        {
            //Do something maybe
        }

        public DieEffect GetDieEffect()
        {
            return DieEffect.Fire;
        }
    }
}