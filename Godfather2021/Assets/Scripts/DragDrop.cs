using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [HideInInspector] public Vector2 previousPos;
    [HideInInspector] public bool droppedOnSlot = false;
    [HideInInspector] public ItemSlot currentSlot;
    [HideInInspector] public ItemSlot previousSlot;

    public System.Action onDraggingEnd;
    public System.Action onDraggingBegin;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        previousPos = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .75f;
        droppedOnSlot = false;
        previousSlot = currentSlot; 
        currentSlot.isOccupied = false;
        currentSlot.myItem = null;
        currentSlot = null;
        if (onDraggingBegin != null) onDraggingBegin.Invoke();

        rectTransform.SetSiblingIndex(InterfaceManager.instance.gamePanel.transform.childCount - 1);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //get object to follow mouse (eventData.delta is mouse mouvement)
        //divide by canvas scale factor so that regardless of the screen size, the object will always correctly follow the mouse
        rectTransform.anchoredPosition += eventData.delta / InterfaceManager.instance.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.instance.Play("Drop");

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true; 
        if (droppedOnSlot == false)
        {
            rectTransform.position = previousPos;
            currentSlot = previousSlot;
            currentSlot.isOccupied = true;
            currentSlot.myItem = gameObject;
        }

        if (onDraggingEnd != null) onDraggingEnd.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.Play("Grab");
        print("mouse clicked on object");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Knife>())
        {
            if (GetComponent<IngredientManager>())
            {
                GetComponent<IngredientManager>().Cut();
            }
        }
    }
}
