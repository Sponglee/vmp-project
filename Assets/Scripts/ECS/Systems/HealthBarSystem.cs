using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthBarSystem))]
public sealed class HealthBarSystem : UpdateSystem
{
    private Filter filter;

    
    public override void OnAwake()
    {
        this.filter = this.World.Filter.With<HealthBarComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var healthBarComponent = ref entity.GetComponent<HealthBarComponent>();

            if (!healthBarComponent.IsInitialized)
            {
                InitializeUI(healthBarComponent);
            }
        }
    }

    public void InitializeUI(HealthBarComponent aHealthBar)
    {
        
    }
}