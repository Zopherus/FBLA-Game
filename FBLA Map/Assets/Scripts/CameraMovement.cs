using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private const float SPEED = 10.0f;
    private const float minFov = 15.0f;
    private const float maxFov = 90.0f;
    private const float sensitivity = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(SPEED * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-SPEED * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -SPEED * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, SPEED * Time.deltaTime, 0));
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
