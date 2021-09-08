using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Game ScriptObj/Ingredient", order = 1)]
public class Ingredients : ScriptableObject
{
    public string name = "";
    public Sprite[] sprites;
    public Color[] colors;
}
