using UnityEngine;
using UnityEngine.UI;

public enum IngredientType
{
    TOMATO,
    MEAT,
    PASTA
}

public class Ingredient : MonoBehaviour
{

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] colors;
    private Image image;
    private bool isCut = false;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        //image.sprite = sprites[0];
        image.color = colors[0];
    }

    public void Cut()
    {
        if (!isCut)
        {
            isCut = true;
            image.color = colors[1];
        }
        else
        {
            print("déjà coupé");
        }
    }

}
