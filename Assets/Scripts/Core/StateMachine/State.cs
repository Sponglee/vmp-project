using Anthill.Inject;

public abstract class State
{
    [Inject] public Game Game { get; set; }
    #region Constructor
    protected State(StateMachine stateMachine)
    {
        AntInject.Inject<State>(this);
        _stateMachine = stateMachine;
    }
    #endregion

    #region Fields
    protected StateMachine _stateMachine;
    #endregion

    public virtual void Enter()
    {
        // trajectoryController = GameManager.Instance.TrajectoryController;
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
    }
}