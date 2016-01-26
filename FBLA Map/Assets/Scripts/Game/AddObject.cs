using UnityEngine;
using System.Collections;

public class AddObject : MonoBehaviour {
    public GameObject[] SpawnBases;

    public GameObject SpawnObject(string objectName, int baseNum)
    {
        GameObject gameObj = (GameObject)Instantiate(Resources.Load(objectName));

        Vector3 spawnOffset = new Vector3(0.0f, 0.0f, 30.0f);
        gameObj.transform.position = SpawnBases[baseNum].transform.position + spawnOffset;
        while (IsInOtherUnit(gameObj, gameObj.GetComponent<Collider>().bounds))
        {
            gameObj.transform.Translate(new Vector3(0.5f, 0.0f, 0.0f));
        }

        return gameObj;
    }

    public void SpawnObject(string objectName)
    {
        SpawnObject(objectName, 0);
    }

    private bool IsInOtherUnit(GameObject obj, Bounds bounds)
    {
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("GameUnit"))
        {
            if (!unit.Equals(null) && !unit.GetComponent<Collider>().Equals(null) && !unit.Equals(obj) && unit.GetComponent<Collider>().bounds.Intersects(bounds))
            {
                return true;
            }
        }
        return false;
    }
}
