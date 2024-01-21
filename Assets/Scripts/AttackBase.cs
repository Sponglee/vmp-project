using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public class AttackBase : MonoBehaviour, IAttack
{
    protected AttackProvider _attackProvider;
    [field: SerializeField] public LayerMask LayerMask { get; set; }
    [field: SerializeField] public GameObject HitFx { get; set; }
    [field: SerializeField] public float AttackDamage { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float AttackCooldown { get; set; }

    public Transform[] ShootingPoints;

    private int currentShootingPoint = 0;

    public virtual void InitializeAttack()
    {
    }


    public virtual void Attack()
    {
    }

    protected Transform GetNextShootingPoint()
    {
        Transform tmpPoint = ShootingPoints[currentShootingPoint];
        currentShootingPoint++;
        if (currentShootingPoint >= ShootingPoints.Length)
            currentShootingPoint = 0;

        return tmpPoint;
    }
}