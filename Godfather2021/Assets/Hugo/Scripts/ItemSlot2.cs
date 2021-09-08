using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot2 : MonoBehaviour, IDropHandler
{

    public CookingTimer cookingTimer;

    public void OnDrop(PointerEventData eventData)
    {
        print("item dropped");
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<DragDrop>().previousPos = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;

            cookingTimer.ingredientsInPan.Add(eventData.pointerDrag);

            cookingTimer.timeStart = true;
            cookingTimer.actualTimer = cookingTimer.maxTimer;
        }
    }
}
