using System;
using System.Collections;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    public delegate void ProjectileCollisionDelegate(EntityProvider entityProvider);


    public ProjectileCollisionDelegate OnProjectileCollision;


    private void OnParticleCollision(GameObject other)
    {
        var entity = other.GetComponent<EntityProvider>();

        if (entity == null) return;


        OnProjectileCollision?.Invoke(entity);
    }
}