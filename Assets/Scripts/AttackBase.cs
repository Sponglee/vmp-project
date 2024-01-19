using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class AttackBase : MonoBehaviour, IAttack
{
    protected AttackProvider _attackProvider;

    public virtual void InitializeAttack()
    {
    }


    public virtual void Attack()
    {
    }
}