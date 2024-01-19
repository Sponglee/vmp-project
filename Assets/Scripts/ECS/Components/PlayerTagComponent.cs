using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct PlayerTagComponent : IComponent
{
    [field: SerializeField] public CharacterTag CharacterTag { get; set; }
}

public enum CharacterTag
{
    Player,
    Enemy
}