using Anthill.Core;
using Anthill.Inject;
using UnityEngine;

public class LobbyController : ISystem
{
    [Inject] public Game Game { get; set; }
    private LobbyView _view;
    private Camera _camera;

    public void AddedToEngine()
    {
        AntInject.Inject<LobbyController>(this);
        _view = ObjectFactory.Create<LobbyView>("UI/LobbyView/LobbyView", Game.UIRoot);
        _camera = Camera.main;
        _view.Hide();

        _view.StartButton.onClick.AddListener(OnStartClickedHandler);
    }

    public void RemovedFromEngine()
    {
        _view.StartButton.onClick.RemoveListener(OnStartClickedHandler);
    }


    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }


    private void OnStartClickedHandler()
    {
        Game.GameManager.StartGameplay();
    }
}