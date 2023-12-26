using DG.Tweening;
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
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PickupSystem))]
public sealed class PickupSystem : UpdateSystem
{
    private Filter _pickableFilter;
    private Filter _playerFilter;


    public override void OnAwake()
    {
        _pickableFilter = World.Filter.With<PickableComponent>().With<TransformComponent>().Build();
        _playerFilter = World.Filter.With<PlayerTagComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var entity in _pickableFilter)
            {
                ref var pickableComponent = ref entity.GetComponent<PickableComponent>();

                pickableComponent.PickupRadius = 50f;
            }
        }

        foreach (var playerEntity in _playerFilter)
        {
            ref var playerTransformComponent = ref playerEntity.GetComponent<TransformComponent>();

            foreach (var entity in _pickableFilter)
            {
                ref var pickableComponent = ref entity.GetComponent<PickableComponent>();
                ref var transformComponent = ref entity.GetComponent<TransformComponent>();

                if (!pickableComponent.IsInitialized)
                {
                    pickableComponent.IsInitialized = true;
                }

                if (pickableComponent.IsInitialized && !pickableComponent.IsPickedUp &&
                    Vector3.Distance(
                        playerTransformComponent.Transform.position,
                        transformComponent.Transform.position) <= pickableComponent.PickupRadius)
                {
                    pickableComponent.IsPickedUp = true;
                    pickableComponent.TargetTransform = playerTransformComponent.Transform;
                    pickableComponent.StartPosition = transformComponent.Transform.position;
                }

                if (pickableComponent.IsPickedUp)
                {
                    transformComponent.Transform.position =
                        Vector3.Lerp(pickableComponent.StartPosition, pickableComponent.TargetTransform.position +
                                                                      Vector3.up *
                                                                      pickableComponent.AnimationCurve.Evaluate(
                                                                          pickableComponent.Timer /
                                                                          pickableComponent.PickupDuration),
                            pickableComponent.Timer / pickableComponent.PickupDuration);
                }

                if (pickableComponent.IsInitialized && pickableComponent.IsPickedUp &&
                    pickableComponent.TargetTransform != null)
                {
                    pickableComponent.Timer += deltaTime;

                    if (pickableComponent.Timer >= pickableComponent.PickupDuration)
                    {
                        Destroy(transformComponent.Transform.gameObject);
                    }
                }
            }
        }
    }
}