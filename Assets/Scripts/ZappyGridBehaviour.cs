using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Grids2;

public class ZappyGridBehaviour : GridBehaviour<GridPoint3, MeshTileCell>
{
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnLeftClick(GridPoint3 clickedPoint)
    {
        print("left click");
    }

    public void OnRightClick(GridPoint3 clickedPoint)
    {
        print("right click");
    }

}
