using Anthill.Core;
using Anthill.Inject;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ExperienceSystem))]
public sealed class ExperienceSystem : UpdateSystem
{
    private Filter _experienceSourceFilter;
    private Filter _experienceFilter;
    private ExperienceBarController _experienceBarController;

    [Inject] public Game Game { get; set; }

    public override void OnAwake()
    {
        AntInject.Inject<ExperienceSystem>(this);

        _experienceBarController = AntEngine.Get<Menu>().Get<ExperienceBarController>();

        _experienceSourceFilter = World.Filter.With<PickableComponent>().With<TransformComponent>()
            .With<ExperienceSourceComponent>().Build();

        _experienceFilter = World.Filter.With<TransformComponent>()
            .With<ExperienceComponent>().With<PlayerTagComponent>().Build();

        foreach (var experienceEntity in _experienceFilter)
        {
            ref var experienceComponent = ref experienceEntity.GetComponent<ExperienceComponent>();

            InitializeExperienceComponents(ref experienceComponent);
        }
    }

    public override void OnUpdate(float deltaTime)
    {
        if (Game.GameManager.IsPaused) return;

        foreach (var experienceEntity in _experienceFilter)
        {
            ref var experienceComponent = ref experienceEntity.GetComponent<ExperienceComponent>();
            ref var transformComponent = ref experienceEntity.GetComponent<TransformComponent>();


            foreach (var experienceSourceEntity in _experienceSourceFilter)
            {
                ref var experienceSourceComponent =
                    ref experienceSourceEntity.GetComponent<ExperienceSourceComponent>();
                ref var pickableComponent = ref experienceSourceEntity.GetComponent<PickableComponent>();

                if (pickableComponent.IsTargetReached && !experienceSourceComponent.IsDepleted)
                {
                    experienceComponent.CurrentExperience += experienceSourceComponent.ExperienceGain;

                    _experienceBarController.UpdateBarValue(experienceComponent.CurrentExperience /
                                                            experienceComponent.MaxExperience);


                    Destroy(
                        ObjectFactory.CreateObject(experienceSourceComponent.ExpFx, transformComponent.Transform,
                            transformComponent.Transform.position + Vector3.up),
                        3f);

                    experienceSourceComponent.IsDepleted = true;

                    if (experienceComponent.CurrentExperience >= experienceComponent.MaxExperience)
                    {
                        Destroy(
                            ObjectFactory.CreateObject(experienceComponent.LevelUpFx, transformComponent.Transform,
                                transformComponent.Transform.position),
                            3f);

                        LevelUp(ref experienceComponent);
                        Game.GameManager.ChoiceWindow();
                    }
                }
            }
        }
    }

    private void LevelUp(ref ExperienceComponent aExperienceComponent)
    {
        Game.ExperienceManager.LevelUp(ref aExperienceComponent);
        InitializeExperienceComponents(ref aExperienceComponent);
    }

    private void InitializeExperienceComponents(ref ExperienceComponent aExperienceComponent)
    {
        Game.ExperienceManager.InitializeExperience(ref aExperienceComponent);
        _experienceBarController.UpdateBarValue(aExperienceComponent.CurrentExperience /
                                                aExperienceComponent.MaxExperience);
    }
}