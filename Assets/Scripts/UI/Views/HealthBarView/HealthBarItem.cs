using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarItem : MonoBehaviour
{
    public Image fill;
    public bool Active = false;
    
    private Transform _t;
    
    public void UpdatePosition(Vector3 aPosition)
    {
        _t ??= transform;

        _t.position = aPosition;
    }

    public void UpdateHealthBar(float aValue)
    {
        fill.fillAmount = aValue;
    }

    public void SetKind(HealthBarKind aKind)
    {
        switch (aKind)
        {
            case HealthBarKind.Player:
                fill.color = Color.green;
                break;
            case HealthBarKind.Enemy:
                fill.color = Color.red;
                break;
        }
    }
}