using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    public GameObject egg;
    public int id;

  public void Show()
    {
        egg.SetActive(true);
    }

    public void Hide()
    {
        egg.SetActive(false);
    }
}
