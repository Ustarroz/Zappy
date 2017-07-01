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

       /* InterpretResponse("sgt 10\n");
        InterpretResponse("msz 10 5\n");
        InterpretResponse("tna toto\n");
        InterpretResponse("tna billy\n");

        InterpretResponse("pnw 0 0 0 1 1 toto\n");
        InterpretResponse("ppo 0 1 0 1\n");
        InterpretResponse("ppo 0 1 1 1\n");
        InterpretResponse("ppo 0 0 1 1\n");
        InterpretResponse("ppo 0 0 0 1\n");


        /*   InterpretResponse("pnw 1 0 0 2 2 toto\n");
        InterpretResponse("pnw 2 0 0 3 3 billy\n");
        InterpretResponse("pnw 3 0 0 4 4 billy\n");

        InterpretResponse("ppo 0 0 0 2\n");
     /*   InterpretResponse("ppo 2 10 0 1\n");
        InterpretResponse("ppo 3 10 0 1\n");
        InterpretResponse("ppo 4 10 0 1\n");

       //        InterpretResponse("pie 0 1 1\n");

        /*
        InterpretResponse("pnw 1 0 1 1 2 toto\n");

         InterpretResponse("pnw 2 1 0 1 3 billy\n");
         InterpretResponse("pnw 3 1 3 1 8 billy\n");
         InterpretResponse("ebo 0\n");


         //  InterpretResponse("pdi 3\n");
         //  InterpretResponse("seg billy\n");


         //InterpretResponse("ppo 0 0 0 2\n");
         //  InterpretResponse("ppo 0 0 1 1\n");
         //InterpretResponse("ppo 0 0 2 1\n");


         /*InterpretResponse("ppo 0 0 4 1\n");
         InterpretResponse("ppo 0 10 4 1\n");
         InterpretResponse("ppo 0 0 4 1\n");
         InterpretResponse("ppo 0 0 5 1\n");

         //  InterpretResponse("ppo 0 0 0 4\n");
         // InterpretResponse("ppo 0 0 0 3\n");

         //InterpretResponse("bct 0 0 1 2 3 4 5 6 7");

         /*  InterpretResponse("ppo 0 0 0 2\n");
           InterpretResponse("ppo 0 1 0 1\n");
           InterpretResponse("pex 0\n"); */
        /*    InterpretResponse("pex 0\n");*/
        /*            InterpretResponse("pic 0 0 1 0\n");
                    InterpretResponse("pic 2 2 2 2\n");
                    InterpretResponse("pic 3 3 2 1\n");
        */

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
