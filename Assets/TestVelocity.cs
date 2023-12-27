using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestVelocity : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float Force = 100f;

    public ForceMode ForceMode;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Rigidbody.AddForce(Vector3.right * Force, ForceMode);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            Rigidbody.AddForce(Vector3.up * Force, ForceMode);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position,
            transform.position + Rigidbody.velocity * Rigidbody.velocity.magnitude * Force, Color.red);
    }
}