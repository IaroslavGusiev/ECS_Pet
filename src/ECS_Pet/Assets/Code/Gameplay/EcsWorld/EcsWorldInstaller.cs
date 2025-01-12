using Zenject;
using Code.Gameplay.Features.GameBoard;

namespace Code.Gameplay.EcsWorld
{
    public class EcsWorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameBoardFactory>()
                .AsSingle();
        }
    }
}