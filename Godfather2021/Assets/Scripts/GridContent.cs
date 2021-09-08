using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridContent : MonoBehaviour
{
    public int rows = 1;
    public int cols = 1;
    public GameObject inputFieldPrefab;
    public bool fixedCellSize = false;
    public Vector2 cellSize = Vector2.zero;
    public Vector2 reference = Vector2.zero;

    void Start()
    {
        RectTransform parentRect = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup gridLayout = gameObject.GetComponent<GridLayoutGroup>();
        if (!fixedCellSize)
        {
            gridLayout.cellSize = new Vector2((parentRect.rect.width - gridLayout.padding.left - gridLayout.padding.right - (cols * gridLayout.spacing.x)) / cols, (parentRect.rect.height - gridLayout.padding.top - gridLayout.padding.bottom - (rows * gridLayout.spacing.y)) / rows);
        }
        //else
        //{
        //    float tempX = 0;
        //    float tempY = 0;
        //    tempX = (cellSize.x * parentRect.rect.width) / reference.x;
        //    tempX = (cellSize.y * parentRect.rect.height) / reference.y;
        //}
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject inputField = Instantiate(inputFieldPrefab);
                inputField.transform.SetParent(gameObject.transform, false);
            }
        }
    }
}
