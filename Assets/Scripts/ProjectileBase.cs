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
    public AnimationCurve FlightTrajectoryY;
    public float MovementTimer = 0f;

    protected Vector3 startPosition;
    protected Vector3 endPosition;
    protected Transform projectileTarget;


    protected virtual void Update()
    {
        if (!IsInitialized) return;

        UpdateProjectilePosition();
        UpdateProjectileRotation();

        MovementTimer += Time.deltaTime;

        if (MovementTimer / ProjectileFlightTime > 1f)
        {
            IsInitialized = false;
            projectileTarget = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var entity = other.GetComponent<EntityProvider>();

        if (entity != null)
        {
            OnProjectileCollision?.Invoke(entity);
        }

        Destroy(gameObject);
    }

    public virtual void InitializeProjectile(Transform aProjectileTarget)
    {
        MovementTimer = 0f;
        startPosition = transform.position;
        endPosition = aProjectileTarget.transform.position;
        projectileTarget = aProjectileTarget;

        IsInitialized = true;
    }

    public virtual void InitializeProjectile(Vector3 aProjectileDestination)
    {
        MovementTimer = 0f;
        startPosition = transform.position;
        endPosition = aProjectileDestination;
        projectileTarget = null;

        IsInitialized = true;
    }

    protected virtual void UpdateProjectilePosition()
    {
        if (projectileTarget != null)
        {
            endPosition = projectileTarget.position;
        }

        transform.position = Vector3.Lerp(startPosition,
            endPosition + Vector3.up * FlightTrajectoryY.Evaluate(MovementTimer / ProjectileFlightTime),
            MovementTimer / ProjectileFlightTime);
    }

    protected virtual void UpdateProjectileRotation()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.Lerp(startPosition,
            endPosition + Vector3.up * FlightTrajectoryY.Evaluate(MovementTimer / ProjectileFlightTime),
            MovementTimer + .25f / ProjectileFlightTime) - transform.position, Vector3.up);
    }
}