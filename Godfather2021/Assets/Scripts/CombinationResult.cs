using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationResult : MonoBehaviour
{

    public List<Ingredients> recipeIngredients = new List<Ingredients>();
    public List<IngredientState> recipeIngredientsState;

    private Image image;

    private DragDrop dragAndDrop;
    public CookingTimer cookingTimer;

    public bool isBurnt = false;

    private void Start()
    {
        dragAndDrop = GetComponent<DragDrop>();

        dragAndDrop.onDraggingBegin += () =>
        {
            cookingTimer.mealServed = null;
            Debug.Log("Drag Meal");
        };
        dragAndDrop.onDraggingEnd += () =>
        {
            cookingTimer.TimerBack();
        };
    }

    public void Initialize(List<GameObject> allIngredients)
    {
        image = GetComponent<Image>();

        foreach (GameObject ingrMan in allIngredients)
        {
            recipeIngredients.Add(ingrMan.GetComponent<IngredientManager>().myIngredient);
            recipeIngredientsState.Add(ingrMan.GetComponent<IngredientManager>().state);
        }
        LevelManager.instance.plate = this;
        LevelManager.instance.CompareRecipe();
    }

    public void ChangeAspect(float ratio)
    {
        if(ratio > 75)
        {
            image.color = LevelManager.instance.recipe.colors[0];
        }
        else if(ratio > 50)
        {
            image.color = LevelManager.instance.recipe.colors[1];
        }
        else if (ratio > 25)
        {
            image.color = LevelManager.instance.recipe.colors[2];
        }
        else
        {
            image.color = LevelManager.instance.recipe.colors[3];
        }
    }
}
