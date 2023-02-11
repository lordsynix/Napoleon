using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    private void Start()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject unit in units)
        {
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), unit.GetComponent<CapsuleCollider>());
            Debug.Log(unit.tag);
        }
    }
}