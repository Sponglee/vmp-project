using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.Core;
using DG.Tweening;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileAttack : AttackBase
{
    public bool IsHoming = false;
    public float DelayBeforeAttack;

    public GameObject projectileFx;

    protected AimProvider _aimProvider;

    private Collider[] _hitColliders;
    private EntityProvider targetEntity;

    public override void InitializeAttack()
    {
        _attackProvider = transform.GetComponent<AttackProvider>();
        _aimProvider = transform.GetComponentInParent<AimProvider>();

        _hitColliders = new Collider[GameSettings.GetReference<Settings>().MaxTargetSize];
    }

    public override void Attack()
    {
        _attackProvider.GetData().IsArmed = true;

        var size = Physics.OverlapSphereNonAlloc(transform.position, AttackRange,
            _hitColliders, LayerMask);
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
        DOVirtual.DelayedCall(_aimProvider.GetData().AimDuration + DelayBeforeAttack, AimComplete);
    }

    private void AimComplete()
    {
        _attackProvider.GetData().IsArmed = false;

        Transform shootingPoint = GetNextShootingPoint();

        ProjectileBase tmpProjectile = ObjectFactory.CreateObject(projectileFx,
                null, shootingPoint.position, _aimProvider.GetData().GetAimRotation(shootingPoint, IsHoming))
            .GetComponent<ProjectileBase>();

        Destroy(tmpProjectile.gameObject, 3f);

        tmpProjectile.OnProjectileCollision = ProjectileHitCallback;
    }

    private void ProjectileHitCallback(EntityProvider collisionEntity)
    {
        if (collisionEntity == null) return;

        if (collisionEntity.Entity.Has<CachedDamageComponent>())
        {
            ref var cachedDamageComponent =
                ref collisionEntity.Entity.GetComponent<CachedDamageComponent>();
            cachedDamageComponent.DamageCached += AttackDamage;
            cachedDamageComponent.HitFx = HitFx;
        }
        else
        {
            collisionEntity.Entity.AddComponent<CachedDamageComponent>();
            ref var cachedDamageComponent =
                ref collisionEntity.Entity.GetComponent<CachedDamageComponent>();
            cachedDamageComponent.DamageCached += AttackDamage;
            cachedDamageComponent.HitFx = HitFx;
        }


        if (collisionEntity != null && collisionEntity.Entity.Has<TargetedTagComponent>())
        {
            collisionEntity.Entity.RemoveComponent<TargetedTagComponent>();
        }
    }

    //move this to system
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

        if (tmpEntity != null && !tmpEntity.Entity.Has<TargetedTagComponent>())
        {
            tmpEntity.Entity.AddComponent<TargetedTagComponent>();
        }

        return tmpEntity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}