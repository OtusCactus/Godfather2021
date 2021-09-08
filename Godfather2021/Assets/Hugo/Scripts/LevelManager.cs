using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();

    public Recipe recipe;
    public Recipient recipient;

    public Dictionary<string, bool> recipeIngredients = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        if(recipes.Count != 0)
        {
            recipe = recipes[Random.Range(0, recipes.Count)];
        }

        recipeIngredients.Add("Pastas", recipe.pastas);
        recipeIngredients.Add("Tomato Sauce", recipe.tomatoSauce);
        recipeIngredients.Add("Meat", recipe.meat);
        recipeIngredients.Add("Apple", recipe.apple);

    }

}
