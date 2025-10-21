using UnityEngine;
using Code.StaticData;

namespace Code.Gameplay.Fighter
{
    public interface IFighterFactory
    {
        GameEntity CreateFighter(FighterTypeId fighterTypeId, Vector3 at);
    }
}