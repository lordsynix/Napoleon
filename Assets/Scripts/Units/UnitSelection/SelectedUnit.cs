using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnDestroy()
    {
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
