using System;
using UnityEngine;
using UnityEngine.UI;


public class IngredientManager : MonoBehaviour
{

    public Ingredients myIngredient;
    private Image image;
    public IngredientState state = new IngredientState();



    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Initialize()
    {
        image = GetComponent<Image>();
        image.sprite = myIngredient.sprites[0];
        image.color = myIngredient.colors[0];
    }

    public void Cut()
    {
        if (!state.isCut)
        {
            state.isCut = true;
            image.color = myIngredient.colors[1];
            image.sprite = myIngredient.sprites[1];
        }
        else
        {
            print("déjà coupé");
        }
    }

}
