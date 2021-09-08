using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour, IDropHandler
{
    private DragDrop dragAndDrop;

    private void Start()
    {
        dragAndDrop = GetComponent<DragDrop>();

        dragAndDrop.onDraggingBegin += () =>
        {
            dragAndDrop.previousSlot.isKnife = false;
        };
        dragAndDrop.onDraggingEnd += () =>
        {
            dragAndDrop.currentSlot.isKnife = true;
        };
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<IngredientManager>())
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().previousPos;
                eventData.pointerDrag.GetComponent<IngredientManager>().Cut();
                print("couper");
            }
        }
    }
}
