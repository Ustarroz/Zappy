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
        commands["pbc"] = updateManager.Broadcast;

        /*InterpretResponse("sgt 10\n");
        InterpretResponse("sgt 1\n");
        InterpretResponse("msz 10 5\n");
        InterpretResponse("tna toto\n");
        InterpretResponse("tna billy\n");

        InterpretResponse("pnw 0 0 0 1 1 toto\n");

        InterpretResponse("pnw 1 0 0 2 2 toto\n");
        InterpretResponse("pnw 2 0 0 3 3 billy\n");
        InterpretResponse("pnw 3 0 0 4 4 billy\n");
        InterpretResponse("pbc 0 toto");
        InterpretResponse("seg toto");
*/        
    }

    public void InterpretResponse(string response)
    {
        string[] line = response.Split('\n');

        foreach (string l in line)
        {
            print(l);
            string[] arg = l.Split(' ');

            if (commands != null && commands.ContainsKey(arg[0]))
                commands[arg[0]](arg);
        }
    }

}
