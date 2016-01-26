using UnityEngine;
using System.Collections.Generic;
using System;

public class EntityMgr
{
    public static List<GameEntity> GetAllEntities()
    {
        return GetAllEntities(null);
    }

    public static List<GameEntity> GetAllEntities(Func<GameEntity, Boolean> filter)
    {
        List<GameEntity> allEntities = new List<GameEntity>();


        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GameUnit"))
        {
            GameEntity gameUnit = obj.GetComponent<GameEntity>();
            if  (gameUnit != null && (filter == null || filter(gameUnit)))
                allEntities.Add(gameUnit);
        }

        return allEntities;
    }
}
