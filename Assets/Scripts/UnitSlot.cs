using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
{

    private void Update()
    {
          
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            InstantiateUnitUI(Unit.instance.activeUnitID);

    }

    private void InstantiateUnitUI(int unitID)
    {
        if (unitID == 0)
            return;
        var newUnit = Instantiate(Unit.instance.unitUI[unitID - 1]);
        newUnit.transform.SetParent(transform);
        newUnit.transform.position = transform.position;
        newUnit.transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Unit.instance.dragDropActive)
            return;
        InstantiateUnitUI(Unit.instance.activeUnitID);
    }

}
