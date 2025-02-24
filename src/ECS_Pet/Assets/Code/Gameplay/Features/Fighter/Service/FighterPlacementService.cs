using System.Collections.Generic;

namespace Code.Gameplay.Fighter
{
    public class FighterPlacementService : IFighterPlacementService
    {
        private readonly Dictionary<int, int> _towersToCells = new(); // key: cell id, value: tower id
        
        public void RegisterFighter(int towerId, int cellId)
        {
            _towersToCells[cellId] = towerId;
        }
        
        public void UnregisterFighter(int cellId)
        {
            _towersToCells.Remove(cellId);
        }
    }
}