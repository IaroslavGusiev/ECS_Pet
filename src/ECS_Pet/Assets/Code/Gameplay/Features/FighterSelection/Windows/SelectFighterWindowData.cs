using Code.StaticData;
using System.Collections.Generic;

namespace Code.Gameplay.FighterSelection
{
    public class SelectFighterWindowData
    {
        public readonly List<FighterTypeId> FighterTypesToShow;

        public SelectFighterWindowData(List<FighterTypeId> fighterTypesToShow)
        {
            FighterTypesToShow = fighterTypesToShow;
        }
    }
}