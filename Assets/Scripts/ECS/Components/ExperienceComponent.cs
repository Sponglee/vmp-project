using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ExperienceComponent : IComponent
{
    [field: SerializeField] public float Id { get; set; }
    [field: SerializeField] public GameObject LevelUpFx { get; set; }
    [field: SerializeField] public float CurrentExperience { get; set; }
    [field: SerializeField] public float MaxExperience { get; set; }
    [field: SerializeField] public int CurrentLevel { get; set; }
}