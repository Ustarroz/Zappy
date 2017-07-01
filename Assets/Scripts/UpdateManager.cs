using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public Map map;

    public static float frequency = 100;
<<<<<<< HEAD
    public Incantation incantation;
    public GameObject endUI;
    public GameObject inventoryUI;
=======
    public GameObject   incantationPrefab;
    public GameObject   eggPrefab;
    public List<Incantation> incantationList;
    public List<Egg> eggList;
>>>>>>> origin/rework_incantation

    public void UpdateCell(string[] res)
    {
        print("bct : " + res.Length + " - " + res[0]);
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
            if (map.cells == null)
                map.CreateMap();
        }
    }

    public void NewPlayer(string[] res)
    {
        if (res.Length == 7 && res[0] == "pnw")
        {
            spawnManager.SpawnPlayer(int.Parse(res[1]), int.Parse(res[2]), int.Parse(res[3]), int.Parse(res[4]), int.Parse(res[5]), res[6]);
        }
    }

    public void UpdatePlayerLvl(string[] res)
    {
        if (res.Length == 3 && res[0] == "plv")
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
<<<<<<< HEAD
            p.LevelUp(int.Parse(res[2]));
            incantation.StopIncantation(true);
=======
            p.level = int.Parse(res[2]);
            p.Spawn(p.transform.position);
>>>>>>> origin/rework_incantation
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
        int x;
        int y;
        Vector2 pos;

        if (res.Length < 3)
            return;
        x = int.Parse(res[1]);
        y = int.Parse(res[2]);
        pos = new Vector2(x, y);
        GameObject go = Instantiate(incantationPrefab);
        Incantation newIncant = go.GetComponent<Incantation>();
        newIncant.setPosition(pos);
        incantationList.Add(newIncant);
        incantationList.Find(incant => incant.getPosition() == pos).PlayIncantation();
    }

    public void EndIncantation(string[] res)
    {
        int x;
        int y;
        int success;
        Vector2 pos;

        if (res.Length < 4)
            return;
        x = int.Parse(res[1]);
        y = int.Parse(res[2]);
        success = int.Parse(res[3]);
        pos = new Vector2(x, y);
        Incantation incantation = incantationList.Find(incant => incant.getPosition() == pos);
        if (incantation != null)
        {
            incantation.StopIncantation(success == 1);
            incantationList.Remove(incantation);
        }
    }

    public void LayEgg(string[] res)
    {
<<<<<<< HEAD
        if (res.Length == 5)
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[2]));
            p.egg.id = int.Parse(res[1]);
            p.egg.Show();
            spawnManager.SpawnEgg(p.egg);  
        }
=======
        int x;
        int y;
        int id;
        Vector2 pos;

        if (res.Length != 5)
            return;
        x = int.Parse(res[3]);
        y = int.Parse(res[4]);
        id = int.Parse(res[1]);
        pos = new Vector2(x, y);
        GameObject go = Instantiate(eggPrefab);
        Egg newEgg = go.GetComponent<Egg>();
        newEgg.setPosition(pos);
        newEgg.setId(id);
        eggList.Add(newEgg);
        eggList.Find(egg => egg.getPosition() == pos).Pop();
>>>>>>> origin/rework_incantation
    }

    public void HatchingEgg(string[] res)
    {
<<<<<<< HEAD
        if (res.Length == 2)
        {
            Egg egg = spawnManager.FindEggById(int.Parse(res[1]));

        }
    }

    public void ConnectPlayerForEgg(string[] res)
    {
        if (res.Length == 2)
        {
            Egg egg = spawnManager.FindEggById(int.Parse(res[1]));

            spawnManager.eggs.Remove(egg);
            egg.Hide();
=======
        int id;
        Vector2 pos;

        if (res.Length != 2)
            return;
        id = int.Parse(res[1]);
        Egg egg = eggList.Find(e => e.getId() == id);
        if (egg != null)
        {
            egg.Crack();
            eggList.Remove(egg);
>>>>>>> origin/rework_incantation
        }
    }

    public void EggDied(string[] res)
    {
        if (res.Length == 2)
        {
            Egg egg = spawnManager.FindEggById(int.Parse(res[1]));

            spawnManager.eggs.Remove(egg);
            egg.Hide();
        }
    }

    public void Put(string[] res)
    {
        if (res.Length == 3)
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            p.inventory.UpdateRessource(int.Parse(res[2]), -1);
            p.Put();
        }
    }

    public void Take(string[] res)
    {
        if (res.Length == 3)
        {
            Player p = spawnManager.FindPlayerById(int.Parse(res[1]));
            p.inventory.UpdateRessource(int.Parse(res[2]), 1);
            p.Take();
        }
    }

    public void PlayerDied(string[] res)
    {
        if (res.Length == 2)
        {
            int playerId = int.Parse(res[1]);
            Player player = spawnManager.FindPlayerById(playerId);
            if (player != null)
            {
                spawnManager.players.Remove(player);
                Destroy(player.gameObject);
            }
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
                p.UpdateAnimSpeed();
            }
        }
    }

    public void EndGame(string[] res)
    {
        if (res.Length == 2 && res[0] == "seg")
        {
            print("game over");
            inventoryUI.SetActive(false);
            endUI.SetActive(true);
            endUI.transform.GetChild(2).GetComponent<Text>().text = res[1].ToUpper();
           // GetComponent<NetworkAsync>().Disconnect();
        }
    }

    public void TeamName(string[] res)
    {
        if (res.Length == 2)
        {
            spawnManager.AddTeam(res[1]);
        }
    }
}