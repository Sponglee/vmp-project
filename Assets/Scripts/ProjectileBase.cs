using System;
using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public delegate void ProjectileCollisionDelegate(EntityProvider entityProvider);

    public ProjectileCollisionDelegate OnProjectileCollision;

    public bool IsInitialized = false;
    public float ProjectileFlightTime = 2f;

    public float MovementTimer = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Transform projectileTarget;


    private void Update()
    {
        if (!IsInitialized) return;

        if (projectileTarget != null)
        {
            endPosition = projectileTarget.position;
        }

        transform.position = Vector3.Lerp(startPosition, endPosition, MovementTimer / ProjectileFlightTime);
        transform.rotation =
            Quaternion.LookRotation(Vector3.Lerp(startPosition, endPosition,
                MovementTimer + .25f / ProjectileFlightTime) - transform.position, Vector3.up);

        MovementTimer += Time.deltaTime;

        if (MovementTimer / ProjectileFlightTime >= 1f)
        {
            IsInitialized = false;
            projectileTarget = null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<EntityProvider>();

        if (entity == null) return;


        OnProjectileCollision?.Invoke(entity);
        Destroy(gameObject);
    }

    public void InitializeProjectile(Transform aProjectileTarget)
    {
        MovementTimer = 0f;
        startPosition = transform.position;
        endPosition = aProjectileTarget.transform.position;
        projectileTarget = aProjectileTarget;

        IsInitialized = true;
    }

    public void InitializeProjectile(Vector3 aProjectileDestination)
    {
        MovementTimer = 0f;
        startPosition = transform.position;
        endPosition = aProjectileDestination;
        projectileTarget = null;

        IsInitialized = true;
    }
}