using Anthill.Core;
using Anthill.Inject;
using UnityEngine;

public class ExperienceBarController : ISystem
{
    [Inject] public Game Game { get; set; }
    private ExperienceBarView _view;
    private Camera _camera;

    public void AddedToEngine()
    {
        AntInject.Inject<ExperienceBarController>(this);
        _view = ObjectFactory.Create<ExperienceBarView>("UI/ExperienceBarView/ExperienceBarView", Game.UIRoot);
        _camera = Camera.main;
        _view.Hide();
    }

    public void RemovedFromEngine()
    {
        throw new System.NotImplementedException();
    }


    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    public void UpdateBarValue(float aValue)
    {
        _view.ExperienceBarItem.UpdateBarValue(aValue);
    }
}