using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plate : MonoBehaviour, IDropHandler
{

    public List<Ingredients> recipeIngredients = new List<Ingredients>();
    public List<IngredientState> recipeIngredientsState;


    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CombinationResult>())
        {
            LevelManager.instance.finalScore = eventData.pointerDrag.GetComponent<CombinationResult>().mealScore;
            eventData.pointerDrag.gameObject.SetActive(false);
            //recipeIngredients.Add(eventData.pointerDrag.GetComponent<IngredientManager>().myIngredient);
            //recipeIngredientsState.Add(eventData.pointerDrag.GetComponent<IngredientManager>().state);
            InterfaceManager.instance.mealImage.sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
            InterfaceManager.instance.mealImage.color = eventData.pointerDrag.GetComponent<Image>().color;
            GameManager.instance.ChangeState(GameState.RESULT);
        }
    }
}
