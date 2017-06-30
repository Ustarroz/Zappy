﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Player> players;
    public Map map;
    public Material[] body;
    public Material[] arms;

    private Dictionary<string, int> teamMaterial;
    private int teamIndex = 0;

    public void SpawnPlayer(int id, int x, int y, int orientation, int level, string team)
    {
        Vector3 pos = map.cells[x, y].transform.position;
        GameObject go = Instantiate(playerPrefab);
        Player player = go.GetComponent<Player>();

        player.gridPos = new Vector2(x, y);
        player.transform.position = map.cells[x, y].transform.position;
        player.team = team;
        player.id = id;
        player.level = level;
        players.Add(player);
        player.orientation = (Player.Orientation)orientation;
        player.transform.eulerAngles = ConvertOrientation((Player.Orientation)orientation);
        player.LevelUp(level);
        if (teamMaterial.ContainsKey(team) && teamMaterial[team] < body.Length)
        {
            SkinnedMeshRenderer skmr = player.transform.GetChild(1).GetChild(1).GetComponent<SkinnedMeshRenderer>();
            Material[] mats = skmr.materials;
            mats[0] = body[teamMaterial[team]];
            mats[4] = arms[teamMaterial[team]];
            skmr.materials = mats;
        }
    }

    public void AddTeam(string teamName)
    {
        if (teamMaterial == null)
            teamMaterial = new Dictionary<string, int>();
        if (!teamMaterial.ContainsKey(teamName))
        {
            teamMaterial[teamName] = teamIndex;
            ++teamIndex;
        }
    }

    public Player FindPlayerById(int id)
    {
        return players.Find((x) => x.id == id);
    }

    public Vector3 ConvertOrientation(Player.Orientation orientation)
    {
        switch (orientation)
        {
            case Player.Orientation.NORTH:
                return new Vector3(0, 0, 0);
            case Player.Orientation.EAST:
                return new Vector3(0, 90, 0);
            case Player.Orientation.SOUTH:
                return new Vector3(0, 180, 0);
            case Player.Orientation.WEST:
                return new Vector3(0, 270, 0);
        };
        return Vector3.zero;
    }
}
