using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {
    private const float UNIT_SPEED = 10.0f;

    public bool Selected = false;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.speed = UNIT_SPEED;
    }

	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonUp(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraMovement.InvertMouseY(camPos.y);
            Selected = CameraMovement.Selection.Contains(camPos);
        }
        if (Selected)
            SetSelectedColor(Color.red);
        else
            SetSelectedColor(Color.white);
    }

    public void MoveTo(Vector3 positionToMoveTo)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
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
