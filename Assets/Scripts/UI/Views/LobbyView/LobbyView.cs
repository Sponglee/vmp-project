using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    public Transform container;

    public Button StartButton;


    private void Awake()
    {
    }

    private void OnDestroy()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}