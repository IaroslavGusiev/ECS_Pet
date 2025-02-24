using UnityEngine;
using Code.StaticData;
using Code.Infrastructure;
using Code.Common.Extensions;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardFactory : IGameBoardFactory
    {
        private readonly IEntityFactory _entityFactory;

        public GameBoardFactory(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public GameEntity CreateGameBoardCell(string viewPath, Vector3 position, Material material)
        {
            return _entityFactory
                .CreateEntity<GameEntity>(needToSetId: true)
                .AddViewPath(viewPath)
                .AddWorldPosition(position)
                .AddMaterialChangeRequest(material)
                .AddLayerMask(CollisionLayer.GameBoard.AsMask())
                .With(entity => entity.isGameBoardCell = true)
                .With(entity => entity.isOccupied = false);
        }
    }
}