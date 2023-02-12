using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitCreator : MonoBehaviour
{
    public Transform leftParent;
    public Transform centerParent;
    public Transform rightParent;
    [SerializeField] private GameObject[] unitPrefabs;

    private Dictionary<string, GameObject> nameToUnitDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var unit in unitPrefabs)
        {
            nameToUnitDict.Add(unit.GetComponent<Unit>().GetType().ToString(), unit);
        }
    }
    public GameObject CreateUnit(Type type, string squadName)
    {
        if (squadName == "left")
        {
            GameObject prefab = nameToUnitDict[type.ToString()];
            if (prefab)
            {
                GameObject newUnit = Instantiate(prefab, leftParent);
                return prefab;
            }
            return null;
        }

        else if (squadName == "center")
        {
            GameObject prefab = nameToUnitDict[type.ToString()];
            if (prefab)
            {
                GameObject newUnit = Instantiate(prefab, centerParent);
                return prefab;
            }
            return null;
        }

        else if (squadName == "right")
        {
            GameObject prefab = nameToUnitDict[type.ToString()];
            if (prefab)
            {
                GameObject newUnit = Instantiate(prefab, rightParent);
                return prefab;
            }
            return null;
        }

        else
        {
            return null;
        }
    }
}
