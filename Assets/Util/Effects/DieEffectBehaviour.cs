namespace Util.Effects
{
    public interface IDieEffectBehaviour
    {
        void OnActivate();
        void OnDeactivate();
        DieEffect GetDieEffect();
    }
}