using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Layout")]
public class ArmySetup : ScriptableObject
{
    [Serializable]
    private class ArmyBuilder
    {
        public Vector3 position;
        public UnitType unitType;
        public GroupType groupType;
        public SquadType squadType;
        public Side side;
    }

    [SerializeField] private ArmyBuilder[] armyBuild;

    public int GetUnitCount()
    {
        return armyBuild.Length;
    }

    public Vector3 GetPosition(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return Vector3.zero;
        }
        return new Vector3(armyBuild[index].position.x, armyBuild[index].position.y, armyBuild[index].position.z);
    }   
    
    public string GetUnitName(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return "";
        }
        return armyBuild[index].unitType.ToString();
    }

    public UnitType GetUnitType(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return UnitType.Infantry;
        }
        return armyBuild[index].unitType;
    }

    public GroupType GetGroupType(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return GroupType.infantry;
        }
        return armyBuild[index].groupType;
    }

    public SquadType GetSquadType(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return SquadType.center;
        }
        return armyBuild[index].squadType;
    }

    public Side GetSide(int index)
    {
        if (armyBuild.Length <= index)
        {
            Debug.LogError("Index of unit is out of range!");
            return Side.ally;
        }
        return armyBuild[index].side;
    }
}
