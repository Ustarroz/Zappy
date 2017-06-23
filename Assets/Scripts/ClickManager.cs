using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{

    //private InventoryUI inventoryUI;

    private void Start()
    {
    //    inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<InventoryUI>();
    }

    private ZappyObjects Click()
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ZappyObjects inv = Click();

            InventoryUI.UpdateUI("Food", inv.Food);
            InventoryUI.UpdateUI("Deraumere", inv.Deraumere);
            InventoryUI.UpdateUI("Linebate", inv.Linemate);
            InventoryUI.UpdateUI("Mendiane", inv.Mendiane);
            InventoryUI.UpdateUI("Phiras", inv.Phiras);
            InventoryUI.UpdateUI("Sibur", inv.Sibur);
            InventoryUI.UpdateUI("Thystame", inv.Thystame);
        }
    }
}
