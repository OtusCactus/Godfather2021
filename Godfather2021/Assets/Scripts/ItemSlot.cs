using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public bool isOccupied = false;
    public bool isKnife = false;

    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null)
        {
            if (isOccupied)
            {
                if (isKnife)
                {
                    eventData.pointerDrag.GetComponent<IngredientManager>().Cut();
                    print("couper slot");
                }
                eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = false;
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                eventData.pointerDrag.GetComponent<DragDrop>().previousPos = GetComponent<RectTransform>().position;
                eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
                eventData.pointerDrag.GetComponent<DragDrop>().currentSlot = this;
                isOccupied = true;
            }
        }
    }
}
