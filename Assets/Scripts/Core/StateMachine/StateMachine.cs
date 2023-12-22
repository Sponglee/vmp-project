using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    #region Events
    public EventHandler<State> OnStateChange;
    #endregion
    public StateEnum ActiveState;
    public State CurrentState { get; private set; }
    //States
    public StartState StartState { get; private set; }
    public PlayState PlayState { get; private set; }
    public FinishState FinishState { get; private set; }
    public PausedState PausedState { get; private set; }

    public void Initialize()
    {
        // Initializing state machine with all possible states
        StartState = new StartState(this);
        PlayState = new PlayState(this);
        FinishState = new FinishState(this);
        PausedState = new PausedState(this);
        SetUpStartState(ActiveState);
    }

    private void Update()
    {
        // Handling current state update calls
        CurrentState.HandleInput();
        CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        // Handling current state fixed update calls
        CurrentState.PhysicsUpdate();
    }

    public void SetUpStartState(StateEnum startingState)
    {
        State state = StateEnumToState(startingState);
        CurrentState = state;
        state.Enter();
        OnStateChange?.Invoke(this, CurrentState);
    }

    public StateEnum CheckState()
    {
        State state = CurrentState;
        StateEnum stateEnum = StateEnum.StartState;
        switch (state)
        {
            case StartState _:
                stateEnum = StateEnum.StartState;
                break;
            case PlayState _:
                stateEnum = StateEnum.PlayState;
                break;
            case FinishState _:
                stateEnum = StateEnum.FinishState;
                break;
            case PausedState _:
                stateEnum = StateEnum.PausedState;
                break;
        }
        return stateEnum;
    }

    public void ChangeState(StateEnum value)
    {
        ActiveState = value;
        ChangeState(StateEnumToState(value));
    }

    private void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();

        OnStateChange?.Invoke(this, CurrentState);
    }

    private State StateEnumToState(StateEnum defaultState)
    {
        State state = null;

        switch (defaultState)
        {
            case StateEnum.StartState:
                state = StartState;
                break;
            case StateEnum.PlayState:
                state = PlayState;
                break;
            case StateEnum.FinishState:
                state = FinishState;
                break;
        }
        return state;
    }

}
