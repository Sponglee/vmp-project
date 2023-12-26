using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : AttackBase
{
    public SwingEmitter SwingEmitter;

    public override void InitializeAttack(float attackRadius)
    {
        if (SwingEmitter != null)
            SwingEmitter.Initialize(attackRadius);
    }

    public override void Attack()
    {
        if (SwingEmitter != null)
            SwingEmitter.Emit();
        base.Attack();
    }
}