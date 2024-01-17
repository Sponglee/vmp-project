using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileAttack : AttackBase
{
    public float ShootRadius = 1f;
    private Collider[] hitColliders;

    public override void InitializeAttack()
    {
        hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack(Entity entity)
    {
        ref var attackComponent = ref entity.GetComponent<AttackComponent>();

        base.Attack(entity);


        var size = Physics.OverlapSphereNonAlloc(transform.position, ShootRadius, hitColliders,
            attackComponent.LayerMask);

        var targetEntity = GetClosestTarget(size);

        if (targetEntity == null) return;

        TakeDamage(targetEntity, attackComponent);

        RotateTurret(entity, targetEntity);
    }

    public EntityProvider GetClosestTarget(int size)
    {
        for (int i = 0; i < size; i++)
        {
            EntityProvider targetEntitiy = hitColliders[i].GetComponent<EntityProvider>();

            if (targetEntitiy == null) continue;
            return targetEntitiy;
        }

        return null;
    }

    public void TakeDamage(EntityProvider targetEntity, AttackComponent attackComponent)
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

    public void RotateTurret(Entity entity, EntityProvider targetEntity)
    {
        if (!entity.Has<TurretRotationComponent>()) return;

        ref var turretRotateComponent = ref entity.GetComponent<TurretRotationComponent>();


        turretRotateComponent.RotateToTarget(targetEntity.transform);
    }
}