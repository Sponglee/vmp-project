using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct MovementComponent : IComponent
{
    [field: SerializeField] public float HorizontalInput { get; set; }
    [field: SerializeField] public float VerticalInput { get; set; }

    public float Speed;
}