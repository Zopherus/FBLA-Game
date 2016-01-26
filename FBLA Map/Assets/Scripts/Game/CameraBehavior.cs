﻿using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{

    ///////////////////////
    // CONSTANTS
    ////////////////////

    // Camera movement variables.
    private const float SPEED = 20.0f;
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
    public Texture2D DefaultCursor = null;
    public Texture2D AttackCursor = null;

    private Vector3 _startClick = -Vector3.one;
    private bool deselectRect = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        CheckCameraSelection();

        CheckMoveClick();
    }

    private void UpdateCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        Texture2D setTexture = DefaultCursor;

        if (Physics.Raycast(ray, out hit))
        {
            GameEntity hitGameUnit = hit.collider.gameObject.GetComponent<GameEntity>();
            if (hitGameUnit != null && hitGameUnit.GetTeam() != 0)
            {
                setTexture = AttackCursor;
            }
        }

        float cursorSizeX = setTexture.width;
        float cursorSizeY = setTexture.height;

        GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, cursorSizeX, cursorSizeY), setTexture);
        //TODO:
        // Uncomment this line in production.
        //Cursor.visible = false;
    }

    private void CheckMoveClick()
    {
        // Right click.
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                InteractionMgr.OnGameObjInteractionClicked(hit.collider.gameObject, hit.point);
            }
        }
    }

    private void CheckCameraSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startClick = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            deselectRect = true;
        }

        if (Input.GetMouseButton(0))
            Selection = new Rect(_startClick.x, InvertMouseY(_startClick.y), Input.mousePosition.x - _startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(_startClick.y));
    }

    private void OnGUI()
    {
        if (deselectRect)
        {
            // Deselect the rectangle.
            _startClick = -Vector3.one;
            Selection = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
            deselectRect = false;
        }

        if (_startClick != -Vector3.one)
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(Selection, SelectedHighlight);
        }

        UpdateCursor();
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
            transform.Translate(new Vector3(0, -SPEED * Time.deltaTime * Mathf.Tan(0.95993108859f), -SPEED* Time.deltaTime * (1/ Mathf.Tan(0.95993108859f))));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //55 degrees in radians
            transform.Translate(new Vector3(0, SPEED * Time.deltaTime * Mathf.Tan(0.95993108859f), SPEED * Time.deltaTime * (1/Mathf.Tan(0.95993108859f))));
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * CAMERA_SENS;
        fov = Mathf.Clamp(fov, MIN_FOV, MAX_FOV);
        Camera.main.fieldOfView = fov;
    }
}
