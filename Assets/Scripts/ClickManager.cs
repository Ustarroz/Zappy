using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
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
                    inv = go.GetComponent<Player>().inventory;
                else
                    inv = go.GetComponent<ZappyCell>().inventory;
                if (inv != null)
                {
                    invUI.UpdateUI(go.name, "Food", inv.Food);
                    invUI.UpdateUI(go.name, "Deraumere", inv.Deraumere);
                    invUI.UpdateUI(go.name, "Linemate", inv.Linemate);
                    invUI.UpdateUI(go.name, "Mendiane", inv.Mendiane);
                    invUI.UpdateUI(go.name, "Phiras", inv.Phiras);
                    invUI.UpdateUI(go.name, "Sibur", inv.Sibur);
                    invUI.UpdateUI(go.name, "Thystame", inv.Thystame);
                }
            }
        }
    }
}
