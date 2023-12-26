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
    public LayerMask LayerMask;
    [field: SerializeField] public float AttackRadius { get; set; }
    [field: SerializeField] public float AttackDamage { get; set; }
    [field: SerializeField] public float AttackCooldown { get; set; }
    public bool IsInitialized { get; set; }
    public float AttackTimer { get; set; }
    public AttackBase Attack;
}