using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectedUnitDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();
    private List<Vector3> formationPositions = new List<Vector3>();
    public float spread;

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
    }

    public void Deselect(int id)
    {
        Destroy(selectedTable[id].GetComponent<SelectedUnit>());
        selectedTable.Remove(id);
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
    }

    /*The following code checks if there are more than one unit selected if the player presses the right mouse button.
    If there are more than one, the script will calculate a formation for each of the units in the dictionary.
    */
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
              
            if (Physics.Raycast(ray, out hit, 50000))
            {
                if (selectedTable.Count > 1)
                {
                    CalculateFormation(hit.point);
                }
                else
                {
                    foreach (KeyValuePair<int, GameObject> unit in selectedTable)
                    { 
                        unit.Value.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    }
                }
            }           
        }
    }
    public void CalculateFormation(Vector3 destination)
    {
        if (selectedTable.Count % 2 == 0)
        {
            int formationWidth = selectedTable.Count / 2;
            int formationDepth = selectedTable.Count / 2;

            Vector3 middleOffset = new Vector3(formationWidth * 0.5f, 0, formationDepth * 0.5f);

            for (int x = 0; x < formationWidth; x++)
            {
                for (int z = 0; z < formationDepth; z++)
                {
                    Vector3 pos = new Vector3(x, 0, z);
                    pos += middleOffset;
                    pos *= spread;
                    formationPositions.Add(pos + destination);
                }
            }
            SendFormationData();
            formationPositions.Clear();
        }
        
    }

    private void SendFormationData()
    {
        int i = 0;
        Debug.Log(formationPositions.Count);
        foreach (KeyValuePair<int, GameObject> unit in selectedTable)
        {
            Debug.Log(formationPositions[i]);
            Debug.Log(i + ". iteration");
            unit.Value.GetComponent<NavMeshAgent>().SetDestination(formationPositions[i]);
            i++;
            if (i > formationPositions.Count)
            {
                i = 0;
            }
        }
    }
}