using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {

    public Texture2D selectionHighlight;
    public static Rect seletionRect = new Rect(0,0,0,0);
    private Vector3 startClick = -Vector3.one;

    public Unit[] units = new Unit[0];

    private Color selectionColor = Color.green;

    private float ScreenToRectSpaceY(float y)
    {
        return Screen.height - y;
    }

    public void Select(Unit unit)
    {
        unit.selected = true;
        unit.GetComponent<Renderer>().material.color = selectionColor;
    }

    private void Unselect(Unit unit)
    {
        unit.selected = false;
        unit.GetComponent<Renderer>().material.color = unit.materialColor;
    }
    
    private void HandleDragSelection()
    {
        foreach(var unit in units)
        {
            if(unit.GetComponent<Renderer>().isVisible)
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(unit.transform.position);
                camPos.y = ScreenToRectSpaceY(camPos.y);
                
                if(seletionRect.Contains(camPos))
                {
                    Select(unit);
                }
                else
                {
                    Unselect(unit);
                }

            }
            else
            {
                Unselect(unit);
            }
        }
    }

    private void HandleClickSelection(Vector3 startClick)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(startClick), out hitInfo);
        if (hit) 
        {
            Unit unitHit = hitInfo.transform.gameObject.GetComponent<Unit>();
            if(unitHit != null)
            {
                Select(unitHit); 
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            }
            foreach(var unit in units)
            {
                if(unit != unitHit && unit.selected)
                {
                    Unselect(unit);
                }                  
            }
        } 
        else 
        {
            Debug.Log("No hit");
            foreach(var unit in units)
            {
                if(unit.selected)
                {
                    Unselect(unit);
                }
            }
        }
    }

    private void UpdateMultiSelectionRect(Vector3 startClick)
    {
        seletionRect = new Rect(startClick.x, ScreenToRectSpaceY(startClick.y),
                Input.mousePosition.x - startClick.x,
                ScreenToRectSpaceY(Input.mousePosition.y) - ScreenToRectSpaceY(startClick.y));

        if(seletionRect.width < 0)
        {
            seletionRect.x += seletionRect.width;
            seletionRect.width = -seletionRect.width;
        }
        if(seletionRect.height < 0)
        {
            seletionRect.y += seletionRect.height;
            seletionRect.height = -seletionRect.height;
        }
    }

    private void HandleSelection()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(seletionRect.width == 0 && seletionRect.height == 0)
            {
                // not multiselecting, ok to perform click selection

                HandleClickSelection(startClick);
            }

            startClick = -Vector3.one;
            seletionRect = new Rect(0,0,0,0);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateMultiSelectionRect(startClick);
            if(seletionRect.width > 0 || seletionRect.height > 0)
            { 
                HandleDragSelection();
            }
        }
    }

    

    void Update()
    {
        HandleSelection();
    }

    void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color (1,1,1,0.5f);
            GUI.DrawTexture(seletionRect, selectionHighlight);
        }
    }
}
