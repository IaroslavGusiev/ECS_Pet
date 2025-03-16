using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Fighter
{
    public class FighterFactory : IFighterFactory
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly Vector3 _spawnOffsetForGameBoard = new(0f, 0.5f, 0f);

        public FighterFactory(
            IEntityFactory entityFactory, 
            IStaticDataService staticDataService)
        {
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }
        
        public GameEntity CreateFighter(FighterTypeId fighterTypeId, Vector3 at)
        {
            FighterConfig fighterConfig = _staticDataService.GetFighterConfig(fighterTypeId);

            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddViewPath(fighterConfig.ViewPath)
                .AddFighterTypeId(fighterTypeId)
                .AddWorldPosition(at)
                .With(entity => entity.isFighter = true)
                .With(entity => entity.isSelected = true);
        }
    }
}