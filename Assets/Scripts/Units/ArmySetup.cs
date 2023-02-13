using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Layout")]
public class ArmySetup : ScriptableObject
{
    [Serializable]
    private class LeftFlank
    {
        public GroupType groupType;
    }

    [Serializable]
    private class Center
    {
        public GroupType groupType;
    }

    [Serializable]
    private class RightFlank
    {
        public GroupType groupType;
    }

    [SerializeField] private LeftFlank[] leftFlankBuild;
    [SerializeField] private Center[] centerBuild;
    [SerializeField] private RightFlank[] rightFlankBuild;  
    
    public string GetGroupName(int index, int squadIndex)
    {
        if (squadIndex == 0)
        {   
            if (leftFlankBuild.Length <= index)
            {
                Debug.LogError("Index of group is out of range!");
                return "";
            }
            return leftFlankBuild[index].groupType.ToString();
        }
        else if (squadIndex == 1)
        {
            if (centerBuild.Length <= index)
            {
                Debug.LogError("Index of group is out of range!");
                return "";
            }
            return centerBuild[index].groupType.ToString();
        }
        else if (squadIndex == 2)
        {
            if (rightFlankBuild.Length <= index)
            {
                Debug.LogError("Index of unit is out of range!");
                return "";
            }
            return rightFlankBuild[index].groupType.ToString();
        }
        else
        {
            Debug.LogError("Index of squad is out of range!");
            return "";
        }
    }

    public GroupType GetGroupType(int index, int squadIndex)
    {
        if (squadIndex == 0)
        {
            if (leftFlankBuild.Length <= index)
            {
                Debug.LogError("Index of group is out of range!");
                return GroupType.Infantry;
            }
            return leftFlankBuild[index].groupType;
        }
        else if (squadIndex == 1)
        {
            if (centerBuild.Length <= index)
            {
                Debug.LogError("Index of group is out of range!");
                return GroupType.Infantry;
            }
            return centerBuild[index].groupType;
        }
        else if (squadIndex == 2)
        {
            if (rightFlankBuild.Length <= index)
            {
                Debug.LogError("Index of unit is out of range!");
                return GroupType.Infantry;
            }
            return rightFlankBuild[index].groupType;
        }
        else
        {
            Debug.LogError("Index of squad is out of range!");
            return GroupType.Infantry;
        }
    }

    public string GetSquadName(int squadIndex)
    {
        if (squadIndex == 0)
        {
            return "left";
        }
        else if (squadIndex == 1)
        {
            return "center";
        }
        else if (squadIndex == 2)
        {
            return "right";
        }
        else
        {
            Debug.LogError("Index of squad is out of range!");
            return "";
        }
    }

    public int GetSquadCount(int squadIndex)
    {
        if (squadIndex == 0)
        {
            return leftFlankBuild.Length;
        }
        else if (squadIndex == 1)
        {
            return centerBuild.Length;
        }
        else if (squadIndex == 2)
        {
            return rightFlankBuild.Length;
        }
        else
        {
            Debug.LogError("Index of squad is out of range!");
            return 0;
        }
    }
}
