using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwingEmitter : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    private float attackRadius;

    public float AttackRadius
    {
        get { return attackRadius; }
        set { attackRadius = value; }
    }

    public void Initialize(float aRadius)
    {
        AttackRadius = aRadius;
        UpdateParticleSystem();
    }

    public void Emit()
    {
        ParticleSystem.Play();
    }

    private void UpdateParticleSystem()
    {
        ParticleSystem.transform.localScale = Vector3.one * AttackRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    private void OnValidate()
    {
        UpdateParticleSystem();
    }
}