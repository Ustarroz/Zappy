using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZappyObjects {

    public int Food { get; set; }
    public int Linemate { get; set; }
    public int Deraumere { get; set; }
    public int Sibur { get; set; }
    public int Mendiane { get; set; }
    public int Phiras { get; set; }
    public int Thystame { get; set; }

    public ZappyObjects()
    {
        Food = Linemate = Deraumere = Sibur = Mendiane = Phiras = Thystame = 0;
    }

    public void UpdateValues(int food, int libenate, int deraumere, int sibur, int mendiane, int phiras, int thystame)
    {
        Food = food;
        Linemate = libenate;
        Deraumere = deraumere;
        Sibur = sibur;
        Mendiane = mendiane;
        Phiras = phiras;
        Thystame = thystame;
    }
}
