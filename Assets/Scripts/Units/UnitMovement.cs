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

    public GameObject leftSquad;
    public GameObject centerSquad;
    public GameObject rightSquad;

    public float spread;

    private void Start()
    {
        unitDictionary = GetComponent<SelectedUnitDictionary>();
        StartCoroutine(StartingFormation());
    }

    private IEnumerator StartingFormation()
    {
        yield return new WaitForSeconds(2f);

        // Starting formation for left squad
        Transform[] leftSquadUnits = leftSquad.GetComponentsInChildren<Transform>();
        foreach (Transform unit in leftSquadUnits)
        {
            unitDictionary.AddSelected(unit.gameObject);
        }

        CalculateFormation(leftSquad.transform.position);
        unitDictionary.DeselectAll();

        // Starting formation for center squad
        Transform[] centerSquadUnits = centerSquad.GetComponentsInChildren<Transform>();
        foreach (Transform unit in centerSquadUnits)
        {
            unitDictionary.AddSelected(unit.gameObject);
        }

        CalculateFormation(centerSquad.transform.position);
        unitDictionary.DeselectAll();

        // Starting formation for right squad
        Transform[] rightSquadUnits = rightSquad.GetComponentsInChildren<Transform>();
        foreach (Transform unit in rightSquadUnits)
        {
            unitDictionary.AddSelected(unit.gameObject);
        }

        CalculateFormation(rightSquad.transform.position);
        unitDictionary.DeselectAll();


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
