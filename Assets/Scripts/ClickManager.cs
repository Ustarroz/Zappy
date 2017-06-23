using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    ZappyObjects Click()
    {
        RaycastHit hit;
        Ray screenToWorldPos = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(screenToWorldPos, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Clickable")))
        {
            Debug.Log(hit.transform.name);
            //if (ZoneMap.HexMap.mapGrid.WorldToGrid(hit.transform.localPosition))
            //Debug.Log(ZoneMap.HexMap.mapGrid.WorldToGrid(hit.transform.localPosition).name);
            if (hit.transform.tag == "Player")
                return hit.transform.parent.gameObject.GetComponent<Player>().inventory;
            else
                return hit.transform.gameObject.GetComponent<ZappyCell>().inventory;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZappyObjects inv = Click();
        }
    }
}
