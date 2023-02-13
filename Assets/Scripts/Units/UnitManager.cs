using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitCreator))]
public class UnitManager : MonoBehaviour
{
    [SerializeField] private ArmySetup startingSetup;
    private UnitCreator unitCreator;

    private void Awake()
    {
        unitCreator = GetComponent<UnitCreator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        BuildArmyFromLayout(startingSetup);
    }

    private void BuildArmyFromLayout(ArmySetup setup)
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < setup.GetSquadCount(j); i++)
            {
                string typeName = setup.GetGroupName(i, j);
                GroupType groupType = setup.GetGroupType(i, j);
                string squadName = setup.GetSquadName(j);
                Type type = Type.GetType(typeName);

                GenerateGroup(squadName, type, groupType);
            }
        }
    }

    private void GenerateGroup(string squadName, Type type, GroupType groupType)
    {
        for (int i = 0; i < 36; i++)
        {
            GenerateUnit(squadName, type, groupType);
        }
    }

    private void GenerateUnit(string squadName, Type type, GroupType groupType)
    {
        Unit newUnit = unitCreator.CreateUnit(type, squadName, groupType).GetComponent<Unit>();
    }
}
