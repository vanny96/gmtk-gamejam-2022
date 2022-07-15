namespace Util.Effects
{
    public interface IDieEffectObserver
    {
        void OnDieEffectActivation(DieEffect dieEffect);
        void OnDieEffectDeactivation(DieEffect dieEffect);
    }
}