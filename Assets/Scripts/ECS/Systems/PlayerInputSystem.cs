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
   
    
    public override void OnAwake()
    {
        filter = this.World.Filter.With<PlayerInputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter) {
            ref var playerInputComponent = ref entity.GetComponent<PlayerInputComponent>();
            
            playerInputComponent.HorizontalInput = Input.GetAxisRaw("Horizontal");
            playerInputComponent.VerticalInput = Input.GetAxisRaw("Vertical");
        }
    }
}