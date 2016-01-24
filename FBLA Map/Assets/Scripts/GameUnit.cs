using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {

    private const float HEALTH_BAR_WIDTH = 100.0f;
    private const float HEALTH_BAR_HEIGHT = 5.0f;

    // Variables to be set in the editor.
    public float UnitSpeed = 10.0f;
    public float MaxUnitHealth = 100.0f;
    public Texture2D HealthBarTexture = null;

    public bool Selected = false;
    public float CurrentUnitHealth = 100.0f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = UnitSpeed;
    }

	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonUp(0))
        {
            if (CameraMovement.Selection.width < 0)
            {
                CameraMovement.Selection.x += CameraMovement.Selection.width;
                CameraMovement.Selection.width = -CameraMovement.Selection.width;
            }
            if (CameraMovement.Selection.height < 0)
            {
                CameraMovement.Selection.y += CameraMovement.Selection.height;
                CameraMovement.Selection.height = -CameraMovement.Selection.height;
            }
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraMovement.InvertMouseY(camPos.y);
            Selected = CameraMovement.Selection.Contains(camPos);
        }
        if (Selected)
            SetSelectedColor(Color.red);
        else
            SetSelectedColor(Color.white);
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.Repaint && Selected)
        {
            Vector3 shiftedPos = transform.position;
            shiftedPos.y += 10.0f;

            Vector3 screenSpacePoint = WorldToScreenPoint(shiftedPos);
            // Display the units health bar. Also draw it centered.
            float percentageFull = CurrentUnitHealth / MaxUnitHealth;
            GUI.DrawTexture(new Rect(screenSpacePoint.x - (HEALTH_BAR_WIDTH / 2.0f), screenSpacePoint.y, HEALTH_BAR_WIDTH * percentageFull, HEALTH_BAR_HEIGHT), HealthBarTexture);
        }
    }

    public Vector3 WorldToScreenPoint(Vector3 position)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
        screenPosition.y = Screen.height - screenPosition.y;
        return screenPosition;
    }

    public void MoveTo(Vector3 positionToMoveTo)
    {
        agent.destination = positionToMoveTo;
    }

    void SetSelectedColor(Color color)
    {
        // All the rendering components of the children must be set as well.
        Renderer[] childrenRenderers = GetComponentsInChildren<Renderer>() as Renderer[];
        foreach (Renderer childrenRenderer in childrenRenderers)
            childrenRenderer.material.color = color;
    }
}
