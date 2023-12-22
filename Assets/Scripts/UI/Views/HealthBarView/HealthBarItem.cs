using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarItem : MonoBehaviour
{

    public Image fill;
    public Slider slider;
    public bool Active = false;

    private Camera camRef;
    private Transform _t;
    
    
    private void Awake()
    {
        _t = transform;
    }

    public void UpdatePosition(Vector3 aPosition)
    {
        _t.position = camRef.WorldToScreenPoint(aPosition);
    }

    public void UpdateHealthBar(float hpValue)
    {
        slider.value = hpValue;
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