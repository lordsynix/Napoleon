using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyBuilder : MonoBehaviour
{
    public static ArmyBuilder instance;

    public GameObject[] unitUIPrefabs;
    
    [Header("Squad References")]
    public Toggle leftSquadToggle;
    public Toggle rightSquadToggle;
    public GameObject leftSlots;
    public GameObject rightSlots;

    [Header("Units")]
    public int[] leftSquadUnits, rightSquadUnits, centerUnits;

    [Header("Attributes")]
    public int squadUnitsAmount;
    public int centerUnitAmount;

    private void Awake()
    {
        instance = this;

        leftSquadUnits = new int[squadUnitsAmount];
        rightSquadUnits = new int[squadUnitsAmount];
        centerUnits = new int[centerUnitAmount];
    }

    public void LeftSquad()
    {
        if (!rightSquadToggle.isOn)
            return;
        int i = 0;
        foreach (Transform t in leftSlots.transform)
        {
            int unitID = leftSquadUnits[i];
            if (unitID == 0)
                i++;
            else
            {
                t.GetComponent<UnitSlot>().GenerateUIUnits(leftSquadUnits[i]);
                i++;
            }
        }
    }

    public void RightSquad()
    {
        if (!leftSquadToggle.isOn)
            return;
        int i = 0;
        foreach (Transform t in rightSlots.transform)
        {
            int unitID = rightSquadUnits[i];
            if (unitID == 0)
                i++;
            else
            {
                t.GetComponent<UnitSlot>().GenerateUIUnits(rightSquadUnits[i]);
                i++;
            }
        }
    }

    public void Back()
    {
        if (leftSquadToggle.isOn)
        {
            rightSquadUnits = leftSquadUnits;
            rightSquadToggle.isOn = true;
        }

        if (rightSquadToggle.isOn)
        {
            leftSquadUnits = rightSquadUnits;
            leftSquadToggle.isOn = true;
        }
            
    }

    public void StartBattle()
    {
        
    }

    public void SetTogglesFromLeftSquad()
    {
        rightSquadToggle.isOn = leftSquadToggle.isOn;
    }

    public void SetTogglesFromRightSquad()
    {
        leftSquadToggle.isOn = rightSquadToggle.isOn;
    }
}
