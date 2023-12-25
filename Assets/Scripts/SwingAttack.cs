using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : AttackBase
{
    public SwingEmitter SwingEmitter;

    public override void InitializeAttack(float attackRadius)
    {
        SwingEmitter.Initialize(attackRadius);
    }

    public override void Attack()
    {
        base.Attack();
        SwingEmitter.Emit();
    }
}