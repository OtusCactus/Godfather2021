using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class IngredientManager : MonoBehaviour
{

    public Ingredients myIngredient;
    private Image image;
    public IngredientState state = new IngredientState();

    public GameObject knife;
    public bool isCutting = false;


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

    public void Cut(bool needToGoBack)
    {
        if (!state.isCut)
        {
            StartCoroutine("CutAnimation");
            GetComponent<DragDrop>().canDrop = false;
            isCutting = true;
            if (myIngredient.type == IngredientType.MEAT)
            {
                AudioManager.instance.Play("CutMeat");
            }
            else
            {
                AudioManager.instance.Play("CutVege");
            }
            state.isCut = true;
            //shake rectTransform
            StartCoroutine(WaitAndStopCut(needToGoBack));
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

    private IEnumerator WaitAndStopCut(bool goBack)
    {
        yield return new WaitForSeconds(0.85f);
        //a terme, wait for clip length
        isCutting = false;
        image.sprite = myIngredient.sprites[1];
        GetComponent<DragDrop>().canDrop = true;
        if (goBack)
        {
            GetComponent<RectTransform>().position = GetComponent<DragDrop>().previousPos;
        }
    }

}
