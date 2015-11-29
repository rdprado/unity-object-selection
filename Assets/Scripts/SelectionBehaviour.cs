using UnityEngine;

public class SelectionBehaviour : MonoBehaviour
{
    private Color selectionColor = Color.green;

    public void Select(Unit unit)
    {
        unit.selected = true;
        unit.GetComponent<Renderer>().material.color = selectionColor;
    }

    public void Unselect(Unit unit)
    {
        unit.selected = false;
        unit.GetComponent<Renderer>().material.color = unit.materialColor;
    }
}
