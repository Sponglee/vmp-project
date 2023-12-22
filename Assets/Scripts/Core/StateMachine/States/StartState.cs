using System.Collections;
using System.Collections.Generic;
using Anthill.Core;
using UnityEngine;

public class StartState : State
{

    public StartState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Game.CameraManager.SetLive("startCam");
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {

        base.HandleInput();
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
