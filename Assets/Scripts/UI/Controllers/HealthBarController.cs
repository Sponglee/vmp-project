using Anthill.Core;
using Anthill.Inject;

public class HealthBarController : ISystem
{
    [Inject] public Game Game { get; set; }
    private HealthBarView _view;
    public void AddedToEngine()
    {
        AntInject.Inject<HealthBarController>(this);
        _view = ObjectFactory.Create<HealthBarView>("UI/HealthBarView/HealthBarView", Game.UIRoot);

        _view.Hide();
    }

    public void RemovedFromEngine()
    {
        throw new System.NotImplementedException();
    }
}
