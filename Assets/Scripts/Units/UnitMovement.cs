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
    private List<Vector3> groupFormationPositions = new List<Vector3>();

    public GameObject leftSquad;
    public GameObject centerSquad;
    public GameObject rightSquad;

    [SerializeField] private ArmySetup setup;

    public float spread;
    public float groupSpread;

    private void Start()
    {
        unitDictionary = GetComponent<SelectedUnitDictionary>();
        StartCoroutine(StartingFormation());
    }

    private IEnumerator StartingFormation()
    {
        yield return new WaitForSeconds(2f);

        // Starting formation for left squad
        UnitInformation[] leftSquadUnits = leftSquad.GetComponentsInChildren<UnitInformation>();
        CalculateStartingFormation(leftSquad.transform.position, 0);
        for (int i = 0; i < setup.GetSquadCount(0); i++)
        {
            for (int j = 0 + (i*36); j < 36 *(i+1); j++)
            {
                unitDictionary.AddSelected(leftSquadUnits[j].gameObject);
            }
            CalculateFormation(groupFormationPositions[i]);
            unitDictionary.DeselectAll();           
        }
        groupFormationPositions.Clear();

        yield return new WaitForSeconds(2f);

        // Starting formation for center squad
        UnitInformation[] centerSquadUnits = centerSquad.GetComponentsInChildren<UnitInformation>();
        CalculateStartingFormation(centerSquad.transform.position, 1);
        for (int i = 0; i < setup.GetSquadCount(1); i++)
        {
            for (int j = 0 + (i*36); j < 36 *(i+1); j++)
            {
                unitDictionary.AddSelected(centerSquadUnits[j].gameObject);
            }
            CalculateFormation(groupFormationPositions[i]);
            unitDictionary.DeselectAll();            
        }
        groupFormationPositions.Clear();

        yield return new WaitForSeconds(2f);

        // Starting formation for right squad
        UnitInformation[] rightSquadUnits = rightSquad.GetComponentsInChildren<UnitInformation>();
        CalculateStartingFormation(rightSquad.transform.position, 2);
        for (int i = 0; i < setup.GetSquadCount(2); i++)
        {
            for (int j = 0 + (i*36); j < 36 *(i+1); j++)
            {
                unitDictionary.AddSelected(rightSquadUnits[j].gameObject);
            }
            CalculateFormation(groupFormationPositions[i]);
            unitDictionary.DeselectAll();            
        }
        groupFormationPositions.Clear();
    }

    private void CalculateStartingFormation(Vector3 destination, int squadIndex)
    {
        int formationWidth = setup.GetSquadCount(squadIndex);
        Vector3 middleOffset = new Vector3(formationWidth / 2, 0, 0);

        for (int x = 0; x < formationWidth; x++)
        {
            Vector3 pos = new Vector3(x, 0, 0);
            pos -= middleOffset;
            pos *= groupSpread;
            groupFormationPositions.Add(pos + destination);
        }
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
        if ((Mathf.Sqrt(unitDictionary.selectedTable.Count) % 1 == 0))
        {
            int formationWidth = (int)Mathf.Sqrt(unitDictionary.selectedTable.Count);
            int formationDepth = (int)Mathf.Sqrt(unitDictionary.selectedTable.Count);
            float offsetX = 0f;
            float offsetY = 0f;
            for (int i = 2; i < unitDictionary.selectedTable.Count + 1; i++)
            {
                if (Mathf.Sqrt(i) % 1 == 0)
                {
                    offsetX += 0.5f;
                    offsetY += 0.5f;
                }
            }

            Vector3 middleOffset = new Vector3(offsetX, 0, offsetY);

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
        else
        {
            int remainder = GetRemainder();
            int formationWidth = (int)Mathf.Sqrt(unitDictionary.selectedTable.Count - remainder);
            int formationDepth = (int)Mathf.Sqrt(unitDictionary.selectedTable.Count - remainder);

            float offsetX = 0f;
            float offsetY = 0f;
            for (int i = 2; i < unitDictionary.selectedTable.Count + 1; i++)
            {
                if (Mathf.Sqrt(i) % 1 == 0)
                {
                    offsetX += 0.5f;
                    offsetY += 0.5f;
                }
            }

            Vector3 middleOffset = new Vector3(offsetX, 0, offsetY);

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
            if (remainder < Mathf.Sqrt(unitDictionary.selectedTable.Count - remainder))
            {
                for (int i = 0; i < remainder; i++)
                {
                    Vector3 pos = new Vector3(formationWidth - i - 1, 0, formationDepth);
                    pos -= middleOffset;
                    pos *= spread;
                    formationPositions.Add(pos + destination);
                }
            }
            else
            {
                for (int i = 0; i < remainder -(remainder - Mathf.Sqrt(unitDictionary.selectedTable.Count - remainder)); i++)
                {
                    Vector3 pos = new Vector3(formationWidth - i - 1, 0, formationDepth);
                    pos -= middleOffset;
                    pos *= spread;
                    formationPositions.Add(pos + destination);
                }
                for (int i = 0; i < remainder - Mathf.Sqrt(unitDictionary.selectedTable.Count - remainder); i++)
                {
                    Vector3 pos = new Vector3(formationWidth - i - 1, 0, formationDepth + 1);
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
            if (unit.Value.GetComponent<NavMeshAgent>() != null)
            {
                unit.Value.GetComponent<NavMeshAgent>().SetDestination(formationPositions[i]);
                i++;
                if (i > formationPositions.Count)
                {
                    i = 0;
                }
            }
        }
    }

    private void ShuffleList()
    {
        System.Random rand = new System.Random();
        unitDictionary.selectedTable = unitDictionary.selectedTable.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);

    }

    private int GetRemainder()
    {
        int n = 0;
        for (int i = unitDictionary.selectedTable.Count; i > 0; i--)
        {
            if (Mathf.Sqrt(i) % 1 == 0)
            {
                n = unitDictionary.selectedTable.Count - i;
                break;
            }
        }
        return n;
    }
}
