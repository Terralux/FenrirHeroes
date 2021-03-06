﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEntity : MonoBehaviour {

	public string deviceID;
	public int playerID;
	public bool playerReady = false;

	static int totalNetworkEntities = 0;

	private Player myPlayer;

	public PlayerInputHandler inputHandler;

	void Awake() {
		playerID = totalNetworkEntities;
		totalNetworkEntities++;
	}

	void Start() {
		deviceID = Toolbox.FindComponent<NetworkHandler> ().GetPlayerInfo(playerID);
		myPlayer = GetComponent<Player> ();
		Toolbox.FindComponent<NetworkInputHandler> ().AddEntity (this);
		inputHandler = GetComponent<PlayerInputHandler> ();
	}
		
	public void MoveInDirection(TileDirections direction){
		inputHandler.MoveInDirection (direction, myPlayer.getStats ().stats [4].getStat ());
	}

}