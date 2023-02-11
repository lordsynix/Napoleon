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
        for (int i = 0; i < setup.GetUnitCount(); i++)
        {
            Vector3 unitLocation = setup.GetPosition(i);
            GroupType groupType = setup.GetGroupType(i);
            SquadType squadType = setup.GetSquadType(i);
            Side side = setup.GetSide(i);
            string typeName = setup.GetUnitName(i);

            Type type = Type.GetType(typeName);

            GenerateUnit(unitLocation, groupType, squadType, side, type);
        }
    }

    private void GenerateUnit(Vector3 unitLocation, GroupType groupType, SquadType squadType, Side side, Type type)
    {
        Unit newUnit = unitCreator.CreateUnit(type).GetComponent<Unit>();
        newUnit.SetData(unitLocation, groupType, squadType, side);
    }
}
