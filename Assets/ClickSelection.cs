using UnityEngine;

public class ClickSelection
{
    private SelectionBehaviour _selectionBehaviour;
    public ClickSelection(SelectionBehaviour selectionBehaviour)
    {
        _selectionBehaviour = selectionBehaviour;
    }

    public void MakeSelection(Unit[] units, Vector3 clickPoint)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(clickPoint), out hitInfo);
        if (hit) 
        {
            Unit unitHit = hitInfo.transform.gameObject.GetComponent<Unit>();
            if(unitHit != null)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                SingleUnitHit(unitHit, units);
            }
            else
            {
                Debug.Log("No hit");
                NoUnitsHit(units);
            }
        } 
        else 
        {
            Debug.Log("No hit");
            NoUnitsHit(units);
        }

    }

    private void SingleUnitHit(Unit unitHit, Unit[] units)
    {
        if(unitHit != null)
        {
            _selectionBehaviour.Select(unitHit); 
        }
        foreach(var unit in units)
        {
            if(unit != unitHit && unit.selected)
            {
                _selectionBehaviour.Unselect(unit);
            }                  
        }
    }

    private void NoUnitsHit(Unit[] units)
    {
        foreach(var unit in units)
        {
            if(unit.selected)
            {
                _selectionBehaviour.Unselect(unit);
            }
        }
    }
}
