using Anthill.Inject;
using DG.Tweening;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AimSystem))]
public sealed class AimSystem : UpdateSystem
{
    private Filter _turretFilter;
    [Inject] public Game Game { get; set; }


    public override void OnAwake()
    {
        AntInject.Inject<AimSystem>(this);

        _turretFilter = World.Filter.With<AimComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;

        foreach (var turretEntity in _turretFilter)
        {
            ref var turretRotationComponent = ref turretEntity.GetComponent<AimComponent>();
            ref var transformComponent = ref turretEntity.GetComponent<TransformComponent>();

            if (turretRotationComponent.TurretLookTarget == null)
            {
                turretRotationComponent.SetRotateTarget(null);
            }

            //Remove Movement Component if stop while aiming
            if (turretRotationComponent.IsRotationInProgress && turretRotationComponent.StopWhileAiming &&
                !turretEntity.Has<StopTagComponent>())
            {
                turretEntity.AddComponent<StopTagComponent>();
            }
            else if (!turretRotationComponent.IsRotationInProgress && turretRotationComponent.StopWhileAiming &&
                     turretEntity.Has<StopTagComponent>())
            {
                turretEntity.RemoveComponent<StopTagComponent>();
            }

            if (turretRotationComponent.IsRotationInProgress && turretRotationComponent.TurretLookTarget != null)
            {
                turretRotationComponent.LookTimer += deltaTime;

                var dir = turretRotationComponent.TurretLookTarget.position -
                          turretRotationComponent.TurretPivot.position;
                dir.y = 0f;


                turretRotationComponent.TurretPivot.rotation = Quaternion.Lerp(turretRotationComponent.StartRotation,
                    Quaternion.LookRotation(dir, Vector3.up),
                    turretRotationComponent.LookTimer / turretRotationComponent.AimDuration);


                if (turretRotationComponent.LookTimer >= turretRotationComponent.AimDuration)
                {
                    turretRotationComponent.IsRotationInProgress = false;
                }
            }
            else if (turretRotationComponent.IsRotationInProgress && turretRotationComponent.TurretLookTarget == null)
            {
                turretRotationComponent.LookTimer += deltaTime;

                turretRotationComponent.TurretPivot.rotation =
                    Quaternion.Lerp(turretRotationComponent.StartRotation, transformComponent.Transform.rotation,
                        turretRotationComponent.LookTimer / turretRotationComponent.AimDuration);


                if (turretRotationComponent.LookTimer >= turretRotationComponent.AimDuration)
                {
                    turretRotationComponent.IsRotationInProgress = false;
                }
            }
            else if (!turretRotationComponent.IsRotationInProgress && turretRotationComponent.TurretLookTarget != null)
            {
                turretRotationComponent.ResetTimer += deltaTime;
                if (turretRotationComponent.ResetTimer >= turretRotationComponent.TurretRotationResetTime)
                {
                    turretRotationComponent.SetRotateTarget(null);
                }
            }
        }
    }
}