using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject prefabsUI;
    public static Dictionary<string, Text> inventoryPair;
    public readonly string[] names = { "Linemate", "Deraumere", "Sibur", "Mendiane", "Phiras", "Thystame", "Food" };
    public Sprite[] img;

    void Awake()
    {
        inventoryPair = new Dictionary<string, Text>();
        for (int i = 0; i < names.Length; i++)
        {
            GameObject go = Instantiate(prefabsUI, transform);
            go.name = names[i];
            go.transform.GetChild(0).GetComponent<Image>().sprite = img[i];
            go.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
            go.transform.GetChild(1).GetComponent<Text>().text = names[i];
            inventoryPair[names[i]] = go.transform.GetChild(2).GetComponent<Text>();
        }
    }

    public static void UpdateUI(string names, string value)
    {
        if (inventoryPair.ContainsKey(names))
            inventoryPair[names].text = value;
    }

    public static void UpdateUI(string names, int value)
    {
        if (inventoryPair.ContainsKey(names))
            inventoryPair[names].text = value.ToString();
    }
}
