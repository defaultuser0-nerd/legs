﻿using System;
using BepInEx;
using legs;
using UnityEngine;
using Utilla;
using Newtilla;

namespace legs
{
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	[ModdedGamemode]
	public class Plugin : BaseUnityPlugin
	{
		bool inRoom;

        private readonly string[] targetNames =
	{
            "shoulder.L",
            "shoulder.R"
        };

        private void legs()
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (MatchesTargetName(obj.name)) // makes it apply to everyone with the target names 
                {
                    obj.transform.localPosition = new Vector3(-0.0183f, 0.0759f, 0.0791f); // moves the shoulder to the 'leg spot'
                }
            }
        }
        private void arms()
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (MatchesTargetName(obj.name)) // makes it apply to everyone with the target names 
                {
                    obj.transform.localPosition = new Vector3(-0.0183f, 0.3432f, 0.0791f); // moves the shoulder back to the og spot
                }
            }
        }

        private bool MatchesTargetName(string objName)
        {
            foreach (string targetName in targetNames) // matches the target blah blah
            {
                if (objName.Equals(targetName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        void Start()
	{
            Newtilla.Newtilla.OnJoinModded += OnModdedJoin;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeave; 
            HarmonyPatches.ApplyHarmonyPatches();
	    GorillaTagger.OnPlayerSpawned(Initialized);
	}

	void Initialized()
	{
        }

	[ModdedGamemodeJoin]        
	public void OnModdedJoin(string gamemode)
	{
            legs(); // applies legs 
            Debug.Log("Enabled Legs"); // debug logs
            inRoom = true;
		}

	[ModdedGamemodeLeave]
	public void OnModdedLeave(string gamemode)
	{
            arms(); // applies arms 
            Debug.Log("Disabled Legs"); // debug logs
            inRoom = false;
	}
	}
}
