using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GameplaySystem))]
public sealed class GameplaySystem : UpdateSystem
{
    private Filter _playerFilter;
    private bool DeathCheck = false;

    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<GameplaySystem>(this);

        this._playerFilter = this.World.Filter.With<PlayerTagComponent>().With<TransformComponent>()
            .With<PlayerInputComponent>()
            .With<DeathComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in _playerFilter)
        {
            ref var deathComponent = ref entity.GetComponent<DeathComponent>();

            DeathCheck = deathComponent.IsInitialized;

            entity.RemoveComponent<PlayerInputComponent>();
        }


        if (DeathCheck)
        {
            Game.GameManager.GameOver();
        }
    }
}