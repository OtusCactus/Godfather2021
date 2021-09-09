using System;
using UnityEngine;

public enum IngredientType
{
    MEAT,
    ORANGE,
    PINK,
    GREEN,
    WHITE,
    GREY
}

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Game ScriptObj/Ingredient", order = 1)]
public class Ingredients : ScriptableObject
{
    public string name = "";
    public Sprite[] sprites;
    public Color[] colors;
    public IngredientType type;
}
