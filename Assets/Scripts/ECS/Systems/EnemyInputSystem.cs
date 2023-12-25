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

    public override void OnAwake()
    {
        _enemyInputFilter = this.World.Filter.With<EnemyInputComponent>().With<TransformComponent>()
            .With<EnemyTagComponent>()
            .Build();
        _playerTagFilter = this.World.Filter.With<PlayerTagComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        Vector3 targetPosition = _playerTagFilter.First().GetComponent<TransformComponent>().Transform.position;

        foreach (var entity in _enemyInputFilter)
        {
            ref var enemyInputComponent = ref entity.GetComponent<EnemyInputComponent>();
            ref var enemyTagComponent = ref entity.GetComponent<EnemyTagComponent>();

            enemyInputComponent.MovementProvider.GetData().HorizontalInput =
                (targetPosition - enemyTagComponent.Transform.position).normalized.x;
            enemyInputComponent.MovementProvider.GetData().VerticalInput =
                (targetPosition - enemyTagComponent.Transform.position).normalized.z;
        }
    }
}