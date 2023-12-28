using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceButtonItem : MonoBehaviour
{
    public ChoiceData ChoiceDataRef;
    public Button SelectButton;

    public event Action<ChoiceButtonItem> ChoiceItemClicked;

    private void Awake()
    {
        SelectButton.clicked += ClickHandler;
    }

    private void OnDestroy()
    {
        SelectButton.clicked -= ClickHandler;
    }
    
    private void ClickHandler()
    {
        ChoiceItemClicked?.Invoke(this);
    }
}