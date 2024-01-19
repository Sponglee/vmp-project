using System.ComponentModel;
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
    [field: SerializeField] public Transform TurretLookTarget { get; set; }
    [field: SerializeField] public float LookTimer { get; set; }

    [field: SerializeField] public bool IsRotationInProgress { get; set; }
    public Tween RotationTween { get; set; }
    public Quaternion StartRotation { get; set; }

    public Transform TurretPivot;
    [FormerlySerializedAs("TurretRotationDuration")] public float AimDuration;
    public float TurretRotationResetTime;

    public void SetRotateTarget(Transform aTarget)
    {
        TurretLookTarget = aTarget;
        StartRotation = TurretPivot.rotation;
        LookTimer = 0f;
        IsRotationInProgress = true;
    }
}