using UnityEngine;
using System.Collections;

public abstract class SelectablePlayer : GamePlayer
{

    public bool Selected = false;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (GetTeam() == 0)
            UpdateIsSelected();
    }

    public void UpdateIsSelected()
    {
        if (Input.GetMouseButtonUp(0) && CameraBehavior.Selection.width * CameraBehavior.Selection.height > 2)
        {
            if (CameraBehavior.Selection.width < 0)
            {
                CameraBehavior.Selection.x += CameraBehavior.Selection.width;
                CameraBehavior.Selection.width = -CameraBehavior.Selection.width;
            }
            if (CameraBehavior.Selection.height < 0)
            {
                CameraBehavior.Selection.y += CameraBehavior.Selection.height;
                CameraBehavior.Selection.height = -CameraBehavior.Selection.height;
            }
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraBehavior.InvertMouseY(camPos.y);
            Selected = CameraBehavior.Selection.Contains(camPos);
        }
        CheckUnitClick();
        if (Selected)
            SetSelectedColor(Color.red);
        else
            SetSelectedColor(Color.white);
    }


    private void CheckUnitClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "GameUnit")
                {
                    if (GetComponent<Collider>().bounds.Contains(hit.point))
                    {
                        DeselectAllUnits();
                        Selected = true;
                    }
                }
            }
        }
    }

    public void DeselectAllUnits()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GameUnit"))
        {
            // Alll units that are selectable must go here?
            GameUnit gameUnit = obj.GetComponent<GameUnit>();
            if (gameUnit != null)
                gameUnit.Selected = false;
        }
    }
}
