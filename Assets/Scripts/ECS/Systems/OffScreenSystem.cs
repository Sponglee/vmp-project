using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(OffScreenSystem))]
public sealed class OffScreenSystem : UpdateSystem
{
    private Camera _camera;
    private Filter _offscreenFilter;
    private Filter _withinscreenFilter;

    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<OffScreenSystem>(this);
        _camera = Camera.main;

        _offscreenFilter = this.World.Filter.With<MovementComponent>().With<OffScreenTagComponent>().Build();
        _withinscreenFilter = this.World.Filter.With<MovementComponent>().Without<OffScreenTagComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;


        foreach (var entity in this._withinscreenFilter)
        {
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            if (IsOffScreen(transformComponent))
            {
                entity.AddComponent<OffScreenTagComponent>();
            }
        }

        foreach (var entity in this._offscreenFilter)
        {
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            if (!IsOffScreen(transformComponent))
            {
                entity.RemoveComponent<OffScreenTagComponent>();
            }
        }
    }

    private bool IsOffScreen(TransformComponent aComponent)
    {
        Vector3 viewportPoint = _camera.WorldToViewportPoint(aComponent.Transform.position);

        return (viewportPoint.x < -0.2f || viewportPoint.x > 1.2f || viewportPoint.y < -0.2f || viewportPoint.y > 1.2f);
    }
}