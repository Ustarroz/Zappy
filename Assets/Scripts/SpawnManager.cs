using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public Color[] teamColor;
    public List<Player> players;

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

    public void SpawnPlayer(Vector3 pos, int orientation, int id, string team)
    {

        GameObject go = Instantiate(playerPrefab);
        Player player = go.GetComponent<Player>();

        player.team = team;
        player.id = id;
        player.Spawn(pos);
        players.Add(player);
        switch (orientation)
        {
            case 1:
                //north
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
            case 2:
                //east
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0)); 
                break;
            case 3:
                //south
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case 4:
                //west
                player.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                break;
        };
        //change color material for team;
    }
}
