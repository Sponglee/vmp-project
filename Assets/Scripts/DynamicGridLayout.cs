using System;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public int spacing;

    private int columns;

    private void OnValidate()
    {
        UpdateLayourGroup();
    }

    public void UpdateLayourGroup()
    {
        columns = gridLayoutGroup.transform.childCount;

        float containerWidth = gridLayoutGroup.GetComponent<RectTransform>().rect.width;
        float cellWidth = (containerWidth - ((columns - 1) * spacing)) / columns;

        gridLayoutGroup.cellSize = new Vector2(cellWidth, gridLayoutGroup.cellSize.y);
    }
}