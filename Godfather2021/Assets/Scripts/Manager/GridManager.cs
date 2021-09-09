using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{

    private GridLayoutGroup grid;
    private int column = 0;
    private int row = 0;
    private bool rowCounted = false;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject knife;
    [SerializeField] private TextAsset gridData;
    [SerializeField] private List<Ingredients> allIngredients = new List<Ingredients>();

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();

        GameManager.instance.onStateChange += () =>
        {
            if(GameManager.instance.state == GameState.TUTO)
            {
                GetColumnAndRow(grid, out column, out row);
                ReadGrid();
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        //if (!rowCounted && grid != null)
        //{
        //    rowCounted = true;
        //    GetColumnAndRow(grid, out column, out row);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ReadGrid();
        //}
    }

    void ReadGrid()
    {
        string[] data = gridData.text.Split(new char[] { '\n' });

        for (int i = 0; i < data.Length; i++)
        {
            string[] row = SplitCsv(data[i]);

            for (int j = 0; j < row.Length; j++)
            {
                if (!row[j].Contains("null"))
                {
                    if (row[j] != "null" && row[j] != "null/r" && row[j] != "" && row[j] != "null ")
                    {
                        print("it's : " + i + " & " + j + " = " + row[j] + " length " + row[j].Length);
                        Vector2 newPos = new Vector2(j, i);
                        PlaceObjectOnGrid(newPos, row[j]);
                    }
                }
            }
        }
    }

    public string[] SplitCsv(string line)
    {
        List<string> result = new List<string>();
        string currentStr = "";
        bool inQuotes = false;
        for (int i = 0; i < line.Length; i++) // For each character
        {
            if (line[i] == ';') // Comma
            {
                if (!inQuotes) // If not in quotes, end of current string, add it to result
                {
                    result.Add(currentStr.ToString());
                    currentStr = "";
                }
            }
            else // Add any other character to current string
                currentStr += line[i];
        }
        result.Add(currentStr.ToString());
        return result.ToArray(); // Return array of all strings
    }

    void PlaceObjectOnGrid(Vector2 placeOnGrid, string name)
    {
        int childIndex = (int)(placeOnGrid.y * column + placeOnGrid.x);
        if (childIndex < grid.transform.childCount) {
            Vector2 correctPos = grid.transform.GetChild(childIndex).GetComponent<RectTransform>().position;
            GameObject go = null;
            if (name == "knife")
            {
                go = Instantiate(knife, correctPos, Quaternion.identity);
                grid.transform.GetChild(childIndex).GetComponent<ItemSlot>().isKnife = true;
            }
            else
            {
                go = Instantiate(item, correctPos, Quaternion.identity);
                foreach (Ingredients ingredient in allIngredients)
                {
                    if(name.Contains(ingredient.name))
                    {
                        go.GetComponent<IngredientManager>().myIngredient = ingredient;
                        go.GetComponent<IngredientManager>().Initialize();
                        break;
                    }
                }
            }
            go.transform.SetParent(InterfaceManager.instance.gamePanel.transform, true);
            go.GetComponent<DragDrop>().currentSlot = grid.transform.GetChild(childIndex).GetComponent<ItemSlot>();
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            grid.transform.GetChild(childIndex).GetComponent<ItemSlot>().isOccupied = true;
            grid.transform.GetChild(childIndex).GetComponent<ItemSlot>().myItem = go;
        }
    }

    void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.position;

        for (int i = 1; i < glg.transform.childCount; i++)
        {
           RectTransform currentChildObj = glg.transform.GetChild(i).GetComponent<RectTransform>();

           Vector2 currentChildPos = currentChildObj.position;

            if (firstChildPos.y == currentChildPos.y)
            {
                column++;
            }
            else
            {
                row++;
                i += column - 1;
            }
        }




        //bool stopCountingRow = false;

        ////Loop through the rest of the child object
        //for (int i = 1; i < glg.transform.childCount; i++)
        //{
        //    //Get the next child
        //   RectTransform currentChildObj = glg.transform.
        //   GetChild(i).GetComponent<RectTransform>();

        //    Vector2 currentChildPos = currentChildObj.anchoredPosition;

        //    //if first child.x == otherchild.x, it is a column, ele it's a row
        //    if (firstChildPos.x == currentChildPos.x)
        //    {
        //        column++;
        //        //Stop couting row once we find column
        //        stopCountingRow = true;
        //    }
        //    else
        //    {
        //        if (!stopCountingRow)
        //            row++;
        //    }
        //}
    }
}
