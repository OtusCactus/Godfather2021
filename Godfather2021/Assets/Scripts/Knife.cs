using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Knife : MonoBehaviour, IDropHandler
{
    private DragDrop dragAndDrop;

    [SerializeField] private Sprite[] animSprites;

    private Image image;
    private Animation anim;

    private void Start()
    {
        dragAndDrop = GetComponent<DragDrop>();
        image = GetComponent<Image>();
        anim = GetComponent<Animation>();

        dragAndDrop.onDraggingBegin += () =>
        {
            dragAndDrop.previousSlot.isKnife = false;
        };
        dragAndDrop.onDraggingEnd += () =>
        {
            dragAndDrop.currentSlot.isKnife = true;
        };
    }

    public void OnCut(bool needToGoBack)
    {
        GetComponent<DragDrop>().canDrop = false;
        //shake rectTransform
        GetComponent<RectTransform>().SetAsLastSibling();
        anim.Play("A_KnifeCut");
        StartCoroutine(WaitAndStopCut(needToGoBack));
    }

    private IEnumerator WaitAndStopCut(bool goBack)
    {
        yield return new WaitForSeconds(0.85f);
        dragAndDrop.canDrop = true;
        anim.Play("A_KnifeIdle");
        if (goBack)
        {
            GetComponent<RectTransform>().position = dragAndDrop.previousPos;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<IngredientManager>())
            {
                eventData.pointerDrag.GetComponent<IngredientManager>().Cut(true);
                OnCut(false);
                print("couper");
            }
        }
    }

    public void ChangeSprite(int id)
    {
        image.sprite = animSprites[id];
    }
}
