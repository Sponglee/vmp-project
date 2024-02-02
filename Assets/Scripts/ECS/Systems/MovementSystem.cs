using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.Collections;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : FixedUpdateSystem
{
    private Filter filter;

    private Vector3 direciton;

    [Inject] public Game Game { get; set; }


    public override void OnAwake()
    {
        AntInject.Inject<MovementSystem>(this);
        filter = this.World.Filter.With<MovementComponent>().Without<OffScreenTagComponent>().With<TransformComponent>()
            .Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;

        foreach (var entity in this.filter)
        {
            ref var movementComponent = ref entity.GetComponent<MovementComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            transformComponent.Transform.Translate(new Vector3(
                movementComponent.HorizontalInput * movementComponent.Speed * deltaTime,
                0f,
                movementComponent.VerticalInput * movementComponent.Speed * deltaTime), Space.World);

            if (movementComponent.HorizontalInput != 0 || movementComponent.VerticalInput != 0)
            {
                movementComponent.IsMoving = true;

                direciton.x = movementComponent.HorizontalInput;
                direciton.y = 0f;
                direciton.z = movementComponent.VerticalInput;

                transformComponent.Transform.rotation = Quaternion.LookRotation(Vector3.Lerp(
                    transformComponent.Transform.forward,
                    direciton,
                    movementComponent.RotationSpeedRatio), Vector3.up);
            }
            else
            {
                movementComponent.IsMoving = false;
            }
        }
    }
}