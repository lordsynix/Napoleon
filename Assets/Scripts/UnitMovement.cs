using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent unitNavMeshAgent;

    void Start()
    {
        unitNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && GetComponent<SelectedUnit>() != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50000))
            {
                unitNavMeshAgent.SetDestination(hit.point);
            }
        }
    }
}
