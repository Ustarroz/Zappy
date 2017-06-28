using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public Map map;

    public void UpdateCell(string[] res)
    {
        if (res.Length == 10 && res[0] == "bct")
            map.UpdateCell(int.Parse(res[1]), int.Parse(res[2]), int.Parse(res[3]),
                int.Parse(res[4]), int.Parse(res[5]), int.Parse(res[6]), int.Parse(res[7]), int.Parse(res[8]),
                int.Parse(res[9]));
    }

    public void MapSizeUpdate(string[] res)
    {
        if (res.Length == 3 && res[0] == "msz")
        {
            map.dimension.x = int.Parse(res[1]);
            map.dimension.y = int.Parse(res[2]);
            map.CreateMap();
        }
    }

    public void NewPlayer(string[] res)
    {
        if (res.Length == 7 && res[0] == "pnw")
        {
            Vector3 pos = map.cells[int.Parse(res[2]), int.Parse(res[3])].transform.position;
            spawnManager.SpawnPlayer(pos, int.Parse(res[4]), int.Parse(res[1]), res[5]);
        }
    }

    public void UpdatePlayerLvl(string[] res)
    {
        if (res.Length == 3 && res[0] == "plv")
        {

        }
    }

    public void UpdatePlayerPos(string[] res)
    {
        if (res.Length == 3 && res[0] == "ppo")
        {

        }
    }

    public void PlayerExpulse(string[] res)
    {

    }

    public void StartIncantation(string[] res)
    {

    }

    public void EndIncantation(string[] res)
    {

    }

    public void LayEgg(string[] res)
    {

    }

    public void HatchingEgg(string[] res)
    {

    }

    public void EggDied(string[] res)
    {
        if (res[0] == "")
        {

        }
    }

    public void Put(string[] res)
    {

    }

    public void Take(string[] res)
    {

    }

    public void PlayerDied(string[] res)
    {

    }

    public void UpdateUnitTime(string[] res)
    {
        if (res.Length == 2 && res[0] == "sgt")
        {

        }
    }

    public void EndGame(string[] res)
    {
        if (res.Length == 2 && res[0] == "seg")
        {

        }
    }
}
