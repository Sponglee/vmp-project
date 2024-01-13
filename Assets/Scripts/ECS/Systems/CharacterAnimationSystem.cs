using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.Collections;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CharacterAnimationSystem))]
public sealed class CharacterAnimationSystem : UpdateSystem
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private Filter animatorsFilter;

    [Inject] public Game Game { get; set; }


    public override void OnAwake()
    {
        AntInject.Inject<CharacterAnimationSystem>(this);
        animatorsFilter = this.World.Filter.With<MovementComponent>().With<CharacterAnimatorComponent>()
            .Without<OffScreenTagComponent>()
            .Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;

        foreach (var entity in this.animatorsFilter)
        {
            ref var movementComponent = ref entity.GetComponent<MovementComponent>();
            ref var animatorComponent = ref entity.GetComponent<CharacterAnimatorComponent>();

            if (movementComponent.IsMoving)
            {
                animatorComponent.CharacterAnimator.SetBool(IsMoving, true);
            }
            else
            {
                animatorComponent.CharacterAnimator.SetBool(IsMoving, false);
            }
        }
    }
}