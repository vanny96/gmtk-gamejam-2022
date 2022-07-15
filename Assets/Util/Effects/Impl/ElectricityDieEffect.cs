namespace Util.Effects.Impl
{
    public class ElectricityDieEffect: IDieEffectBehaviour
    {
        public void OnActivate()
        {
        }

        public void OnDeactivate()
        {
        }

        public DieEffect GetDieEffect()
        {
            return DieEffect.Electricity;
        }
    }
}