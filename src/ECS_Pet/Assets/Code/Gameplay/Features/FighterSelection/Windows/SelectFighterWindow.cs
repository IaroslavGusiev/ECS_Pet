using Zenject;
using UnityEngine;
using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Infrastructure;
using Code.Common.Extensions;
using System.Collections.Generic;
using Code.Infrastructure.Services;

namespace Code.Gameplay.FighterSelection
{
    public class SelectFighterWindow : BaseWindow<SelectFighterWindowData>
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private SelectFighterView fighterViewPrefab;

        private readonly List<SelectFighterView> _fighterViews = new();
        private IStaticDataService _staticDataService;
        private IEntityFactory _entityFactory;
        private IInstantiator _instantiator;
        
        private FighterTypeId _selectedFighter;

        [Inject]
        public void Construct(
            IInstantiator instantiator, 
            IEntityFactory entityFactory,
            IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _entityFactory = entityFactory;
            _staticDataService = staticDataService;
        }
        
        public override void SetupOnInstantiate(SelectFighterWindowData data)
        {
            foreach (FighterTypeId type in data.FighterTypesToShow)
            {
                _instantiator
                    .InstantiatePrefabForComponent<SelectFighterView>(fighterViewPrefab, contentContainer)
                    .Initialize(_staticDataService.GetFighterConfig(type), HandleClickOnFighterView)
                    .With(view => _fighterViews.Add(view));
            }
        }

        public void DeselectAll() => 
            _fighterViews.ForEach(view => view.MarkAsSelected(false));

        private void HandleClickOnFighterView(FighterTypeId type)
        {
            if (_selectedFighter == type)
            {
                return;
            }
            
            DeselectAll();
            MarkAsSelected(type);

            _entityFactory
                .CreateEntity<GameEntity>()
                .With(entity => entity.AddFighterTypeId(_selectedFighter))
                .With(entity => entity.isFighterRequest = true);
        }

        private void MarkAsSelected(FighterTypeId type)
        {
            _selectedFighter = type;
            
            _fighterViews
                .Find(view => view.FighterTypeId == type)
                .MarkAsSelected(true);
        }
    }
}