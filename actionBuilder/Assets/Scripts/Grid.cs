using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public int sizeX = 100;
    public int sizeZ = 100;
    public int sizeY = 2;

    public float tileSize = 1.0f;

    public Material pointerMaterial;
    public Material pointerCanNotMaterial;

    //public GameObject[,,] board;
    public GameObject[,] board;
    public GameObject[] pointers;
    private GameObject pointer;
    private int selectedBlock = 0;
    public int selectedX;
    public int selectedZ;
    //public int selectedY;

    void Awake ()
    {
        //board = new GameObject[sizeX,sizeZ,sizeY]; 
        board = new GameObject[sizeX, sizeZ];
        SelectPointer();
    }
	
	void Update ()
    {
        //pointer.transform.position = new Vector3(selectedX, selectedY, selectedZ);
        pointer.transform.position = new Vector3(selectedX, 0, selectedZ);
        changeBlock();
        RotateBlock();
   	}

    void changeBlock()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedBlock < pointers.Length - 1)
                selectedBlock++;
            else selectedBlock = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedBlock > 0)
                selectedBlock--;
            else selectedBlock = pointers.Length - 1;
        }
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            
            SelectPointer();
        }
    }
    void RotateBlock()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            pointer.transform.Rotate(0, 0, 90.0f);
            pointers[selectedBlock].transform.Rotate(0, 0, 90.0f);
        }
    }

    public GameObject GetPointer()
    {
        return pointer;
    }

    public int GetSelectedBlock()
    {
        return selectedBlock;
    }

    void SelectPointer()
    {
        Destroy(pointer);
        pointer = Instantiate(pointers[selectedBlock]) as GameObject;
        pointer.transform.localScale = Vector3.one * tileSize;
        Renderer rend = pointer.GetComponent<Renderer>();
        rend.sharedMaterial = pointerMaterial;
        MeshCollider collider = pointer.GetComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
        pointer.layer = 2;
        pointer.AddComponent<Pointer>();
    }

    public bool CanBuild()
    {
        bool build = true;
        foreach (GameObject o in board)
        {

            if (o != null && GetPointer().GetComponent<Collider>().bounds.Contains(o.transform.position))
            {
                build = false;
            }
        }
        Renderer rend = pointer.GetComponent<Renderer>();
        if (!build)
        {
            rend.sharedMaterial = pointerCanNotMaterial;
        }
        else if(rend.sharedMaterial == pointerCanNotMaterial)
        {
            rend.sharedMaterial = pointerMaterial;
        }
        return build;
    }
}
