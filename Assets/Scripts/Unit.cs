using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static Unit instance;

    public GameObject[] unitUI;
    
    public Dictionary<int, string> unitTypeFromInt = new Dictionary<int, string>()
    {
        [1] = "Infantry",
        [2] = "Cavalry",
        [3] = "Artillery",
        [4] = "Logistics"

    };

    private void Awake()
    {
        instance = this;
    }
}
