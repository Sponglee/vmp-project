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

    public async void InitializeChoice(ChoiceData[] aChoiceDatas)
    {
        for (int i = 0; i < aChoiceDatas.Length; i++)
        {
            ChoiceButtonItem tmpItem =
                ObjectFactory.CreateObject(_view.choiceDataPref, _view.container).GetComponent<ChoiceButtonItem>();

            tmpItem.InitializeChoiceItem(aChoiceDatas[i]);
            tmpItem.ChoiceItemClicked += OnChoicePickedHandler;

            if (!_view.ChoiceButtonItems.Contains(tmpItem))
                _view.ChoiceButtonItems.Add(tmpItem);
        }


        _view.DynamicGridLayout.UpdateGridLayout();
    }

    public void Show()
    {
        _view.Show();
        ClearButtons();
    }

    public void Hide()
    {
        _view.Hide();
    }

    private void ClearButtons()
    {
        for (int i = _view.container.childCount - 1; i >= 0; i--)
        {
            ChoiceButtonItem tmpItem = _view.container.GetChild(i).GetComponent<ChoiceButtonItem>();
            tmpItem.ChoiceItemClicked -= OnChoicePickedHandler;
            GameObject.Destroy(tmpItem.gameObject);
        }

        _view.ChoiceButtonItems.Clear();
    }

    private void OnChoicePickedHandler(ChoiceButtonItem aChoice)
    {
        Game.GameManager.StartGameplay();
        ClearButtons();
    }
}