using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class IngredientManager : MonoBehaviour
{

    public Ingredients myIngredient;
    private Image image;
    public IngredientState state = new IngredientState();

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
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }
    }

}
