using UnityEngine;

public class DragSelection : SelectionMode
{
    public Rect dragArea;

    public bool IsDragAreaZero()
    {
        return dragArea.width == 0 && dragArea.height == 0;
    }

    private float ScreenToRectSpaceY(float y)
    {
        return Screen.height - y;
    }
    
    public void MouseIsBeingPressed(Vector3 startClick, Vector3 currentMousePosition)
    {
        UpdateDragArea(startClick, currentMousePosition);

        if(!IsDragAreaZero())
            MouseIsBeingDragged();     
    }

    public void UpdateDragArea(Vector3 startClick, Vector3 currentMousePosition)
    {
        dragArea = BuildDragArea(startClick, currentMousePosition); 
        AdjustAreaWhenDirectionIsNegative();
        
    }
    
    Rect BuildDragArea(Vector3 startClick, Vector3 currentMousePosition)
    {
        return new Rect(startClick.x,
                ScreenToRectSpaceY(startClick.y),
                currentMousePosition.x - startClick.x,
                ScreenToRectSpaceY(currentMousePosition.y) - ScreenToRectSpaceY(startClick.y)); 
    }

    void AdjustAreaWhenDirectionIsNegative()
    {
        if(dragArea.width < 0) {
            dragArea.x += dragArea.width;
            dragArea.width = -dragArea.width;
        }

        if(dragArea.height < 0) {
            dragArea.y += dragArea.height;
            dragArea.height = -dragArea.height;
        }
    }

    private void MouseIsBeingDragged()
    {
        foreach(var unit in units) {
            if(IsUnitVisibleForCamera(unit) && IsUnitUnderDragArea(unit))
                selectionBehaviour.Select(unit);
            else if(!IsMultiSelectionModifierOn)
                selectionBehaviour.Unselect(unit);
        }
    }
    
    private bool IsUnitVisibleForCamera(Unit unit)
    {
        return unit.GetComponent<Renderer>().isVisible;
    }

    private bool IsUnitUnderDragArea(Unit unit)
    {
        Vector3 unitPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
        unitPosition.y = ScreenToRectSpaceY(unitPosition.y);

        return dragArea.Contains(unitPosition);
    }
}
