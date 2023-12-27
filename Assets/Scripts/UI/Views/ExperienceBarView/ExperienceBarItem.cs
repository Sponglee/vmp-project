using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarItem : MonoBehaviour
{
    public Image fill;
    public bool Active = false;

    private Transform _t;

    public void UpdatePosition(Vector3 aPosition)
    {
        _t ??= transform;

        _t.position = aPosition;
    }

    public void UpdateBarValue(float aValue)
    {
        fill.fillAmount = aValue;
    }
}