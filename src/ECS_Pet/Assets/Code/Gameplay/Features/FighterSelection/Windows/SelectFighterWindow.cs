using Zenject;
using UnityEngine;
using Code.StaticData;
using Code.UI.BaseWindow;
using Code.Infrastructure;
using Code.Common.Extensions;
using Code.Gameplay.UI.Gold;
using Code.Gameplay.Fighter;
using System.Collections.Generic;
using Code.Infrastructure.Services;

namespace Code.Gameplay.FighterSelection
{
    public class SelectFighterWindow : BaseWindow<SelectFighterWindowData>
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private SelectFighterView fighterViewPrefab;

        private readonly List<SelectFighterView> _fighterViews = new();
        private readonly Dictionary<FighterTypeId, FighterConfig> _fighterConfigs = new();

        private IInstantiator _instantiator;
        private IEntityFactory _entityFactory;
        private IStorageUIService _storageUIService;
        private IStaticDataService _staticDataService;
        private IFighterAffordabilityService _fighterAffordabilityService;
        
        private FighterTypeId _selectedFighter;

        [Inject]
        public void Construct(
            IInstantiator instantiator, 
            IEntityFactory entityFactory,
            IStorageUIService storageUIService,
            IStaticDataService staticDataService,
            IFighterAffordabilityService fighterAffordabilityService)
        {
            _instantiator = instantiator;
            _entityFactory = entityFactory;
            _storageUIService = storageUIService;
            _staticDataService = staticDataService;
            _fighterAffordabilityService = fighterAffordabilityService;
        }
        
        public override void SetupOnInstantiate(SelectFighterWindowData data)
        {
            foreach (FighterTypeId type in data.FighterTypesToShow)
            {
                FighterConfig config = _staticDataService.GetFighterConfig(type);
                _fighterConfigs[type] = config;
                
                _instantiator
                    .InstantiatePrefabForComponent<SelectFighterView>(fighterViewPrefab, contentContainer)
                    .Initialize(config, HandleClickOnFighterView)
                    .With(view => _fighterViews.Add(view));
            }

            _storageUIService.GoldChanged += RefreshFighterAvailability;
            RefreshFighterAvailability();
        }

        private void OnDestroy()
        {
            if (_storageUIService != null)
            {
                _storageUIService.GoldChanged -= RefreshFighterAvailability;
            }
        }

        public void DeselectAll()
        {
            _selectedFighter = FighterTypeId.Unknown;
            _fighterViews.ForEach(view => view.MarkAsSelected(false));
        }

        private void HandleClickOnFighterView(FighterTypeId type)
        {
            if (_selectedFighter == type)
            {
                return;
            }

            FighterConfig config = _fighterConfigs[type];

            if (_fighterAffordabilityService.CanAfford(config) == false)
            {
                RefreshFighterAvailability();
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

        private void RefreshFighterAvailability()
        {
            foreach (SelectFighterView view in _fighterViews)
            {
                bool canPurchase = _fighterAffordabilityService.CanAfford(_fighterConfigs[view.FighterTypeId]);
                view.SetInteractable(canPurchase);
            }
        }
    }
}
