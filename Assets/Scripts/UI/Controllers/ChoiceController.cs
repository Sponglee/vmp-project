using System;
using Anthill.Core;
using Anthill.Inject;
using UnityEngine;

public class ChoiceController : ISystem
{
    [Inject] public Game Game { get; set; }
    private ChoiceView _view;
    private Camera _camera;


    public void AddedToEngine()
    {
        AntInject.Inject<ChoiceController>(this);
        _view = ObjectFactory.Create<ChoiceView>("UI/ChoiceView/ChoiceView", Game.UIRoot);
        _camera = Camera.main;

        _view.OnChoicePicked += OnChoicePickedHandler;

        _view.Hide();
    }

    public void RemovedFromEngine()
    {
        _view.OnChoicePicked -= OnChoicePickedHandler;
    }


    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }


    private void OnChoicePickedHandler(ChoiceData aChoice)
    {
        Game.GameManager.StartGameplay();
    }
}