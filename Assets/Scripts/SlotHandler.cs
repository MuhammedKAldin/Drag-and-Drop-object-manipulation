using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IDropHandler, IEndDragHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            // Behaviours
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.gameObject.transform.SetParent(transform);
            eventData.pointerDrag.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            eventData.pointerDrag.gameObject.GetComponent<DragDropHandler>().inUse = false;
        }

    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Debug.Log("OnEndDrag heeeeeeeeeeeeeere");
    }
}
