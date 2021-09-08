using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawner : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    [SerializeField] private GameObject objectToSpawn;
    //private PointerEventData pointer;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject go = Instantiate(objectToSpawn, eventData.position, Quaternion.identity);
        go.transform.SetParent(InterfaceManager.instance.gamePanel.transform, true);
        eventData.pointerDrag = go; // assign instantiated element
        go.GetComponent<CanvasGroup>().blocksRaycasts = false;
        go.GetComponent<CanvasGroup>().alpha = .75f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //
    }
}
