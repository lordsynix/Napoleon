using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformation : MonoBehaviour
{
    private GroupType groupType;

    public GroupType GetGroupType()
    {
        return groupType;
    }

    public void SetGroupType(GroupType newGroupType)
    {
        groupType = newGroupType;
    }
}
