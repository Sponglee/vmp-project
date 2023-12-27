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


#region private

#endregion

#region public

#endregion

#region properties

#endregion

#region UnityCalls

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

#endregion


    public void StartGame()
    {
        Game.StateMachine.ChangeState(StateEnum.PlayState);
    }

    public void PauseGame()
    {
        Game.StateMachine.ChangeState(StateEnum.PausedState);
    }

    public void LevelComplete()
    {
    }

    public void GameOver()
    {
        

        for (var i = 0; i < Game.Installer.updateSystems.Length; i++)
        {
            Game.Installer.updateSystems[i].Enabled = false;
        }

        for (var i = 0; i < Game.Installer.fixedUpdateSystems.Length; i++)
        {
            Game.Installer.fixedUpdateSystems[i].Enabled = false;
        }
    }


#region Private Methods

#endregion
}