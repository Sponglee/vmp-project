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
    private Filter filter;

    public override void OnAwake()
    {
        this.filter = this.World.Filter.With<AttackComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this.filter)
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
                        }
                        else
                        {
                            targetEntitiy.Entity.AddComponent<CachedDamageComponent>();
                            ref var cachedDamageComponent =
                                ref targetEntitiy.Entity.GetComponent<CachedDamageComponent>();

                            cachedDamageComponent.DamageCached += attackComponent.AttackDamage;
                        }
                    }
                }

                attackComponent.Attack.Attack();
                attackComponent.AttackTimer = 0f;
            }
        }
    }
}