using Zenject;
using UnityEngine;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Code.Infrastructure.Services;

namespace Code.Gameplay.Features.GameBoard
{
    public class GameBoardBehaviour : MonoBehaviour
    {
        private readonly List<CellBehaviour> _cells = new();
        private IInstantiator _instantiator;
        private IAddressablesAssetProvider _assetProvider;

        [Inject]
        public void Construct(IInstantiator instantiator, IAddressablesAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<GameBoardBehaviour> Initialize(GameBoardConfig config)
        {
            var cellPrefab = await _assetProvider.LoadAndGetComponent<CellBehaviour>(config.CellPrefabPath);
            Material randomMaterial = config.GetRandomCellMaterial();
            Vector2Int boardSize = config.BoardSize;

            int minX = -boardSize.x / 2;
            int maxX = boardSize.x / 2 - 1;
            
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = 0; y < boardSize.y; y++)
                {
                    var cell = _instantiator.InstantiatePrefabForComponent<CellBehaviour>(cellPrefab, transform);
                    _cells.Add(cell);
                    cell.transform.localPosition = new Vector3(x, 0, y);
                    cell.UpdateMaterial(randomMaterial);
                }
            }

            return this;
        }
    }
}