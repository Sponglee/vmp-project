using UnityEngine;

public class AttackBase : MonoBehaviour, IAttack
{
    public virtual void InitializeAttack(float AttackRadius)
    {
    }

    public virtual void Attack(AttackComponent aAttackComponent)
    {
    }
}