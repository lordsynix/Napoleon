using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlot : MonoBehaviour, IDropHandler
{
    private ArmyBuilder armyBuilder;

    public enum Type
    {
        Center,
        LeftSquad,
        RightSquad
    }

    public Type type;

    private void Awake()
    {
        armyBuilder = ArmyBuilder.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            int unitID = eventData.pointerDrag.gameObject.GetComponent<DragDrop>().unitID;
            InstantiateUnitUI(unitID);
        }
    }

    private void InstantiateUnitUI(int unitID)
    {
        if (unitID == 0)
            return;
        foreach (Transform t in transform)
            Destroy(t.gameObject);

        var newUnit = Instantiate(armyBuilder.unitUIPrefabs[unitID - 1], transform);
        newUnit.transform.position = transform.position;
        newUnit.transform.localScale = Vector3.one;

        int unit = int.Parse(name);

        if (type == Type.Center) 
            armyBuilder.centerUnits[unit - 1] = unitID;

        if (type == Type.LeftSquad)
            armyBuilder.leftSquadUnits[unit - 1] = unitID;

        if (type == Type.RightSquad)
            armyBuilder.rightSquadUnits[unit - 1] = unitID;

    }

    public void GenerateUIUnits(int unitID)
    {
        if (unitID == 0)
            return;
        foreach (Transform t in transform)
            Destroy(t.gameObject);

        var newUnit = Instantiate(armyBuilder.unitUIPrefabs[unitID - 1], transform);
        // I have no Idea why this works. Please don't do anything with it.
        RectTransform rt = (RectTransform)newUnit.transform;
        RectTransform rtParent = (RectTransform)transform;
        rt.anchoredPosition = rtParent.anchoredPosition;
        newUnit.transform.localPosition -= new Vector3(0f, -88f, 0);
        newUnit.transform.localPosition = new Vector3(31.34265f / 128, newUnit.transform.localPosition.y, 0f);

        int unit = int.Parse(name);

        if (type == Type.Center)
            armyBuilder.centerUnits[unit - 1] = unitID;

        if (type == Type.LeftSquad)
            armyBuilder.leftSquadUnits[unit - 1] = unitID;

        if (type == Type.RightSquad)
            armyBuilder.rightSquadUnits[unit - 1] = unitID;
    }

}
