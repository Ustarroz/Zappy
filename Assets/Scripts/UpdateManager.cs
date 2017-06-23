using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour {

    public static Map map;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
    }

    static void UpdateCell(Vector3 vec, ZappyObjects values) {
        ZappyCell cell = map.cells[(int)vec.x, (int)vec.y];

        cell.inventory = values;
    }
}
