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

    public override void Attack()
    {
        if (SwingEmitter != null)
            SwingEmitter.Emit();

        base.Attack();

        var size = Physics.OverlapSphereNonAlloc(transform.position, SwingRadius, hitColliders,
            _attackProvider.GetData().LayerMask);

        for (int i = 0; i < size; i++)
        {
            EntityProvider targetEntitiy = hitColliders[i].GetComponent<EntityProvider>();

            if (targetEntitiy == null) continue;
            TakeDamage(targetEntitiy, _attackProvider.GetData());
        }
    }

    private void TakeDamage(EntityProvider targetEntity, AttackComponent attackComponent)
    {
        if (targetEntity.Entity.Has<CachedDamageComponent>())
        {
            ref var cachedDamageComponent =
                ref targetEntity.Entity.GetComponent<CachedDamageComponent>();

            cachedDamageComponent.DamageCached += attackComponent.AttackDamage;
            cachedDamageComponent.HitFx = attackComponent.HitFx;
        }
        else
        {
            targetEntity.Entity.AddComponent<CachedDamageComponent>();
            ref var cachedDamageComponent =
                ref targetEntity.Entity.GetComponent<CachedDamageComponent>();

            cachedDamageComponent.DamageCached += attackComponent.AttackDamage;
            cachedDamageComponent.HitFx = attackComponent.HitFx;
        }
    }
}