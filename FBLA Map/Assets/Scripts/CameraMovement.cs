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
    private const float SEL_TIME = 2.0f;

    ///////////////////////////////

    public static Rect Selection = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
    public Texture2D SelectedHighlight = null;

    private Vector3 _startClick = -Vector3.one;
    private float _selectionStartTime = -1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCamera();
        CheckCameraSelection();

        CheckMoveClick();
    }

    private void CheckMoveClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Terrain")
                {
                    Vector3 newPos = hit.point;

                    GameObject[] gameObjs = GameObject.FindGameObjectsWithTag("GameUnit");
                    foreach (GameObject gameObj in gameObjs)
                    {
                        GameUnit gameUnit = gameObj.GetComponent<GameUnit>();
                        if (gameUnit == null)
                            continue;

                        if (gameUnit.Selected)
                        {
                            gameUnit.MoveTo(newPos);
                        }
                    }
                }
            }
        }
    }

    private void CheckCameraSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startClick = Input.mousePosition;
            _selectionStartTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Selection.width < 0)
            {
                Selection.x += Selection.width;
                Selection.width = -Selection.width;
            }
            if (Selection.height < 0)
            {
                Selection.y += Selection.height;
                Selection.height = -Selection.height;
            }
        }

        if (Input.GetMouseButton(0))
            Selection = new Rect(_startClick.x, InvertMouseY(_startClick.y), Input.mousePosition.x - _startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(_startClick.y));
    }

    private void OnGUI()
    {
        float sinceSelection = Mathf.Abs(Time.time - _selectionStartTime);
        if (sinceSelection > SEL_TIME)
        {
            // Deselect the rectangle.
            _startClick = -Vector3.one;
            Selection = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
        }

        if (_startClick != -Vector3.one)
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(Selection, SelectedHighlight);
        }
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
