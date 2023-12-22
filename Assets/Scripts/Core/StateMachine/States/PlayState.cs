using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.Core;
using UnityEngine;

public class PlayState : State
{
    public Action<Transform> onSelectCharacter;
    public Action onUserActionStart;
    public Action onUserActionHold;
    public Action onUserInputEnd;


    public PlayState(StateMachine stateMachine) : base(stateMachine)
    {
        //Maybe initialize joystick here and disabling it later?
    }

    public override void Enter()
    {
        Game.CameraManager.SetLive("playCam");
        // Game.CameraManager.AssignFollowTarget(Game.PlayerController.transform);
        // Game.CameraManager.AssignLookAtTarget(Game.PlayerController.transform);

        AntEngine.Get<Gameplay>().Enable();
        base.Enter();
    }

    public override void Exit()
    {
        AntEngine.Get<Gameplay>().Disable();
        base.Exit();
    }

    public override void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onUserActionStart?.Invoke();

            // Debug.Log("SELECTION");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 200f, Color.magenta, 10f);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 7))
            {
                // Debug.Log("SELECTEFD " + hit.transform.name);
                onSelectCharacter?.Invoke(hit.transform);
            }

        }
        else if (Input.GetMouseButton(0))
        {
            onUserActionHold?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onUserInputEnd?.Invoke();
        }
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
