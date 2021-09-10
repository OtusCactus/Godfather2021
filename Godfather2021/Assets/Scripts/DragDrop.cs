using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector2 previousPos;
    [HideInInspector] public bool droppedOnSlot = false;
    public ItemSlot currentSlot;
    public ItemSlot previousSlot;

    public System.Action onDraggingEnd;
    public System.Action onDraggingBegin;

    public bool canDrop = true;

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

        if(currentSlot != null)
        {
            previousSlot = currentSlot;
            currentSlot.isOccupied = false;
            currentSlot.myItem = null;
            currentSlot = null;
        }
        
        if (onDraggingBegin != null) onDraggingBegin.Invoke();

        rectTransform.SetAsLastSibling(); ;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //get object to follow mouse (eventData.delta is mouse mouvement)
        //divide by canvas scale factor so that regardless of the screen size, the object will always correctly follow the mouse
        rectTransform.anchoredPosition += eventData.delta / InterfaceManager.instance.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.Play("Drop");
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (droppedOnSlot == false)
        {
            if (canDrop)
            {
                if (GetComponent<CookingTimer>())
                {
                    if (GetComponent<CookingTimer>().isOnFire)
                    {
                        rectTransform.position = GameManager.instance.panWarmer.GetComponent<PanWarmer>().panPosition.position;
                    }
                    else
                    {
                        rectTransform.position = previousPos;
                    }
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                }
                else
                {
                    rectTransform.position = previousPos;
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
                }
            }
            if (previousSlot != null)
            {
                if (GetComponent<CookingTimer>())
                {
                    if (!GetComponent<CookingTimer>().isOnFire)
                    {
                        currentSlot = previousSlot;
                        currentSlot.isOccupied = true;
                        currentSlot.myItem = gameObject;
                    }
                }
                else
                {
                    currentSlot = previousSlot;
                    currentSlot.isOccupied = true;
                    currentSlot.myItem = gameObject;
                }
            }
        }

        if (onDraggingEnd != null) onDraggingEnd.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.Play("Grab");
        }
        print("mouse clicked on object");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<Knife>())
        {
            if (GetComponent<IngredientManager>() && !GetComponent<IngredientManager>().state.isCut)
            {
                eventData.pointerDrag.GetComponent<Knife>().OnCut(true);
                GetComponent<IngredientManager>().Cut(false);
            }
        }
    }
}
