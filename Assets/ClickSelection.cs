using UnityEngine;

public class ClickSelection : SelectionMode
{
    public void MouseWasClicked(Vector3 click)
    {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(click), out hitInfo);
        if (hit) 
        {
            Unit unitHit = hitInfo.transform.gameObject.GetComponent<Unit>();
            if(unitHit != null)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                UnitWasHit(unitHit);
            }
            else
            {
                Debug.Log("No hit");
                NoUnitsWereHit();
            }
        } 
        else 
        {
            Debug.Log("No hit");
            NoUnitsWereHit();
        }

    }

    private void UnitWasHit(Unit unitHit)
    {
        if(unitHit != null)
        {
            selectionBehaviour.Select(unitHit); 
        }
        foreach(var unit in units)
        {
            if(unit != unitHit && unit.selected)
            {
                selectionBehaviour.Unselect(unit);
            }                  
        }
    }

    private void NoUnitsWereHit()
    {
        foreach(var unit in units)
        {
            if(unit.selected)
            {
                selectionBehaviour.Unselect(unit);
            }
        }
    }
}
