using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class SwingAttack : AttackBase
{
    public float SwingRadius = 1f;
    public SwingEmitter SwingEmitter;
    private Collider[] hitColliders;

    public override void InitializeAttack()
    {
        if (SwingEmitter != null)
            SwingEmitter.Initialize(SwingRadius);

        hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack(Entity entity)
    {
        ref var attackComponent = ref entity.GetComponent<AttackComponent>();

        if (SwingEmitter != null)
            SwingEmitter.Emit();

        base.Attack(entity);


        var size = Physics.OverlapSphereNonAlloc(transform.position, SwingRadius, hitColliders,
            attackComponent.LayerMask);


        for (int i = 0; i < size; i++)
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
}