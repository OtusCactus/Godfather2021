using UnityEngine;
using UnityEngine.EventSystems;

public class Knife : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Ingredient>())
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().previousPos;
                eventData.pointerDrag.GetComponent<Ingredient>().Cut();
                print("couper");
            }
        }
    }
}
