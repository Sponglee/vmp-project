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
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                StartGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void StartGame()
    {
        if (!IsPaused) return;
        IsPaused = false;

        // Game.ScenarioManager.EnableScenario("Level");
        // Game.ScenarioManager.EnableScenario("Input");
        // Game.ScenarioManager.EnableScenario("UI");

        Game.StateMachine.ChangeState(StateEnum.PlayState);
    }

    public void PauseGame()
    {
        if (IsPaused) return;
        IsPaused = true;

        // Game.ScenarioManager.DisableScenario("Level");
        // Game.ScenarioManager.DisableScenario("Input");
        // Game.ScenarioManager.DisableScenario("UI");

        Game.StateMachine.ChangeState(StateEnum.PausedState);
    }

    public void LevelComplete()
    {
    }

    public void GameOver()
    {
        if (IsPaused) return;
        IsPaused = true;

        // Game.ScenarioManager.DisableScenario("Level");
        // Game.ScenarioManager.DisableScenario("Input");
        // Game.ScenarioManager.DisableScenario("UI");

        Game.StateMachine.ChangeState(StateEnum.FinishState);
    }
}