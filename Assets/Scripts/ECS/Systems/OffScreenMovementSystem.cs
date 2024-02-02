using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(OffScreenMovementSystem))]
public sealed class OffScreenMovementSystem : FixedUpdateSystem
{
    private const float OFFSCREEN_TICK_TIME = 3f;
    private float tickTimer = 0f;

    private Filter filter;
    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<OffScreenMovementSystem>(this);
        filter = this.World.Filter.With<MovementComponent>().With<TransformComponent>().With<OffScreenTagComponent>()
            .Without<StopTagComponent>()
            .Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;


        tickTimer += deltaTime;
        if (tickTimer >= OFFSCREEN_TICK_TIME)
        {
            tickTimer = 0f;
            Tick(deltaTime);
        }
    }

    public void Tick(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var movementComponent = ref entity.GetComponent<MovementComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            transformComponent.Transform.Translate(new Vector3(
                movementComponent.HorizontalInput * movementComponent.Speed * OFFSCREEN_TICK_TIME,
                0f,
                movementComponent.VerticalInput * movementComponent.Speed * OFFSCREEN_TICK_TIME), Space.World);

            if (movementComponent.HorizontalInput != 0 || movementComponent.VerticalInput != 0)
                transformComponent.Transform.rotation = Quaternion.LookRotation(
                    new Vector3(movementComponent.HorizontalInput, 0f, movementComponent.VerticalInput), Vector3.up);
        }
    }
}