using UnityEngine;
using System.Collections;

public class AddObject : MonoBehaviour {
    public GameObject spawnBase;

    public void SpawnObject(string objectName)
    {
        //GameObject gameObj = (GameObject)Instantiate(Resources.Load(objectName));
        GameObject gameObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        gameObj.transform.parent = spawnBase.transform;
        gameObj.transform.localPosition = new Vector3(10.0f, 10.0f, 0.0f);
        //gameObj.transform.Translate(30.0f, 5.0f, 0.0f);
    }
}
