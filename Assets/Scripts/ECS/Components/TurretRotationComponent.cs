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


    public void RotateToTarget(Transform aTarget)
    {
        TurretPivot.LookAt(aTarget.position);
    }
}