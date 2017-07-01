using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public NetworkAsync net;
    public InventoryUI invUI;

    private GameObject Click()
    {
        RaycastHit hit;
        Ray screenToWorldPos = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenToWorldPos, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Clickable")))
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Click();

            if (go != null)
            {
                ZappyObjects inv;

                if (go.tag == "Player")
                {
                    net.Send("pin " + go.GetComponent<Player>().id.ToString() + "\n");
                    inv = go.GetComponent<Player>().inventory;
                }
                else
                {
                    ZappyCell cell = go.GetComponent<ZappyCell>();
                    net.Send("bct " + cell.gridPos.x + " " + cell.gridPos.y + "\n");
                    inv = cell.inventory;
                }
                if (inv != null)
                {
                    invUI.go = go;
                    invUI.activeInventory = inv;
                }
            }
        }
    }
}
