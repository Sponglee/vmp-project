using Anthill.Core;
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
    private Filter _healthFilter;
    private HealthBarController _healthBarController;

    public override void OnAwake()
    {
        _healthBarController = AntEngine.Get<Menu>().Get<HealthBarController>();

        this._healthFilter = this.World.Filter.With<HealthBarComponent>().With<HealthComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._healthFilter)
        {
            ref var healthBarComponent = ref entity.GetComponent<HealthBarComponent>();
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();

            if (!healthBarComponent.IsInitialized)
            {
                InitializeBar(healthBarComponent);
            }
            else
            {
                _healthBarController.UpdateBarPosition(healthBarComponent.HealthBarIndex,
                    healthBarComponent.Transform.position);
                _healthBarController.UpdateBarValue(healthBarComponent.HealthBarIndex,
                    healthComponent.CurrentHealth / healthComponent.MaxHealth);
            }

            if (healthComponent.CurrentHealth <= 0)
            {
                ReleaseBar(healthBarComponent);
            }
        }
    }

    public void InitializeBar(HealthBarComponent aHealthBar)
    {
        int barIndex = _healthBarController.RequestBar(aHealthBar.Kind);

        if (barIndex == -1) return;

        aHealthBar.IsInitialized = true;
        aHealthBar.HealthBarIndex = barIndex;
    }

    public void ReleaseBar(HealthBarComponent aHealthBar)
    {
        _healthBarController.ReleaseBar(aHealthBar.HealthBarIndex);
        aHealthBar.IsInitialized = false;
        aHealthBar.HealthBarIndex = -1;
    }
}