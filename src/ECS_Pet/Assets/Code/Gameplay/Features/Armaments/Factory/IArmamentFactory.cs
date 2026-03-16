using Code.GameplayEffects;
using System.Collections.Generic;

namespace Code.Gameplay.Armaments.Factory
{
    public interface IArmamentFactory
    {
        GameEntity CreateProjectile(GameEntity owner, GameEntity ability);
    }
}