using System.Collections.Generic;

namespace Code.Gameplay.Fighter
{
    public class FighterPlacementService : IFighterPlacementService
    {
        private readonly Dictionary<int, int> _towersToCells = new(); // key: cell id, value: figter id
        
        public void RegisterFighter(int fighterId, int cellId) => 
            _towersToCells[fighterId] = cellId;

        public void UnregisterFighter(int cellId) => 
            _towersToCells.Remove(cellId);
        
        public bool IsFighterPlaced(int fighterId) => 
            _towersToCells.ContainsKey(fighterId);
    }
}