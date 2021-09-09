using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanWarmer : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CookingTimer>())
        {
            if (eventData.pointerDrag.GetComponent<CookingTimer>().ingredientsInPan.Count > 0)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                //eventData.pointerDrag.GetComponent<DragDrop>().previousPos = GetComponent<RectTransform>().position;
                eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;

                eventData.pointerDrag.GetComponent<CookingTimer>().StartTimer();
            }

        }
    }
}
