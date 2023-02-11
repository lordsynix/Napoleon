using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

[RequireComponent(typeof(SelectedUnitDictionary))]
public class UnitMovement : MonoBehaviour
{
    private SelectedUnitDictionary unitDictionary;
    private List<Vector3> formationPositions = new List<Vector3>();

    public float spread;

    private void Start()
    {
        unitDictionary = GetComponent<SelectedUnitDictionary>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50000))
            {
                if (unitDictionary.selectedTable.Count > 1)
                {
                    CalculateFormation(hit.point);
                }
                else
                {
                    foreach (KeyValuePair<int, GameObject> unit in unitDictionary.selectedTable)
                    {
                        unit.Value.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    }
                }
            }
        }
    }
    public void CalculateFormation(Vector3 destination)
    {
        if (unitDictionary.selectedTable.Count % 2 == 0)
        {
            int formationWidth = unitDictionary.selectedTable.Count / 2;
            int formationDepth = unitDictionary.selectedTable.Count / 2;

            Vector3 middleOffset = new Vector3(formationWidth * 0.5f, 0, formationDepth * 0.5f);

            for (int x = 0; x < formationWidth; x++)
            {
                for (int z = 0; z < formationDepth; z++)
                {
                    Vector3 pos = new Vector3(x, 0, z);
                    pos -= middleOffset;
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
        ShuffleList();
        int i = 0;
        foreach (KeyValuePair<int, GameObject> unit in unitDictionary.selectedTable)
        {
            unit.Value.GetComponent<NavMeshAgent>().SetDestination(formationPositions[i]);
            i++;
            if (i > formationPositions.Count)
            {
                i = 0;
            }
        }
    }

    private void ShuffleList()
    {
        System.Random rand = new System.Random();
        unitDictionary.selectedTable = unitDictionary.selectedTable.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);

    }
}
