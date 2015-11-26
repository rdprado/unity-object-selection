using UnityEngine;

public class ClickSelection : SelectionMode
{
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
        Unit unitHit = hitInfo.transform.gameObject.GetComponent<Unit>();
        if(unitHit != null)
            UnitWasHit(unitHit);
        else
            NoUnitsWereHit();
    }

    private void UnitWasHit(Unit unitHit)
    {
        selectionBehaviour.Select(unitHit); 

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
