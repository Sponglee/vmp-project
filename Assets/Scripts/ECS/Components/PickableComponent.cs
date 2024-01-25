using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct PickableComponent : IComponent
{
    [field: SerializeField] public float ExpValue { get; set; }
    [field: SerializeField] public float PickupDuration { get; set; }
    [field: SerializeField] public float PickupRadius { get; set; }
    [field: SerializeField] public float PickupCurveHeight { get; set; }
    [field: SerializeField] public AnimationCurve PickupVerticalCurve { get; set; }

    public float Timer { get; set; }
    public Transform TargetTransform { get; set; }
    public Vector3 StartPosition { get; set; }
    public bool IsPickedUp { get; set; }
    public bool IsTargetReached { get; set; }
    public bool IsInitialized { get; set; }
}