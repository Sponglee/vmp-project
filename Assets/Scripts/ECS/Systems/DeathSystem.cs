using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DeathSystem))]
public sealed class DeathSystem : UpdateSystem
{
    private Filter _deadFilter;

    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<DeathSystem>(this);
        this._deadFilter = this.World.Filter.With<DeathComponent>().With<TransformComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in this._deadFilter)
        {
            ref var deathComponent = ref entity.GetComponent<DeathComponent>();
            ref var transformComponent = ref entity.GetComponent<TransformComponent>();

            if (!deathComponent.IsInitialized)
            {
                // deathComponent.DeathDelay = GameSettings.DeathDelay;
                deathComponent.IsInitialized = true;
            }

            deathComponent.Timer += deltaTime;


            if (deathComponent.Timer >= deathComponent.DeathDelay)
            {
                Destroy(transformComponent.Transform.gameObject);
            }
        }
    }
}