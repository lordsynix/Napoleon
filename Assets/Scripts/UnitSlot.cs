using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            
            var pointerDrag = eventData.pointerDrag;

            var newUnit = Instantiate(Unit.instance.unitUI[pointerDrag.GetComponent<DragDrop>().unitID - 1]);
            newUnit.transform.SetParent(transform);
            newUnit.transform.position = transform.position;
            newUnit.transform.localScale = Vector3.one;
            

        }
    }
}
