using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    public GameObject healthBarItemPrefab;
    public Transform container;
    public List<HealthBarItem> HealthBarItems = new List<HealthBarItem>();
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}