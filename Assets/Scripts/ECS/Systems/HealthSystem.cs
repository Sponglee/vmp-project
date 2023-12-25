using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
public sealed class HealthSystem : UpdateSystem
{
    private Filter filter;

    public override void OnAwake()
    {
        this.filter = this.World.Filter.With<HealthComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();


            if (!healthComponent.IsInitialized)
            {
                healthComponent.CurrentHealth = healthComponent.MaxHealth;
                healthComponent.IsInitialized = true;
            }


            if (healthComponent.CurrentHealth <= 0 && healthComponent.IsInitialized)
            {
                healthComponent.IsDead = true;
                // entity.Dispose();
                Destroy(transformComponent.Transform.gameObject);
            }
        }
    }
}