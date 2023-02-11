using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 currentLocation;
    public Side side;
    public GroupType groupType;
    public SquadType squadType;

    public void SetData(Vector3 unitLocation, GroupType groupType, SquadType squadType, Side side)
    {
        currentLocation = unitLocation;
        this.groupType = groupType;
        this.squadType = squadType;
        this.side = side;
    }
}
