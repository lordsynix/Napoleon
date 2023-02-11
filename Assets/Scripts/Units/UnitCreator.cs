using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitCreator : MonoBehaviour
{
    public Transform unitParent;
    [SerializeField] private GameObject[] unitPrefabs;

    private Dictionary<string, GameObject> nameToUnitDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (var unit in unitPrefabs)
        {
            nameToUnitDict.Add(unit.GetComponent<Unit>().GetType().ToString(), unit);
        }
    }
    public GameObject CreateUnit(Type type)
    {
        GameObject prefab = nameToUnitDict[type.ToString()];
        if (prefab)
        {
            GameObject newUnit = Instantiate(prefab, unitParent);
            return prefab;
        }
        return null;
    }
}
