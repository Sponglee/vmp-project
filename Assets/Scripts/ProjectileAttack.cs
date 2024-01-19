using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.Core;
using DG.Tweening;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileAttack : AttackBase
{
    public float ShootRadius = 1f;
    private Collider[] _hitColliders;
    private EntityProvider targetEntity;


    protected AimProvider _aimProvider;


    public override void InitializeAttack()
    {
        _attackProvider = transform.GetComponent<AttackProvider>();
        _aimProvider = transform.GetComponentInParent<AimProvider>();

        _hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack()
    {
        _attackProvider.GetData().IsArmed = true;

        var size = Physics.OverlapSphereNonAlloc(transform.position, ShootRadius, _hitColliders,
            _attackProvider.GetData().LayerMask);
        targetEntity = GetClosestTarget(size);

        if (targetEntity == null)
        {
            _attackProvider.GetData().IsArmed = false;
            return;
        }

        Aim();
        base.Attack();
    }

    private void Aim()
    {
        _aimProvider.GetData().SetRotateTarget(targetEntity.transform);
        DOVirtual.DelayedCall(_aimProvider.GetData().AimDuration, AimComplete);
    }

    private void AimComplete()
    {
        _attackProvider.GetData().IsArmed = false;
        DealDamage(ref _attackProvider.GetData());
    }

    private void DealDamage(ref AttackComponent attackComponent)
    {
        if (targetEntity == null) return;

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

    private EntityProvider GetClosestTarget(int size)
    {
        EntityProvider tmpEntity = null;

        for (int i = 0; i < size; i++)
        {
            targetEntity = _hitColliders[i].GetComponent<EntityProvider>();

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, ShootRadius);
    }
}