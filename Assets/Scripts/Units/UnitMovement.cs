using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent unitNavMeshAgent;
    public SelectedUnitDictionary unitDictionary;

    void Start()
    {
        unitDictionary = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<SelectedUnitDictionary>();
        unitNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    }
}
