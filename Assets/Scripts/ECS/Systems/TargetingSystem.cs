using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TargetingSystem))]
public sealed class TargetingSystem : UpdateSystem
{
    private Filter _enemyInputFilter;
    private Filter _playerTagFilter;
    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<TargetingSystem>(this);

        _enemyInputFilter = this.World.Filter.With<TransformComponent>().With<EnemyTagComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
    }
}