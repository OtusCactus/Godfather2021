using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public List<Recipe> recipes = new List<Recipe>();

    public Recipient recipient;

    public Recipe recipe;

    public Dictionary<string, bool> recipeIngredients = new Dictionary<string, bool>();

    [HideInInspector] public CombinationResult plate;
    private List<Ingredients> ingredientsNeeded = new List<Ingredients>();
    private List<IngredientState> ingredientsStateNeeded = new List<IngredientState>();

    public float finalScore = 0;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Ingredients ingr in recipe.ingredientsList)
        {
            ingredientsNeeded.Add(ingr);
        }
        foreach (IngredientState ingrState in recipe.ingredientsStateList)
        {
            IngredientState newState = new IngredientState();
            newState.isChecked = false;
            newState.isCooked = ingrState.isCooked;
            newState.isCut = ingrState.isCut;
            ingredientsStateNeeded.Add(newState);
        }

        //if(recipes.Count != 0)
        //{
        //    recipe = recipes[Random.Range(0, recipes.Count)];
        //}

        //recipeIngredients.Add("Pastas", recipe.pastas);
        //recipeIngredients.Add("Tomato Sauce", recipe.tomatoSauce);
        //recipeIngredients.Add("Meat", recipe.meat);
        //recipeIngredients.Add("Apple", recipe.apple);

    }

    public float CompareRecipe()
    {
        float score = 0;
        float maxScore = ingredientsNeeded.Count * 100;
        string[] potentialIngrediens = new string[ingredientsNeeded.Count];

        //compare each ingredient
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            potentialIngrediens[i] = "";
            //if plate has ingredients
            if (plate.recipeIngredients.Count > 0)
            {
                for (int j = 0; j < plate.recipeIngredients.Count; ++j)
                {
                    if (!plate.recipeIngredientsState[j].isChecked && plate.recipeIngredients[j] == ingredientsNeeded[i])
                    {
                        //v�rifier �tat
                        if (plate.recipeIngredientsState[j] == ingredientsStateNeeded[i])
                        {
                            score += 100;

                            //l'ingr�dient est bon, pas besoin de deuxi�me liste pour couleur/cuisson
                            ingredientsStateNeeded[i].isChecked = true;

                            //EN FAIT NON CAR INDEX DANS ARRAY
                            //on retire l'ingr�dient et son �tat de la liste pour �courter les boucles
                            //plate.recipeIngredients.RemoveAt(j);
                            //plate.recipeIngredientsState.RemoveAt(j);
                            plate.recipeIngredientsState[j].isChecked = true;
                            //cet ingr�dient i est fait, on passe au suivant
                            break;
                        }
                        //si �tat pas bon, rajoute � liste d'ingr�dient � checker cuisson si aucun n'est bon dans premi�re boucle
                        else
                        {
                            potentialIngrediens[i] += i.ToString() + "-";
                        }
                    }
                }
            }
        }

        //deuxieme liste pour cuisson
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            if (!ingredientsStateNeeded[i].isChecked)
            {
                if (potentialIngrediens[i].Length > 0)
                {
                    //split string to have all indexes
                    string[] allIndexes = potentialIngrediens[i].Split(char.Parse("-"));

                    for (int j = 0; j < allIndexes.Length; ++j)
                    {
                        int index = 0;
                        bool exist = int.TryParse(allIndexes[j], out index);
                        if (exist && index < plate.recipeIngredientsState.Count)
                        {
                            if (!plate.recipeIngredientsState[index].isChecked && plate.recipeIngredients[index] == ingredientsNeeded[i])
                            {
                                //pas besoin de v�rifier �tat, on sait que faut, mais donne moiti� des points
                                //et check ingr�dients
                                score += 50;
                                ingredientsStateNeeded[i].isChecked = true;
                                plate.recipeIngredientsState[index].isChecked = true;
                                //l'ingr�dient i estfait, on passe � la suite
                                break;
                            }
                        }
                    }
                }
            }
        }


        //troisi�me loop, pour les couleurs
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            potentialIngrediens[i] = "";
            if (!ingredientsStateNeeded[i].isChecked)
            {
                if (plate.recipeIngredients.Count > 0)
                {
                    for (int j = 0; j < plate.recipeIngredients.Count; ++j)
                    {
                        if (!plate.recipeIngredientsState[j].isChecked && plate.recipeIngredients[j].type == ingredientsNeeded[i].type)
                        {
                            //v�rifier �tat
                            if (plate.recipeIngredientsState[j] == ingredientsStateNeeded[i])
                            {
                                score += 25;

                                //l'ingr�dient est bon, pas besoin de deuxi�me liste pour cuisson
                                ingredientsStateNeeded[i].isChecked = true;

                                plate.recipeIngredientsState[j].isChecked = true;
                                //cet ingr�dient i est fait, on passe au suivant
                                break;
                            }
                            //si �tat pas bon, rajoute � liste d'ingr�dient � checker cuisson si aucun n'est bon dans premi�re boucle
                            else
                            {
                                potentialIngrediens[i] += i.ToString() + "-";
                            }
                        }
                    }
                }
            }
        }

        //4eme loop, pour cuisson de type
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            if (!ingredientsStateNeeded[i].isChecked)
            {
                if (potentialIngrediens[i].Length > 0)
                {
                    //split string to have all indexes
                    string[] allIndexes = potentialIngrediens[i].Split(char.Parse("-"));

                    for (int j = 0; j < allIndexes.Length; ++j)
                    {
                        int index = 0;
                        bool exist = int.TryParse(allIndexes[j], out index);
                        if (exist && index < plate.recipeIngredientsState.Count)
                        {
                            if (!plate.recipeIngredientsState[index].isChecked && plate.recipeIngredients[index].type == ingredientsNeeded[i].type)
                            {
                                //pas besoin de v�rifier �tat, on sait que faut, mais donne moiti� des points
                                score += 10;
                                ingredientsStateNeeded[i].isChecked = true;
                                plate.recipeIngredientsState[index].isChecked = true;
                                //l'ingr�dient i est fait, on passe � la suite
                                break;
                            }
                        }
                    }
                }
            }
        }

        //loop des malus car ingr�dients en trop
        for (int i = 0; i < plate.recipeIngredients.Count; i++)
        {
            if (!plate.recipeIngredientsState[i].isChecked)
            {
                score -= 5;
            }
        }
        //loop pour ingr�dients manquants
        for (int i = 0; i < ingredientsNeeded.Count; i++)
        {
            if (!ingredientsStateNeeded[i].isChecked)
            {
                score -= 5;
            }
        }

        print("score : " + score + " / max score : " + maxScore);
        score = Mathf.Clamp((score * 100) / maxScore, 0, 100);
        print("score : " + score + "%");
        return score;
        //TODO
        //plus tard, check grille si ingr�dients coup� en trop

    }
}
    
