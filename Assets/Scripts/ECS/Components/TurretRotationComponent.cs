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
    [SerializeField] private float turretRotationSpeed;
    private Tween _rotationTween;

    public void ResetTurret()
    {
        _rotationTween?.Kill();
        _rotationTween = TurretPivot.DOLocalRotate(Vector3.zero, turretRotationSpeed);
    }

    public void RotateToTarget(Transform aTarget)
    {
        var turretPivotPosition = TurretPivot.position;

        Vector3 lookRotation = (aTarget.position - turretPivotPosition).normalized;
        _rotationTween?.Kill();
        _rotationTween = TurretPivot.DOLookAt(turretPivotPosition + lookRotation, turretRotationSpeed);
    }
}