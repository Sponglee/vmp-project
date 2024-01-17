using DG.Tweening;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct TurretRotationComponent : IComponent
{
    public Transform TurretPivot;

    private Tween rotationTween;

    [SerializeField] private float turretRotationSpeed;

    public void ResetTurret()
    {
        rotationTween?.Kill();
        rotationTween = TurretPivot.DOLocalRotate(Vector3.zero, turretRotationSpeed);
    }

    public void RotateToTarget(Transform aTarget)
    {
        Vector3 lookRotation = (aTarget.position - TurretPivot.position).normalized;
        rotationTween?.Kill();
        rotationTween = TurretPivot.DOLookAt(TurretPivot.position + lookRotation, turretRotationSpeed);
    }
}