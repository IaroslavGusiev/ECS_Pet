using Code.StaticData;
using UnityEngine;

namespace Code.Gameplay.Monster
{
    public interface IMonsterFactory
    {
        GameEntity CreateMonster(MonsterTypeId monsterTypeId, Vector3 at);
    }
}