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

        _view.Hide();
    }

    public void RemovedFromEngine()
    {
    }

    public void InitializeChoice(ChoiceData[] aChoiceDatas)
    {
        ClearButtons();

        for (int i = 0; i < aChoiceDatas.Length; i++)
        {
            ChoiceButtonItem tmpItem =
                ObjectFactory.CreateObject(_view.choiceDataPref, _view.container).GetComponent<ChoiceButtonItem>();

            tmpItem.InitializeChoiceItem(aChoiceDatas[i]);
            tmpItem.ChoiceItemClicked += OnChoicePickedHandler;
            _view.ChoiceButtonItems.Add(tmpItem);
        }

        _view.DynamicGridLayout.UpdateGridLayout();
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    private void ClearButtons()
    {
        for (int i = _view.ChoiceButtonItems.Count - 1; i >= 0; i--)
        {
            _view.ChoiceButtonItems[i].ChoiceItemClicked -= OnChoicePickedHandler;

            GameObject.Destroy(_view.ChoiceButtonItems[i].gameObject);
        }
    }

    private void OnChoicePickedHandler(ChoiceButtonItem aChoice)
    {
        Game.GameManager.StartGameplay();
    }
}