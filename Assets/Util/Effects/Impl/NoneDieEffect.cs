namespace Util.Effects.Impl
{
    public class NoneDieEffect : IDieEffectBehaviour
    {
        public void OnActivate()
        {
            //Do nothing
        }

        public void OnDeactivate()
        {
            //Do nothing
        }

        public DieEffect GetDieEffect()
        {
            return DieEffect.None;
        }
    }
}