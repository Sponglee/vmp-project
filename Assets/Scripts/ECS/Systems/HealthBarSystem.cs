using System;
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
    private Filter _healthBarFilter;
    private HealthBarController _healthBarController;

    public override void OnAwake()
    {
        _healthBarController = AntEngine.Get<Menu>().Get<HealthBarController>();

        this._healthBarFilter = this.World.Filter.With<HealthBarComponent>().With<TransformComponent>()
            .With<HealthComponent>().Build();
    }

    // public override void Dispose()
    // {
    //     Debug.Log("DISPOSE");
    //     _healthBarController.Hide();
    // }
    //
    // private void OnDisable()
    // {
    //     Debug.Log("DISABLE");
    //     _healthBarController.Hide();
    // }
    //
    // private void OnEnable()
    // {
    //     Debug.Log("ENABLE");
    //     _healthBarController.Show();
    // }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._healthBarFilter)
        {
            ref var healthBarComponent = ref entity.GetComponent<HealthBarComponent>();
            ref var healthComponent = ref entity.GetComponent<HealthComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            if (!healthBarComponent.IsInitialized)
            {
                InitializeBar(ref healthBarComponent);
            }
            else
            {
                _healthBarController.UpdateBarPosition(healthBarComponent.HealthBarIndex,
                    transformComponent.Transform.position + healthBarComponent.HealthBarOffset);
                _healthBarController.UpdateBarValue(healthBarComponent.HealthBarIndex,
                    healthComponent.CurrentHealth / healthComponent.MaxHealth);
            }

            if (healthComponent.CurrentHealth <= 0)
            {
                ReleaseBar(ref healthBarComponent);
            }
        }
    }

    public void InitializeBar(ref HealthBarComponent aHealthBar)
    {
        int barIndex = _healthBarController.RequestBar(aHealthBar.Kind);

        if (barIndex == -1) return;

        aHealthBar.IsInitialized = true;
        aHealthBar.HealthBarIndex = barIndex;
    }

    public void ReleaseBar(ref HealthBarComponent aHealthBar)
    {
        _healthBarController.ReleaseBar(aHealthBar.HealthBarIndex);
        aHealthBar.IsInitialized = false;
        aHealthBar.HealthBarIndex = -1;
    }
}