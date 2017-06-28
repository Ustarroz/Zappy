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


        InterpretResponse("msz 10 5\n");
        InterpretResponse("pnw 0 0 2 4 0 toto\n");
        InterpretResponse("bct 0 0 1 2 3 4 5 6 7");
    }

    string[] Parse(string response)
    {
        return response.Split(' ');
    }

    public void InterpretResponse(string response)
    {
        string[] arg = Parse(response);

        if (commands.ContainsKey(arg[0]))
            commands[arg[0]](arg);
    }

}
