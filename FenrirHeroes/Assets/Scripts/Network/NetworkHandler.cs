using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class NetworkHandler : Photon.PunBehaviour {
	
	public List<string> playerDeviceIDs = new List<string>();

	void Awake () {
		Toolbox.RegisterComponent<NetworkHandler> (this);
	}

	void Start () {
		// Simply used for connecting to MasterServer, String given is a "game-version"-check.
		PhotonNetwork.ConnectUsingSettings("0.1");

		Toolbox.FindComponent<MenuHandler> ().BeginSession += BeginSession;
	}

	public string GetPlayerInfo(int index) {
		return playerDeviceIDs[index];
	}

	public void AddNewPlayer(string deviceID) {
		//Add a new player to the list of active player devices
		playerDeviceIDs.Add(deviceID);
	}

	[PunRPC]
	void InitializePlayer(string deviceInfo) {
		// RPC call to make all joining players send their deviceInfo to the Master Client, so he can add them to player list

		if (PhotonNetwork.isMasterClient) {
			if (deviceInfo == SystemInfo.deviceUniqueIdentifier) {
				//This was the masterclients own id, so we stop progress here
				return;
			}

			if (playerDeviceIDs.Contains (deviceInfo) == false) {
				AddNewPlayer (deviceInfo);
				Debug.Log ("New PlayerID was set");
			} else {
				Debug.Log ("Dublicate UniqueIdentifiers Found");
				Debug.Log ("Should assign this player to his already given Player[x] ID");
			}
		}
	}

	[PunRPC]
	void PlayersJoined() {
		//Function will be called when you join a room, this will send your deviceInfo to Master Client, who will pass on to NetWorkManager.
		photonView.RPC ("InitializePlayer", PhotonTargets.MasterClient, SystemInfo.deviceUniqueIdentifier);
	}

	/// <summary>
	/// Raises the joined room event. - Whenever a room is joined, this function runs.
	/// </summary>
	public override void OnJoinedRoom() {
		//Debug Logs//
		Debug.Log ("Joined a room! Room Name: " + PhotonNetwork.room.name);
		Debug.Log ("There are currently " + PhotonNetwork.room.playerCount + " Players in this room ");
		//Debug Logs//
		
		// Make RPC call - Send your device info to Master Client, Master Client will then send it to NetWorkHandler
		PlayersJoined();
	}

	public void JoinRandomRoom() {
		PhotonNetwork.JoinRandomRoom ();
		PlayersJoined ();
	}

	public void BeginSession(){
		//Creating a room, if null is passed, a random name will be given to the room. Possible to set own room name later.
		PhotonNetwork.CreateRoom(null);
	}

	/// <summary>
	/// Raises the photon join room failed event. Only for when a room could be created.
	/// </summary>
	void OnPhotonJoinRoomFailed() {
		Debug.Log ("No room could be created!");
	}

	/// <summary>
	/// Raises the photon random join failed event. Only for when no rooms could be joined.
	/// </summary>
	void OnPhotonRandomJoinFailed() {
		Debug.Log("Can't join a random room!");
		Debug.Log ("Need to create a new room!");
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), PhotonNetwork.connectionStateDetailed.ToString ());
	}

}