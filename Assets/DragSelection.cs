using UnityEngine;

public class DragSelection : SelectionMode
{
    public Rect selectionArea;

    public bool IsSelectionAreaZero()
    {
        return selectionArea.width == 0 && selectionArea.height == 0;
    }

    private float ScreenToRectSpaceY(float y)
    {
        return Screen.height - y;
    }

    public void UpdateSelectionArea(Vector3 startClick, Vector3 currentMousePosition)
    {
        selectionArea = new Rect(startClick.x, ScreenToRectSpaceY(startClick.y),
                currentMousePosition.x - startClick.x,
                ScreenToRectSpaceY(currentMousePosition.y) - ScreenToRectSpaceY(startClick.y));

        if(selectionArea.width < 0)
        {
            selectionArea.x += selectionArea.width;
            selectionArea.width = -selectionArea.width;
        }
        if(selectionArea.height < 0)
        {
            selectionArea.y += selectionArea.height;
            selectionArea.height = -selectionArea.height;
        }
    }
    
    public void MouseIsBeingDragged(Vector3 startClick, Vector3 currentMousePosition)
    {
        UpdateSelectionArea(startClick, currentMousePosition);

        if(!IsSelectionAreaZero())
        {
            foreach(var unit in units)
            {
                if(unit.GetComponent<Renderer>().isVisible)
                {
                    UnitIsVisibleForCamera(unit); 
                }
                else
                {
                    selectionBehaviour.Unselect(unit);
                }
            }
        }
    }

    private void UnitIsVisibleForCamera(Unit unit)
    {
        Vector3 unitPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
        unitPosition.y = ScreenToRectSpaceY(unitPosition.y);

        if(selectionArea.Contains(unitPosition))
        {
            selectionBehaviour.Select(unit);
        }
        else
        {
            selectionBehaviour.Unselect(unit);
        }

    }
}
