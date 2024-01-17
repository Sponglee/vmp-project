using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class AttackBase : MonoBehaviour, IAttack
{
    public virtual void InitializeAttack()
    {
    }

    public virtual void Attack(Entity entity)
    {
    }
}