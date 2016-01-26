using UnityEngine;
using System.Collections.Generic;
using FBLA.Game.AI;

public class InteractionMgr
{
    public static void OnGameObjInteractionClicked(GameObject clickedGameObj, Vector3 clickedLoc)
    {
        GameUnit clickedGameUnit = clickedGameObj.GetComponent<GameUnit>();
        if (clickedGameUnit != null)
        {
            UnitClicked(clickedGameUnit);
        }
        if (clickedGameObj.name == "Terrain")
            OnTerrainInteraction(clickedGameObj, clickedLoc);
    }

    private static void UnitClicked(GameUnit clickedGameUnit)
    {
        if (clickedGameUnit.Team != 0)
        {
            Debug.Log("Clicked on enemy");
            SetStateForAllSelected(new ChaseState(clickedGameUnit));
        }
        else
        {
            // For now just move the selected units towards the clicked unit.
            SetStateForAllSelected(new MoveState(clickedGameUnit.GetPos()));
        }
    }

    private static void OnTerrainInteraction(GameObject clickedGameObj, Vector3 clickedLoc)
    {
        SetStateForAllSelected(new MoveState(clickedLoc));
    }

    private static void SetStateForAllSelected(UnitState<GameEntity> setState)
    {
        IEnumerable<GameUnit> selectedUnits = GetSelectedUnits();
        foreach (GameUnit selectedUnit in selectedUnits)
        {
            selectedUnit.GetStateMachine().ChangeState(setState);
        }
    }

    private static IEnumerable<GameUnit> GetSelectedUnits()
    {
        GameObject[] gameObjs = GameObject.FindGameObjectsWithTag("GameUnit");
        foreach (GameObject gameObj in gameObjs)
        {
            GameUnit gameUnit = gameObj.GetComponent<GameUnit>();
            if (gameUnit == null)
                continue;

            if (gameUnit.Selected)
                yield return gameUnit;
        }
    }
}