using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanWarmer : MonoBehaviour, IDropHandler
{
    public RectTransform panPosition;

    public void OnDrop(PointerEventData eventData)
    {
        //if an object has been droppped, put it in the right place
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<CookingTimer>())
        {
            if (eventData.pointerDrag.GetComponent<CookingTimer>().ingredientsInPan.Count > 0)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = panPosition.position;
                //eventData.pointerDrag.GetComponent<DragDrop>().previousPos = GetComponent<RectTransform>().position;
                eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
                eventData.pointerDrag.GetComponent<CookingTimer>().isOnFire = true;

                eventData.pointerDrag.gameObject.transform.localScale = new Vector3(2, 2, 2);

                eventData.pointerDrag.GetComponent<CookingTimer>().StartTimer();
            }

        }
    }
}
