using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RuntimeHandle;

public class DragDropHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public GameObject relevantObj;
    private GameObject spawnPoint;
    private GameObject spawnClone;
    public RuntimeTransformHandle handle;
    public bool inUse;

    private void Awake()
    {
        handle = GameObject.FindObjectOfType<RuntimeTransformHandle>();
        spawnPoint = GameObject.Find("SpawnPoint");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Setting default value for our 3D-UI usage
        inUse = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        /// Spawn 3D Prefab if we are away from the Slot
        /// By the time we doing the EndDrag we should know that
        if (inUse)
        {
            spawnClone = Instantiate(relevantObj.gameObject, spawnPoint.transform) as GameObject;
            spawnClone.transform.SetParent(spawnPoint.transform);
            rectTransform.GetComponent<RawImage>().enabled = false;

            // Enable the Runtime handlers
            handle.target = spawnClone.transform;
            handle.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
