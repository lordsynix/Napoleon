using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,
                                       IDragHandler
{
    public int unitID = 0;

    private CanvasGroup canvasGroup;
    private Text callback;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        callback = GameObject.FindWithTag("Callback Text").GetComponent<Text>();

        if (unitID == 0)
        {
            Debug.LogWarning("Please assign an unit ID");
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        callback.text = Unit.instance.unitTypeFromInt[unitID] + " selected";
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }


}
