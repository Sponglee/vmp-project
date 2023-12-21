using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MovementSystem))]
public sealed class MovementSystem : FixedUpdateSystem
{
    private Filter filter;


    public override void OnAwake()
    {
        filter = this.World.Filter.With<MovementComponent>().With<PlayerInputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var movementComponent = ref entity.GetComponent<MovementComponent>();
            ref var playerInputComponent = ref entity.GetComponent<PlayerInputComponent>();


            movementComponent.Rigidbody.velocity = new Vector3(
                playerInputComponent.HorizontalInput * movementComponent.Speed * deltaTime,
                movementComponent.Rigidbody.velocity.y,
                playerInputComponent.VerticalInput * movementComponent.Speed * deltaTime);
        }
    }
}