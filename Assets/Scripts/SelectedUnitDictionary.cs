using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitDictionary : MonoBehaviour
{
    public Dictionary<int,GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void AddSelected(GameObject addedObject)
    {
        int id = addedObject.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)))
        {
            selectedTable.Add(id, addedObject);
            addedObject.AddComponent<SelectedUnit>();
        }
    }

    public void Deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<SelectedUnit>());
        selectedTable.Remove(id);
    }

    public void DeselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if (pair.Value != null)
            {
                Destroy(selectedTable[pair.Key].GetComponent<SelectedUnit>());
            }
        }
        selectedTable.Clear();
    }
}
