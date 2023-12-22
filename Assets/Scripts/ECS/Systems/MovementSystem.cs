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
    private const float LOOK_SPEED_SMOOTHING = 0.5f;
    
    private Filter filter;
    
    public override void OnAwake()
    {
        filter = this.World.Filter.With<MovementComponent>().Without<OffScreenTagComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        
        foreach (var entity in this.filter)
        {
            ref var movementComponent = ref entity.GetComponent<MovementComponent>();

            movementComponent.Transform.Translate(new Vector3(
                movementComponent.HorizontalInput * movementComponent.Speed * deltaTime,
                0f,
                movementComponent.VerticalInput * movementComponent.Speed * deltaTime), Space.World);

            if (movementComponent.HorizontalInput != 0 || movementComponent.VerticalInput != 0)
                movementComponent.Transform.rotation = Quaternion.LookRotation(Vector3.Lerp(movementComponent.Transform.forward,
                    new Vector3(movementComponent.HorizontalInput, 0f, movementComponent.VerticalInput),LOOK_SPEED_SMOOTHING), Vector3.up);
        }
    }
}