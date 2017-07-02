using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InterpretServerResponse : MonoBehaviour
{
    public UpdateManager updateManager;
    private Dictionary<string, Action<string[]>> commands;

    private void Awake()
    {
        commands = new Dictionary<string, Action<string[]>>();

        commands["bct"] = updateManager.UpdateCell;
        commands["msz"] = updateManager.MapSizeUpdate;
        commands["pnw"] = updateManager.NewPlayer;
        commands["plv"] = updateManager.UpdatePlayerLvl;
        commands["pin"] = updateManager.UpdatePlayerInv;

        commands["ppo"] = updateManager.UpdatePlayerPos;
        commands["pex"] = updateManager.PlayerExpulse;
        commands["pic"] = updateManager.StartIncantation;
        commands["pie"] = updateManager.EndIncantation;
        commands["enw"] = updateManager.LayEgg;
        commands["eht"] = updateManager.HatchingEgg;
        commands["edi"] = updateManager.EggDied;
        commands["pdr"] = updateManager.Put;
        commands["pgt"] = updateManager.Take;
        commands["sgt"] = updateManager.UpdateUnitTime;
        commands["seg"] = updateManager.EndGame;
        commands["tna"] = updateManager.TeamName;
        commands["pdi"] = updateManager.PlayerDied;
        commands["ebo"] = updateManager.ConnectPlayerForEgg;

        InterpretResponse("sgt 10\n");
        InterpretResponse("msz 10 5\n");
        InterpretResponse("tna toto\n");
        InterpretResponse("tna billy\n");

      //  InterpretResponse("pnw 0 0 0 1 1 toto\n");
        /*   InterpretResponse("ppo 0 0 4 1\n");
           InterpretResponse("ppo 0 0 0 1\n");
           InterpretResponse("ppo 0 9 0 1\n");
           InterpretResponse("ppo 0 0 0 1\n");*/

    

        InterpretResponse("pnw 1 0 0 1 2 toto\n");
        InterpretResponse("pnw 2 0 0 1 3 billy\n");
        InterpretResponse("pnw 3 0 0 1 4 billy\n");


        InterpretResponse("ppo 1 1 0 1\n");
        InterpretResponse("ppo 2 1 0 1\n");
        InterpretResponse("ppo 3 1 0 1\n");

        InterpretResponse("ppo 1 1 1 1\n");
        InterpretResponse("ppo 2 1 1 1\n");
        InterpretResponse("ppo 3 1 1 1\n");

        InterpretResponse("ppo 1 0 1 1\n");
        InterpretResponse("ppo 2 0 1 1\n");
        InterpretResponse("ppo 3 0 1 1\n");

        InterpretResponse("ppo 1 0 0 1\n");
        InterpretResponse("ppo 2 0 0 1\n");
        InterpretResponse("ppo 3 0 0 1\n");

        InterpretResponse("ppo 1 0 4 1\n");
        InterpretResponse("ppo 2 0 4 1\n");
        InterpretResponse("ppo 3 0 4 1\n");

        InterpretResponse("ppo 1 0 0 1\n");
        InterpretResponse("ppo 2 0 0 1\n");
        InterpretResponse("ppo 3 0 0 1\n");

        InterpretResponse("ppo 1 9 0 1\n");
        InterpretResponse("ppo 2 9 0 1\n");
        InterpretResponse("ppo 3 9 0 1\n");

        InterpretResponse("ppo 1 8 0 1\n");
        InterpretResponse("ppo 2 8 0 1\n");
        InterpretResponse("ppo 3 8 0 1\n");

        InterpretResponse("ppo 1 8 4 1\n");
        InterpretResponse("ppo 2 8 4 1\n");
        InterpretResponse("ppo 3 8 4 1\n");

        InterpretResponse("ppo 1 9 4 1\n");

        InterpretResponse("ppo 1 9 3 1\n");


        InterpretResponse("ppo 1 0 3 1\n");
        
    }


    public void InterpretResponse(string response)
    {
        string[] line = response.Split('\n');

        foreach (string l in line)
        {
            string[] arg = l.Split(' ');

            if (commands != null && commands.ContainsKey(arg[0]))
                commands[arg[0]](arg);
        }
    }

}
