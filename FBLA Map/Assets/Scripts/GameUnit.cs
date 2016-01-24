using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour {
    public bool selected = false;

	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonUp(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraMovement.InvertMouseY(camPos.y);
            selected = CameraMovement.Selection.Contains(camPos);
        }
        if (selected)
            SetSelectedColor(Color.red);
        else
            SetSelectedColor(Color.white);
    }

    void SetSelectedColor(Color color)
    {
        // All the rendering components of the children must be set as well.
        Renderer[] childrenRenderers = GetComponentsInChildren<Renderer>() as Renderer[];
        foreach (Renderer childrenRenderer in childrenRenderers)
            childrenRenderer.material.color = color;
    }
}
