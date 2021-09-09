using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour, IDropHandler
{
    private DragDrop dragAndDrop;
    private bool isCutting = false;
    private RectTransform rect;

    private void Start()
    {
        dragAndDrop = GetComponent<DragDrop>();
        rect = GetComponent<RectTransform>();

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
        isCutting = true;
        //shake rectTransform
        StartCoroutine(WaitAndStopCut(needToGoBack));
    }

    private IEnumerator WaitAndStopCut(bool goBack)
    {
        yield return new WaitForSeconds(0.85f);
        isCutting = false;
        dragAndDrop.canDrop = true;
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
}
