using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();

    public Recipe recipe;
    public Recipient recipient;

    public int recipeAliment1;
    public int recipeAliment2;

    // Start is called before the first frame update
    void Start()
    {
        recipe = recipes[Random.Range(0, recipes.Count)];

        recipeAliment1 = recipe.aliment1;
        recipeAliment2 = recipe.aliment2;
    }

}
