using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(OrbSpawnSystem))]
public sealed class OrbSpawnSystem : UpdateSystem
{
    private Filter _orbsFilter;

    public override void OnAwake()
    {
        this._orbsFilter = this.World.Filter.With<OrbSpawnComponent>().With<HealthComponent>()
            .With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._orbsFilter)
        {
            ref var orbSpawnComponent = ref entity.GetComponent<OrbSpawnComponent>();
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();


            if (healthComponent.IsDead)
            {
                ObjectFactory.CreateObject(orbSpawnComponent.OrbPrefab, transformComponent.Transform.position);
                entity.RemoveComponent<OrbSpawnComponent>();
            }
        }
    }
}