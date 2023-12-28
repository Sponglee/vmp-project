using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceView : MonoBehaviour
{
    public GameObject choiceDataPref;

    public event Action<ChoiceData> OnChoicePicked;

    public Transform container;

    public List<ChoiceButtonItem> ChoiceButtonItems = new List<ChoiceButtonItem>();


    public void InitializeButtons(ChoiceData[] aChoiceDatas)
    {
        ClearButtons();

        ChoiceButtonItem tmpItem =
            ObjectFactory.CreateObject(choiceDataPref, container).GetComponent<ChoiceButtonItem>();

        tmpItem.ChoiceItemClicked += ChoiceHandler;
        ChoiceButtonItems.Add(tmpItem);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ClearButtons()
    {
        for (int i = ChoiceButtonItems.Count - 1; i >= 0; i--)
        {
            ChoiceButtonItems[i].ChoiceItemClicked -= ChoiceHandler;

            Destroy(ChoiceButtonItems[i].gameObject);
        }
    }

    private void ChoiceHandler(ChoiceButtonItem aChoice)
    {
        OnChoicePicked?.Invoke(aChoice.ChoiceDataRef);
    }
}