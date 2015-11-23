using UnityEngine;
using System.Collections.Generic;

public class Selection : MonoBehaviour {

    // API
    
    [SerializeField]
    private Texture2D _selectionHighlight;
    [SerializeField]
    private Unit[] _units = new Unit[0];
    [SerializeField]
    private SelectionBehaviour _selectionBehaviour;
    
    public Texture2D selectionHighlight{get{return _selectionHighlight;}set{_selectionHighlight = value;}}
    public Unit[] units{get{return _units;}set{_units = value;}}
    public SelectionBehaviour selectionBehaviour{get{return _selectionBehaviour;}set{_selectionBehaviour = value;}}

    // End - API

    private Vector3 startClick = -Vector3.one;

    private DragSelection _dragSelection;
    private ClickSelection _clickSelection;

    private void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color (1,1,1,0.5f);
            GUI.DrawTexture(_dragSelection.selectionArea, selectionHighlight);
        }
    }

    private void Start()
    {
        _dragSelection = new DragSelection(selectionBehaviour);
        _clickSelection = new ClickSelection(selectionBehaviour);
    }

    private void Update()
    {
        HandleSelection();
    }

    private void HandleSelection()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(_dragSelection.IsSelectionAreaZero())
            {
                // not dragging, ok to perform click selection
                _clickSelection.MakeSelection(units, startClick);
            }

            startClick = -Vector3.one;
            _dragSelection.selectionArea = new Rect(0,0,0,0);
        }

        if(Input.GetMouseButton(0))
        {
            _dragSelection.UpdateSelectionArea(startClick, Input.mousePosition); 
            _dragSelection.MakeSelection(units);
        }

    }
}
