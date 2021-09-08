using UnityEngine;

public class Recipient : MonoBehaviour
{

    public LevelManager levelManager;

    public Recipe recipe;

    public GameObject validText;

    // Start is called before the first frame update
    void Start()
    {
        recipe = new Recipe();
    }
    
    void Update()
    {

        //AddIngredients();
        //Comparison();

    }

    //private void AddIngredients()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        recipe.pastas = !recipe.pastas;
    //        //Debug.Log("Pastas : " + recipe.pastas);

    //    }

    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        recipe.tomatoSauce = !recipe.tomatoSauce;
    //        //Debug.Log("TomatoSauce : " + recipe.tomatoSauce);
    //    }

    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        recipe.meat = !recipe.meat;
    //       // Debug.Log("Meat : " + recipe.meat);
    //    }

    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        recipe.apple = !recipe.apple;
    //        //Debug.Log("Apple : " + recipe.apple);
    //    }
    //}

    //public void Comparison()
    //{
    //    if (recipe.pastas == levelManager.recipeIngredients["Pastas"] &&
    //        recipe.tomatoSauce == levelManager.recipeIngredients["Tomato Sauce"] &&
    //        recipe.meat == levelManager.recipeIngredients["Meat"] &&
    //        recipe.apple == levelManager.recipeIngredients["Apple"])
    //    {
    //        validText.SetActive(true);
    //    }
    //    else
    //    {
    //        validText.SetActive(false);
    //    }
    //}

}
