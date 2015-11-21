using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {

    [SerializeField]
    private Texture2D _selectionHighlight;
    [SerializeField]
    private Unit[] _units = new Unit[0];

    public Texture2D selectionHighlight
    {
        get
        {
            return _selectionHighlight;
        }
        set
        {
            _selectionHighlight = value;
        }
    }

    public Unit[] units  
    {
        get
        {
            return _units;
        }
        set
        {
            _units = value;
        }
    }

    private Rect seletionRect = new Rect(0,0,0,0);
    private Vector3 startClick = -Vector3.one;
    private Color selectionColor = Color.green;

    private float ScreenToRectSpaceY(float y)
    {
        return Screen.height - y;
    }

    private void Select(Unit unit)
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
                Vector3 unitPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
                unitPosition.y = ScreenToRectSpaceY(unitPosition.y);
                
                if(seletionRect.Contains(unitPosition))
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

    private void HandleClickSelection()
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

    private void UpdateMultiSelectionRect()
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

                HandleClickSelection();
            }

            startClick = -Vector3.one;
            seletionRect = new Rect(0,0,0,0);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateMultiSelectionRect();
            if(seletionRect.width > 0 || seletionRect.height > 0)
            { 
                HandleDragSelection();
            }
        }
    }

    // unity 

    private void Update()
    {
        HandleSelection();
    }

    private void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color (1,1,1,0.5f);
            GUI.DrawTexture(seletionRect, selectionHighlight);
        }
    }
}
