using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class IngredientManager : MonoBehaviour
{

    public Ingredients myIngredient;
    private Image image;
    public IngredientState state = new IngredientState();

    public GameObject knife;


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
            StartCoroutine("CutAnimation");
            if(myIngredient.type == IngredientType.MEAT)
            {
                AudioManager.instance.Play("CutMeat");
            }
            else
            {
                AudioManager.instance.Play("CutVege");
            }
            state.isCut = true;
            image.sprite = myIngredient.sprites[1];
        }
        else
        {
            print("déjà coupé");
        }
    }

    private IEnumerator CutAnimation()
    {
        Debug.Log("patate");
        knife.GetComponent<Animator>().SetBool("isCut", true);
        yield return new WaitForSeconds(0.3f);
        knife.GetComponent<Animator>().SetBool("isCut", false);
    }

}
