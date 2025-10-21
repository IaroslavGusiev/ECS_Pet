using Entitas;
using UnityEngine;
using Code.StaticData;

namespace Code.Gameplay.Monster
{
    public class SpawnMonsterOnInitSystem : IInitializeSystem
    {
        private readonly IMonsterFactory _monsterFactory;

        public SpawnMonsterOnInitSystem(IMonsterFactory monsterFactory)
        {
            _monsterFactory = monsterFactory;
        }

        public void Initialize()
        {
            _monsterFactory.CreateMonster(MonsterTypeId.Skeleton, new Vector3(0, 0, 5));
        }
    }
}