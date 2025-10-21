namespace Code.GameplayEffects
{
    public interface IEffectFactory
    {
        GameEntity CreateEffect(EffectConfig effectConfig, int producerId, int targetId);
    }
}