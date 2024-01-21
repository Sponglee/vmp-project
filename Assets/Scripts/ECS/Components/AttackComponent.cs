using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct AttackComponent : IComponent
{
    public AttackBase Attack;
    public bool IsInitialized { get; set; }
    public bool IsArmed;

    public float AttackTimer { get; set; }
}