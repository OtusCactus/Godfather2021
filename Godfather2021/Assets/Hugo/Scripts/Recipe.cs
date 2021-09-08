using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IngredientState
{
    public bool isCut = false;
    public bool isCooked = false;
    public bool isChecked = false;

    public static bool operator ==(IngredientState a, IngredientState b)
    {
        if(a.isCut == b.isCut)
        {
            if(a.isCooked == b.isCooked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public static bool operator !=(IngredientState a, IngredientState b)
    {
        if (a.isCut == b.isCut)
        {
            if (a.isCooked == b.isCooked)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }
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
