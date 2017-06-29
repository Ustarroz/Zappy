using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Color[] teamColor;
    public List<Player> players;
    public Map map;

    int nbTeams;

    void Start()
    {
        //TODO get nbTeams from server
        InitTeamColor();
    }

    void InitTeamColor()
    {
        teamColor = new Color[nbTeams];
        for (int i = 0; i < nbTeams; i++)
        {
            teamColor[i] = Random.ColorHSV(0f, 1f, 0f, 0.7f, 1f, 1f);
        }
    }

    public void SpawnPlayer(int x, int y, int orientation, int id, string team)
    {
        Vector3 pos = map.cells[x, y].transform.position;
        GameObject go = Instantiate(playerPrefab);
        Player player = go.GetComponent<Player>();

        player.gridPos = new Vector2(x, y);
        player.team = team;
        player.id = id;
        player.Spawn(pos);
        players.Add(player);
        player.orientation = (Player.Orientation)orientation;
        player.transform.eulerAngles = ConvertOrientation((Player.Orientation)orientation);
        //change color material for team;
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
