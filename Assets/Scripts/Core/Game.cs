using Anthill.Core;
using Anthill.Inject;
using Scellecs.Morpeh;
using UnityEngine;

public static class Priority
{
    public const int Gameplay = 0;
    public const int Menu = 1;
}

public class Game : AntAbstractBootstrapper
{
#region AntAbstractBootstrapper Implementation

    public Transform UIRoot;
    public StateMachine StateMachine;
    public CameraManager CameraManager;
    public GameManager GameManager;
    public ExperienceManager ExperienceManager;
    public ScenarioManager ScenarioManager;


    private const string playCam = "playCam";

    public override void Configure(IInjectContainer aContainer)
    {
        aContainer.RegisterSingleton<Game>(this);
        aContainer.RegisterSingleton<StateMachine>(StateMachine);
        aContainer.RegisterSingleton<CameraManager>(CameraManager);
        aContainer.RegisterSingleton<GameManager>(GameManager);
        aContainer.RegisterSingleton<ExperienceManager>(ExperienceManager);
        aContainer.RegisterSingleton<ScenarioManager>(ScenarioManager);
    }

#endregion

#region Unity Calls

    private void Start()
    {
        AntEngine.Add<Menu>(Priority.Menu);

        StateMachine.Initialize();

        StateMachine.ChangeState(StateEnum.StartState);
    }

#endregion
}