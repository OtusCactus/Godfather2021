using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationResult : MonoBehaviour
{

    public List<Ingredients> recipeIngredients = new List<Ingredients>();
    public List<IngredientState> recipeIngredientsState;

    [HideInInspector] public Image image;


    public float mealScore = 0;
    public bool isOnPan = false;

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

            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        };
        dragAndDrop.onDraggingEnd += () =>
        {
            if (dragAndDrop.droppedOnSlot)
            {
                cookingTimer.TimerBack();
                isOnPan = false;
            }
            else
            {
                if (isOnPan)
                {
                    cookingTimer.mealServed = this;
                }
            }
        };
    }


    public void Initialize(List<GameObject> allIngredients)
    {
        image = GetComponent<Image>();

        image.color = LevelManager.instance.recipe.colors[0];

        foreach (GameObject ingrMan in allIngredients)
        {
            recipeIngredients.Add(ingrMan.GetComponent<IngredientManager>().myIngredient);
            recipeIngredientsState.Add(ingrMan.GetComponent<IngredientManager>().state);
        }
        LevelManager.instance.plate = this;
        mealScore = LevelManager.instance.CompareRecipe();
    }

    public void ChangeAspect()
    {
        if (isBurnt)
        {
            image.color = LevelManager.instance.recipe.colors[1];
        }
        else if(mealScore > 75)
        {
            image.sprite = LevelManager.instance.recipe.sprites[0];
        }
        else if(mealScore > 50)
        {
            image.sprite = LevelManager.instance.recipe.sprites[1];
        }
        else if (mealScore > 25)
        {
            image.sprite = LevelManager.instance.recipe.sprites[2];
        }
        else
        {
            image.sprite = LevelManager.instance.recipe.sprites[3];
        }
    }
}
