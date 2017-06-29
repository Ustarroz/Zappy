using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public Map map;

    public static float frequency = 100;
    public Incantation incantation;

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
            //   if (map.cells != null && map.cells.Length == 0)
            map.CreateMap();
        }
    }

    public void NewPlayer(string[] res)
    {
        if (res.Length == 7 && res[0] == "pnw")
        {
            spawnManager.SpawnPlayer(int.Parse(res[2]), int.Parse(res[3]), int.Parse(res[4]), int.Parse(res[1]), res[5]);
        }
    }

    public void UpdatePlayerLvl(string[] res)
    {
        if (res.Length == 3 && res[0] == "plv")
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            p.level = int.Parse(res[2]);
            p.Spawn(p.transform.position);
            incantation.StopIncantation(true);
        }
    }

    public void UpdatePlayerPos(string[] res)
    {
        if (res.Length == 5 && res[0] == "ppo")
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            Player.Orientation orientation = (Player.Orientation)int.Parse(res[4]);
            int x = int.Parse(res[2]);
            int y = int.Parse(res[3]);

            if (p.IsPositionDifferent(x, y))
                StartCoroutine(p.Move(x, y));
            else if (p.IsOrientationDifferent(orientation))
                StartCoroutine(p.Turn(orientation));
            p.nexGgridPos.x = x;
            p.nexGgridPos.y = y;
            p.nextOrientation = orientation;
        }
    }

    public void PlayerExpulse(string[] res)
    {
        if (res.Length == 2 && res[0] == "pex")
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            StartCoroutine(p.Expulse());
        }
    }

    public void StartIncantation(string[] res)
    {
        incantation.PlayIncantation();
        // start anim for incant
    }

    public void EndIncantation(string[] res)
    {
        incantation.StopIncantation(false);
        // stop anim for incant
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
        if (res.Length == 3)
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            p.inventory.UpdateRessource(int.Parse(res[2]), -1);
        }
    }

    public void Take(string[] res)
    {
        if (res.Length == 3)
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            p.inventory.UpdateRessource(int.Parse(res[2]), 1);
        }
    }

    public void PlayerDied(string[] res)
    {
        if (res.Length == 2)
        {
            Player player;
            int playerId = int.Parse(res[1]);
            player = spawnManager.FindPlayerById(playerId);
            Destroy(player);
        }
    }

    public void UpdateUnitTime(string[] res)
    {
        if (res.Length == 2 && res[0] == "sgt")
        {
            frequency = int.Parse(res[1]);

            foreach (Player p in spawnManager.players)
            {
                p.speed = 7 / frequency;
            }
        }
    }

    public void EndGame(string[] res)
    {
        if (res.Length == 2 && res[0] == "seg")
        {
            EndGameGui end = GetComponent<EndGameGui>();
        }
    }
}