using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {
    public bool Selected = false;

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
        transform.position = positionToMoveTo;
    }

    void SetSelectedColor(Color color)
    {
        // All the rendering components of the children must be set as well.
        Renderer[] childrenRenderers = GetComponentsInChildren<Renderer>() as Renderer[];
        foreach (Renderer childrenRenderer in childrenRenderers)
            childrenRenderer.material.color = color;
    }
}
