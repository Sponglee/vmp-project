using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyInputSystem))]
public sealed class EnemyInputSystem : UpdateSystem
{
    private Filter _enemyInputFilter;
    private Filter _playerTagFilter;
    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<EnemyInputSystem>(this);

        _enemyInputFilter = this.World.Filter.With<EnemyInputComponent>().With<TransformComponent>()
            .With<EnemyTagComponent>()
            .Build();
        _playerTagFilter = this.World.Filter.With<PlayerTagComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;

        Vector3 targetPosition = _playerTagFilter.First().GetComponent<TransformComponent>().Transform.position;

        foreach (var entity in _enemyInputFilter)
        {
            ref var enemyInputComponent = ref entity.GetComponent<EnemyInputComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            enemyInputComponent.MovementProvider.GetData().HorizontalInput =
                (targetPosition - transformComponent.Transform.position).normalized.x;
            enemyInputComponent.MovementProvider.GetData().VerticalInput =
                (targetPosition - transformComponent.Transform.position).normalized.z;
        }
    }
}