using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anthill.Core;
using Anthill.Inject;
using Cinemachine;
using Scellecs.Morpeh;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [Inject] public Game Game { get; set; }

    public GameSettings GameSettings;
    public bool IsPaused = true;


    public class GameFinishedEvent : UnityEvent<bool>
    {
    };

    public static GameFinishedEvent OnGameFinished = new GameFinishedEvent();

    public class MenuOpenEvent : UnityEvent<bool>
    {
    };

    public static MenuOpenEvent OnMenuOpen = new MenuOpenEvent();

    public async void SlowTime(float targetScale, float targetDuration)
    {
        Time.timeScale = targetScale;
        await Task.Delay(Mathf.RoundToInt(targetDuration) * 1000);
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        AntInject.Inject<GameManager>(this);
    }

    private void Start()
    {
        PauseGame();
        IsPaused = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                StartGame();
                IsPaused = false;
            }
            else
            {
                PauseGame();
                IsPaused = true;
            }
        }
    }


    public void StartGame()
    {
        Game.StateMachine.ChangeState(StateEnum.PlayState);

        Game.ScenarioManager.EnableScenario("Gameplay");
        Game.ScenarioManager.EnableScenario("Input");
        Game.ScenarioManager.EnableScenario("UI");
    }

    public void PauseGame()
    {
        Game.StateMachine.ChangeState(StateEnum.PausedState);

        Game.ScenarioManager.DisableScenario("Gameplay");
        Game.ScenarioManager.DisableScenario("Input");
        Game.ScenarioManager.DisableScenario("UI");
    }

    public void LevelComplete()
    {
    }

    public void GameOver()
    {
        Game.StateMachine.ChangeState(StateEnum.FinishState);

        Game.ScenarioManager.DisableScenario("Gameplay");
        Game.ScenarioManager.DisableScenario("Input");
        Game.ScenarioManager.DisableScenario("UI");
    }
}