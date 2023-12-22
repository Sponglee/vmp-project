using Anthill.Core;
using Anthill.Inject;
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

    public override void Configure(IInjectContainer aContainer)
    {
        aContainer.RegisterSingleton<Game>(this);
        aContainer.RegisterSingleton<StateMachine>(StateMachine);
        aContainer.RegisterSingleton<CameraManager>(CameraManager);
        aContainer.RegisterSingleton<GameManager>(GameManager);
        // .. конфигурация DI Container.
    }

    #endregion

    #region Unity Calls

    private void Start()
    {
        AntEngine.Add<Menu>(Priority.Menu);

        // InitializeScenarios();

        StateMachine.Initialize();
    }

#endregion

    #region Private Methods

    private void InitializeScenarios()
    {
        AntEngine.Add<Gameplay>(Priority.Gameplay);
      
        // .. инициализация других систем
    }



    #endregion
}