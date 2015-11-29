using UnityEngine;

public class ClickSelection : SelectionMode
{
    private Unit unitHit;

    public void MouseWasClicked(Vector3 click)
    {
        RaycastHit hitInfo = new RaycastHit();
        var ray = Camera.main.ScreenPointToRay(click);

        if (Physics.Raycast(ray, out hitInfo))
            ThereWasARayHit(hitInfo);
        else
            NoUnitsWereHit();
    }

    private void ThereWasARayHit(RaycastHit hitInfo)
    {
        unitHit = hitInfo.transform.gameObject.GetComponent<Unit>();
        if(unitHit != null)
            UnitWasHit();
        else
            NoUnitsWereHit();
    }

    private void UnitWasHit()
    {
        selectionBehaviour.Select(unitHit); 

        if(!IsMultiSelectionModifierOn)
        {
            UnselectAllButHit();
        }
    }

    private void UnselectAllButHit()
    {
        foreach(var unit in units)
            if(unit != unitHit && unit.selected)
                selectionBehaviour.Unselect(unit);
    }

    private void NoUnitsWereHit()
    {
        foreach(var unit in units)
            if(unit.selected)
                selectionBehaviour.Unselect(unit);
    }
}
