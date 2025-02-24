using Code.StaticData;
using UnityEngine;

namespace Code.Gameplay.Fighter
{
    public interface IFighterFactory
    {
        GameEntity CreateFighter(FighterTypeId fighterTypeId, Vector3 at);
    }
}