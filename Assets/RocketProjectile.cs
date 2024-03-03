using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class RocketProjectile : ProjectileBase
{
    public float InitialLaunchDuration = .25f;
    public Vector2 InitialLaunchSpeed;

    private Transform targetCached;
    private Vector3 destinationCached;

    private Vector3 launchEndPosition;

    private Quaternion startRotation;

    private Transform _t;
    private Vector3 _forward;

    private void Awake()
    {
        _t = transform;
        _forward = _t.forward;
    }

    protected override void Update()
    {
        if (!IsInitialized)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * InitialLaunchSpeed.x),
                Space.Self);

            _t.rotation = Quaternion.Lerp(startRotation,
                Quaternion.LookRotation((endPosition - _t.position).normalized, Vector3.up),
                MovementTimer / InitialLaunchDuration);

            MovementTimer += Time.deltaTime;

            if (MovementTimer > InitialLaunchDuration)
            {
                if (targetCached != null)
                {
                    base.InitializeProjectile(targetCached);
                }
                else
                {
                    base.InitializeProjectile(destinationCached);
                }
            }

            return;
        }


        base.Update();
    }

    public override void InitializeProjectile(Transform aProjectileTarget)
    {
        targetCached = aProjectileTarget;
        startRotation = _t.rotation;
        // startPosition = _t.position;
        endPosition = aProjectileTarget.transform.position;
        MovementTimer = 0f;
    }

    public override void InitializeProjectile(Vector3 aProjectileDestination)
    {
        destinationCached = aProjectileDestination;
        startRotation = _t.rotation;
        // startPosition = _t.position;
        endPosition = aProjectileDestination;
        MovementTimer = 0f;
    }

    protected override void UpdateProjectileRotation()
    {
        Debug.DrawLine(transform.position, endPosition);

        transform.rotation =
            Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(endPosition - startPosition), .5f);

        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(
        //     Vector3.Lerp(startPosition,
        //         endPosition +
        //         Vector3.up * FlightTrajectoryY.Evaluate(MovementTimer / ProjectileFlightTime),
        //         MovementTimer / ProjectileFlightTime)), .05f);
    }
}