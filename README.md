# ECS Pet

ECS Pet is an experimental Unity auto-battler prototype created to explore Entity Component System (ECS) gameplay and the infrastructure around it. Players select fighters, place them on a grid, and watch the simulation handle movement, targeting, combat, abilities, effects, and character stats.

## Project highlights

- Feature-based gameplay built with **Entitas ECS** and generated contexts/components
- Decoupled entity logic and Unity `GameObject` views
- **Zenject** composition root with factories and injectable services
- Async bootstrap and application state machine powered by **UniTask**
- **Addressables**-based loading for gameplay assets and ScriptableObject configuration
- Systems for fighter placement, AI targets, movement, combat, mana, statuses, VFX, and cleanup

## Tech

Unity 6 (6000.0.59f2), C#, Entitas, Zenject, Addressables, UniTask, PrimeTween, and HDRP.

## Running the project

1. Clone the repository.
2. Open `src/ECS_Pet` with Unity 6000.0.59f2.
3. Open `Assets/Scene/Bootstrap.unity` and enter Play Mode.

Entitas-generated code is included in the repository. Jenny configuration and generation helpers are located in the `Jenny` directory.

## Status

This is a learning and experimentation project rather than a finished game. Some systems and content are incomplete, but the repository demonstrates a working ECS-oriented gameplay architecture and supporting services.
