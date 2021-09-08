using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IngredientState
{
    public bool isCut = false;
    public bool isCooked = false;
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Game ScriptObj/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public List<Ingredients> ingredientsList;
    public List<IngredientState> ingredientsStateList;
    //public bool pastas;
    //public bool tomatoSauce;
    //public bool meat;
    //public bool apple;

}
