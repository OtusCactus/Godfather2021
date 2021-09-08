using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool miaou = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        miaou = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .75f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //get object to follow mouse (eventData.delta is mouse mouvement)
        //divide by canvas scale factor so that regardless of the screen size, the object will always correctly follow the mouse
        rectTransform.anchoredPosition += eventData.delta / InterfaceManager.instance.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("mouse clicked on object");
    }
}
