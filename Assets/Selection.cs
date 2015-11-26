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
    
    public Unit[] units{
        get
        {
            return _units;
        }
        set
        {
            _units = value;
            UpdateSelectionModes();
        }
    }

    public SelectionBehaviour selectionBehaviour
    {
        get
        {
            return _selectionBehaviour;
        }
        set
        {
            _selectionBehaviour = value;
            UpdateSelectionModes();
        }
    }

    // End - API

    private Vector3 startClick = -Vector3.one;

    private DragSelection dragSelection = new DragSelection();
    private ClickSelection clickSelection = new ClickSelection();

    private void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color (1,1,1,0.5f);
            GUI.DrawTexture(dragSelection.dragArea, _selectionHighlight);
        }
    }

    private void Start()
    {
        UpdateSelectionModes(); 
    }

    private void UpdateSelectionModes()
    {
        clickSelection.units = _units;
        dragSelection.units = _units;

        clickSelection.selectionBehaviour = selectionBehaviour;
        dragSelection.selectionBehaviour = selectionBehaviour;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
            MouseButtonWasPressed();
        else if(Input.GetMouseButtonUp(0))
            MouseButtonWasReleased(); 
        
        if(Input.GetMouseButton(0))
            MouseButtonIsBeingPressed();
    }

    private void MouseButtonWasPressed()
    {
        startClick = Input.mousePosition;
    }

    private void MouseButtonWasReleased()
    {
        if(dragSelection.IsDragAreaZero())
            clickSelection.MouseWasClicked(startClick);

        startClick = -Vector3.one;
        dragSelection.dragArea = new Rect(0,0,0,0);
    }

    private void MouseButtonIsBeingPressed()
    {
        dragSelection.MouseIsBeingPressed(startClick, Input.mousePosition);
    }
}
