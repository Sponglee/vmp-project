using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : State
{

    public PausedState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Game.CameraManager.SetLive("pauseCam");
        // CameraManager.Instance.AssignFollowTarget(trajectoryController.transform);
        // CameraManager.Instance.AssignLookAtTarget(trajectoryController.transform);
        base.Enter();
    }

    public override void Exit()
    {
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
