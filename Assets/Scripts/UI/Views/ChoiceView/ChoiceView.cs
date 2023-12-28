using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceView : MonoBehaviour
{
    public GameObject choiceDataPref;

    public Transform container;
    public DynamicGridLayout DynamicGridLayout;

    public List<ChoiceButtonItem> ChoiceButtonItems = new List<ChoiceButtonItem>();


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}