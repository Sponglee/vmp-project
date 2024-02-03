using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class RocketProjectile : ProjectileBase
{
    public float InitialLaunchDuration = .25f;
    public float InitialLaunchSpeed = 50f;

    private Transform targetCached;
    private Vector3 destinationCached;

    private Vector3 launchEndPosition;

    protected override void Update()
    {
        if (!IsInitialized)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * InitialLaunchSpeed), Space.Self);
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
        launchEndPosition = transform.forward * (InitialLaunchSpeed * InitialLaunchDuration);
        MovementTimer = 0f;
    }

    public override void InitializeProjectile(Vector3 aProjectileDestination)
    {
        destinationCached = aProjectileDestination;
        launchEndPosition = transform.forward * (InitialLaunchSpeed * InitialLaunchDuration);
        MovementTimer = 0f;
    }
}