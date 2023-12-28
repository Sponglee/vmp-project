using System.Collections;
using System.Collections.Generic;
using Anthill.Core;
using UnityEngine;

public class ChoiceState : State
{
    public ChoiceState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Game.CameraManager.SetLive("pauseCam");

        var menu = AntEngine.Get<Menu>();
        menu.Get<ChoiceController>().Show();
        menu.Get<ChoiceController>().InitializeChoice(GameSettings.GetReference<ChoiceLibrary>().GetChoices());
        base.Enter();
    }

    public override void Exit()
    {
        var menu = AntEngine.Get<Menu>();
        menu.Get<ChoiceController>().Hide();
        base.Exit();
    }

    public override void HandleInput()
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}