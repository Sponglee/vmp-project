using DG.Tweening;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using IComponent = Scellecs.Morpeh.IComponent;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct AimComponent : IComponent
{
    [field: SerializeField] public bool StopWhileAiming { get; set; }
    [field: SerializeField] public Transform TurretLookTarget { get; set; }
    [field: SerializeField] public float LookTimer { get; set; }
    [field: SerializeField] public float ResetTimer { get; set; }

    [field: SerializeField] public bool IsRotationInProgress { get; set; }
    public Quaternion StartRotation { get; set; }

    public Transform TurretPivot;

    public float AimDuration;

    public float TurretRotationResetTime;

    public void SetRotateTarget(Transform aTarget)
    {
        TurretLookTarget = aTarget;
        StartRotation = TurretPivot.rotation;
        LookTimer = 0f;
        ResetTimer = 0f;
        IsRotationInProgress = true;
    }

    public Quaternion GetAimRotation(Transform aPoint)
    {
        var dir = TurretPivot.forward;

        return Quaternion.LookRotation(dir, Vector3.up);
    }

    public Quaternion GetAimRotation(Transform aPoint, bool IsHoming)
    {
        Vector3 dir = default;

        if (IsHoming && TurretLookTarget != null)
        {
            dir = (TurretLookTarget.position -
                   aPoint.position) + Vector3.up * .5f;
        }
        else
        {
            dir = aPoint.forward; //TurretPivot.forward;
        }


        return Quaternion.LookRotation(dir, Vector3.up);
    }
}