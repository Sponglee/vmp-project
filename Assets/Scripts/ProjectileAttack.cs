using System;
using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileAttack : AttackBase
{
    public float ShootRadius = 1f;
    private Collider[] _hitColliders;

    public override void InitializeAttack()
    {
        _hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack(Entity entity)
    {
        ref var attackComponent = ref entity.GetComponent<AttackComponent>();

        base.Attack(entity);


        var size = Physics.OverlapSphereNonAlloc(transform.position, ShootRadius, _hitColliders,
            attackComponent.LayerMask);

        var targetEntity = GetClosestTarget(size);

        if (targetEntity == null)
        {
            ResetTurret(entity);
            return;
        }

        TakeDamage(targetEntity, attackComponent);

        RotateTurret(entity, targetEntity);
    }

    private EntityProvider GetClosestTarget(int size)
    {
        EntityProvider tmpEntity = null;

        for (int i = 0; i < size; i++)
        {
            EntityProvider targetEntity = _hitColliders[i].GetComponent<EntityProvider>();

            if (targetEntity == null) continue;

            if (tmpEntity != null && Vector3.Distance(tmpEntity.transform.position, transform.position) <
                Vector3.Distance(targetEntity.transform.position, transform.position))
            {
                continue;
            }
            else
            {
                tmpEntity = targetEntity;
            }
        }

        return tmpEntity;
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

    private void RotateTurret(Entity entity, EntityProvider targetEntity)
    {
        if (!entity.Has<TurretRotationComponent>()) return;
        ref var turretRotateComponent = ref entity.GetComponent<TurretRotationComponent>();
        turretRotateComponent.RotateToTarget(targetEntity.transform);
    }

    private void ResetTurret(Entity entity)
    {
        if (!entity.Has<TurretRotationComponent>()) return;
        ref var turretRotateComponent = ref entity.GetComponent<TurretRotationComponent>();
        turretRotateComponent.ResetTurret();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, ShootRadius);
    }
}