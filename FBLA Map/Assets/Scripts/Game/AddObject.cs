using UnityEngine;
using System.Collections;

public class AddObject : MonoBehaviour {
    public GameObject spawnBase;

    public void SpawnObject(string objectName)
    {
        GameObject gameObj = (GameObject)Instantiate(Resources.Load(objectName));

        gameObj.transform.parent = spawnBase.transform;
        while (isInOtherUnit(gameObj, gameObj.GetComponent<Collider>().bounds))
        {
            gameObj.transform.Translate(new Vector3(0.5f, 0.0f, 0.0f));
        }

    }

    public bool isInOtherUnit(GameObject obj, Bounds bounds)
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
