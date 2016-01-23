using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    ///////////////////////
    // CONSTANTS
    ////////////////////

    // Camera movement variables.
    private const float SPEED = 10.0f;
    // The minimum field of view.
    private const float MIN_FOV = 15.0f;
    // The maximum field of view.
    private const float MAX_FOV = 90.0f;
    // The camera sensitivity.
    private const float CAMERA_SENS = 10.0f;

    ///////////////////////////////

    public Texture2D _selectionHighlight = null;
    public static Rect _selection = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    private Vector3 _startClick = -Vector3.one;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCamera();
    }

    private void CheckCameraSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startClick = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_selection.width < 0)
            {
                _selection.x += _selection.width;
                _selection.width = -_selection.width;
            }
            if (_selection.height < 0)
            {
                _selection.y += _selection.height;
                _selection.height = -_selection.height;
            }
        }

        if (Input.GetMouseButton(0))
            _selection = new Rect(_startClick.x, InvertMouseY(_startClick.y), Input.mousePosition.x - _startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(_startClick.y));
    }

    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }

    private void MoveCamera()
    {
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
        fov += Input.GetAxis("Mouse ScrollWheel") * CAMERA_SENS;
        fov = Mathf.Clamp(fov, MIN_FOV, MAX_FOV);
        Camera.main.fieldOfView = fov;
    }
}
