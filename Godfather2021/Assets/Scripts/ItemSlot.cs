using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public bool isOccupied = false;
    public GameObject myItem;
    public bool isKnife = false;

    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null && !eventData.pointerDrag.GetComponent<CookingTimer>())
        {
            if (isOccupied)
            {
                if (isKnife && eventData.pointerDrag.GetComponent<IngredientManager>())
                {
                    eventData.pointerDrag.GetComponent<IngredientManager>().Cut(true);
                    myItem.GetComponent<Knife>().OnCut(false);
                    print("couper slot");
                }
                else
                {
                    if (eventData.pointerDrag.GetComponent<Knife>()) {
                        if (myItem.GetComponent<IngredientManager>() && !myItem.GetComponent<IngredientManager>().state.isCut)
                        {
                            eventData.pointerDrag.GetComponent<Knife>().OnCut(true);
                            myItem.GetComponent<IngredientManager>().Cut(false);
                        }
                    }
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
                myItem = eventData.pointerDrag.gameObject;
            }
        }
    }
}
