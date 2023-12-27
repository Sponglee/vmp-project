using Anthill.Inject;
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
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AttackSystem))]
public sealed class AttackSystem : UpdateSystem
{
    private Filter _attackFilter;
    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<AttackSystem>(this);
        this._attackFilter = this.World.Filter.With<AttackComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;


        foreach (var entity in this._attackFilter)
        {
            ref var attackComponent = ref entity.GetComponent<AttackComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            if (!attackComponent.IsInitialized)
            {
                attackComponent.IsInitialized = true;
                attackComponent.Attack.InitializeAttack(attackComponent.AttackRadius);
            }

            attackComponent.AttackTimer += deltaTime;
            if (attackComponent.AttackTimer >= attackComponent.AttackCooldown)
            {
                //TODO MAKE THIS A SEPARATE SYSTEM
                Collider[] hitColliders =
                    Physics.OverlapSphere(transformComponent.Transform.position, attackComponent.AttackRadius,
                        attackComponent.LayerMask);

                if (hitColliders != null)
                {
                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        EntityProvider targetEntitiy = hitColliders[i].GetComponent<EntityProvider>();

                        if (targetEntitiy == null) continue;

                        if (targetEntitiy.Entity.Has<CachedDamageComponent>())
                        {
                            ref var cachedDamageComponent =
                                ref targetEntitiy.Entity.GetComponent<CachedDamageComponent>();

                            cachedDamageComponent.DamageCached += attackComponent.AttackDamage;
                            cachedDamageComponent.HitFx = attackComponent.HitFx;
                        }
                        else
                        {
                            targetEntitiy.Entity.AddComponent<CachedDamageComponent>();
                            ref var cachedDamageComponent =
                                ref targetEntitiy.Entity.GetComponent<CachedDamageComponent>();

                            cachedDamageComponent.DamageCached += attackComponent.AttackDamage;
                            cachedDamageComponent.HitFx = attackComponent.HitFx;
                        }
                    }
                }

                attackComponent.Attack.Attack();
                attackComponent.AttackTimer = 0f;
            }
        }
    }
}