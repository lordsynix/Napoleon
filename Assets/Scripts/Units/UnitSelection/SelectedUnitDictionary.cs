using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class SelectedUnitDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();
    public Text selectedUnitText;

    public void AddSelected(GameObject addedObject)
    {
        if (addedObject.layer != 3)
        {
            int id = addedObject.GetInstanceID();

            if (!(selectedTable.ContainsKey(id)))
            {
                selectedTable.Add(id, addedObject);
                addedObject.AddComponent<SelectedUnit>();
            }
        }
        CalculateGroupCount();
    }

    public void Deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<SelectedUnit>());
        selectedTable.Remove(id);
        CalculateGroupCount();
    }

    public void DeselectAll()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            if (pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<SelectedUnit>());
            }
        }
        selectedTable.Clear();
        CalculateGroupCount();
    }

    private void Update()
    {
        if (selectedTable.Count == 0)
        {
            selectedUnitText.text = "0 Units selected.";
        }       
    }

    private void CalculateGroupCount()
    {
        int infantryCount = 0;
        int artilleryCount = 0;
        int cavalryCount = 0;
        int logisticsCount = 0;
        foreach (KeyValuePair<int, GameObject> pair in selectedTable)
        {
            GroupType groupType = pair.Value.GetComponent<UnitInformation>().GetGroupType();
            if (groupType == GroupType.Infantry)
            {
                infantryCount += 1;
            }
            else if (groupType == GroupType.Artillery)
            {
                artilleryCount += 1;
            }
            else if (groupType == GroupType.Cavalry)
            {
                cavalryCount += 1;
            }
            else if (groupType == GroupType.Logistics)
            {
                logisticsCount += 1;
            }
        }
        selectedUnitText.text = infantryCount + " Infantry selected, " + artilleryCount + " Artillery selected, " + cavalryCount + " Cavalry selected, " + logisticsCount + " Logistics selected.";
    }
}