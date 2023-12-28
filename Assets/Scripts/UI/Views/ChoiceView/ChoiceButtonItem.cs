using System;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceButtonItem : MonoBehaviour
{
    [HideInInspector] public ChoiceData ChoiceDataRef;
    public Button SelectButton;
    public Image ChoiceIcon;

    public TMPro.TMP_Text ChoiceDescription;

    public event Action<ChoiceButtonItem> ChoiceItemClicked;

    private void Awake()
    {
        SelectButton.onClick.AddListener(ClickHandler);
    }

    private void OnDestroy()
    {
        SelectButton.onClick.RemoveListener(ClickHandler);
    }

    public void InitializeChoiceItem(ChoiceData aChoiceData)
    {
        ChoiceDataRef = aChoiceData;
        ChoiceIcon.sprite = ChoiceDataRef.AbilityIcon;
        ChoiceDescription.text = ChoiceDataRef.AbilityDescription;
    }


    private void ClickHandler()
    {
        ChoiceItemClicked?.Invoke(this);
    }
}