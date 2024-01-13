using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class SwingAttack : AttackBase
{
    public SwingEmitter SwingEmitter;
    private Collider[] hitColliders;

    public override void InitializeAttack(float attackRadius)
    {
        if (SwingEmitter != null)
            SwingEmitter.Initialize(attackRadius);

        hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack(AttackComponent attackComponent)
    {
        if (SwingEmitter != null)
            SwingEmitter.Emit();

        base.Attack(attackComponent);


        var size = Physics.OverlapSphereNonAlloc(transform.position, attackComponent.AttackRadius, hitColliders,
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