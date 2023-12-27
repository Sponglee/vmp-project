using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct ExperienceSourceComponent : IComponent
{
    [field: SerializeField] public float ExperienceGain;
    [field: SerializeField] public GameObject ExpFx { get; set; }
    [field: SerializeField] public bool IsDepleted { get; set; }
}