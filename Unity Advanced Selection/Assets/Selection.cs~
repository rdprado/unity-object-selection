using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {

    public Texture2D selectionHighlight;
    public static Rect selection = new Rect(0,0,0,0);
    private Vector3 startClick = -Vector3.one;

    private void CheckCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(selection.width == 0 && selection.height == 0)
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(startClick), out hitInfo);
                if (hit) 
                {
                    Unit selectedUnit = hitInfo.transform.gameObject.GetComponent<Unit>();
                    if(selectedUnit != null)
                    {
                        //selectedUnit.selected = true;
                        Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    }
                } 
                else {
                    Debug.Log("No hit");
                }
            }

            startClick = -Vector3.one;
            selection = new Rect(0,0,0,0);
        }

        if(Input.GetMouseButton(0))
        {
            selection = new Rect(startClick.x, ScreenToRectSpaceY(startClick.y),
                    Input.mousePosition.x - startClick.x,
                    ScreenToRectSpaceY(Input.mousePosition.y) - ScreenToRectSpaceY(startClick.y));
            
            if(selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }
            if(selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
        }
    }

    public static float ScreenToRectSpaceY(float y)
    {
        return Screen.height - y;
    }

    void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color (1,1,1,0.5f);
            GUI.DrawTexture(selection, selectionHighlight);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
    void Update()
    {
         

        CheckCamera();




    }
}


