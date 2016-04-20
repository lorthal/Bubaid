using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    public Grid grid;

    private Building b;

    public float distance;

	void Start ()
    {
	    
	}
	
	void FixedUpdate ()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));

        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            b = hitInfo.collider.gameObject.GetComponent<Building>();
            grid.selectedX = Mathf.RoundToInt(hitInfo.point.x);
            grid.selectedZ = Mathf.RoundToInt(hitInfo.point.z);
            //grid.selectedY = Mathf.RoundToInt(hitInfo.point.y);
            

            //if (b != null)
            //{
            //    Debug.Log("b position: "+ b.transform.position + ", Pointer position: " + grid.GetPointer().transform.position + ", hit Point: " + hitInfo.point);
            //    if (hitInfo.point.y > grid.tileSize / 2)
            //        grid.selectedY = Mathf.RoundToInt(b.transform.position.y + grid.tileSize);
            //    if (hitInfo.point.y < grid.tileSize / 2)
            //        grid.selectedY = Mathf.RoundToInt(b.transform.position.y);
            //    if (hitInfo.point.x - b.transform.position.x > 0)
            //        grid.selectedX = Mathf.RoundToInt(b.transform.position.x + grid.tileSize);
            //    if (hitInfo.point.x - b.transform.position.x < 0)
            //        grid.selectedX = Mathf.RoundToInt(b.transform.position.x - grid.tileSize);
            //}
        }
        bool build = grid.CanBuild();
        if(build)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject temp = Instantiate(grid.pointers[grid.GetSelectedBlock()]) as GameObject;
                //temp.transform.position = new Vector3(grid.selectedX, grid.selectedY, grid.selectedZ);
                temp.transform.position = new Vector3(grid.selectedX, 0, grid.selectedZ);
                temp.transform.localScale = Vector3.one * grid.tileSize;
                temp.AddComponent<Building>();
                //grid.board[grid.selectedX / (int)grid.tileSize, grid.selectedZ / (int)grid.tileSize, grid.selectedY / (int)grid.tileSize] = temp;
                grid.board[grid.selectedX, grid.selectedZ] = temp;
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (b != null)
            {
                Destroy(b.gameObject);
            }
        }
    }
}
