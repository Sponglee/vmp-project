using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputSystem))]
public sealed class PlayerInputSystem : UpdateSystem
{
    private Filter filter;
    private Transform _cameraTransform;


    public override void OnAwake()
    {
        _cameraTransform = Camera.main.transform;

        filter = this.World.Filter.With<PlayerInputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
        {
            ref var playerInputComponent = ref entity.GetComponent<PlayerInputComponent>();

            Vector3 dir = _cameraTransform.rotation *
                          new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            playerInputComponent.MovementProvider.GetData().HorizontalInput = dir.x;
            playerInputComponent.MovementProvider.GetData().VerticalInput = dir.z;
        }
    }
}