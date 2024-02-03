using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Filter _cachedDamageFilter;

    public override void OnAwake()
    {
        this._cachedDamageFilter = this.World.Filter.With<CachedDamageComponent>()
            .With<HealthComponent>()
            .With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._cachedDamageFilter)
        {
            ref var cachedDamageComponent = ref entity.GetComponent<CachedDamageComponent>();
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();


            if (cachedDamageComponent.DamageCached > 0)
            {
                healthComponent.CurrentHealth -= cachedDamageComponent.DamageCached;
                cachedDamageComponent.DamageCached -= cachedDamageComponent.DamageCached;
                //Spawn numbers here via ui numbers service
                // Debug.Log(cachedDamageComponent.HitFx);
                if (cachedDamageComponent.HitFx != null)
                {
                    Destroy(
                        ObjectFactory.CreateObject(cachedDamageComponent.HitFx,
                            transformComponent.Transform, transformComponent.Transform.position + Vector3.up), 3f);
                }
            }

            if (cachedDamageComponent.DamageCached == 0)
            {
                entity.RemoveComponent<CachedDamageComponent>();
            }
        }
    }
}