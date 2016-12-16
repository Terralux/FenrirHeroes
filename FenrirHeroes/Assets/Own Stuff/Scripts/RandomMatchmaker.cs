using UnityEngine;
using System.Collections;
using Photon;

public class RandomMatchmaker : Photon.PunBehaviour {

	string deviceInfo;

	// Use this for before initialization
	void Awake () {

		Toolbox.RegisterComponent<RandomMatchmaker> (this);
		deviceInfo = SystemInfo.deviceUniqueIdentifier;

	}

	// Use this for initialization
	void Start () {

		// Simply used for connecting to MasterServer, String given is a "game-version"-check.
		PhotonNetwork.ConnectUsingSettings("0.1");

		Toolbox.FindComponent<MenuHandler> ().BeginSession += beginSession;

	}
	/// <summary>
	/// Function for when we are joining the lobby, to find a list of active rooms/games.
	/// </summary>
	/// <remarks>Note: When PhotonNetwork.autoJoinLobby is false, OnConnectedToMaster() will be called and the room list won't
	/// become available.
	/// 
	/// While in the lobby, the roomlist is automatically updated in fixed intervals (which you can't modify).
	/// The room list gets available when OnReceivedRoomListUpdate() gets called after OnJoinedLobby().</remarks>

	/* public override void OnJoinedLobby()
	{
		//Joins any random room that is active atm.
		PhotonNetwork.JoinRandomRoom();

	} */

	/// <summary>
	/// Raises the joined room event. - Whenever a room is joined, this function runs.
	/// </summary>
	public override void OnJoinedRoom()
	{

		//Debug Logs//
		Debug.Log ("Joined a room! Room Name: " + PhotonNetwork.room.name);
		Debug.Log ("There are currently " + PhotonNetwork.room.playerCount + " Players in this room ");
		//Debug Logs//
		
		// Make RPC call - Send your device info to Master Client, Master Client will then send it to NetWorkHandler
		PlayersJoined();

	}

	[PunRPC]
	void InitializePlayer(string deviceInfo){
        // RPC call to make all joining players send their deviceInfo to the Master Client, so he can add them to player list

        if (PhotonNetwork.isMasterClient && Toolbox.FindRequiredComponent<NetworkHandler>().playerID.Contains(deviceInfo) == false)
        {
            Toolbox.FindRequiredComponent<NetworkHandler>().givePlayerID(deviceInfo);
            Debug.Log("New PlayerID was set");
        }
        else if (PhotonNetwork.isMasterClient && Toolbox.FindRequiredComponent<NetworkHandler>().playerID.Contains(deviceInfo) == true)
        {
            Debug.Log("Dublicate UniqueIdentifiers Found");
            Debug.Log("Should assign this player to his already given Player[x] ID");
        }
        else
        {
            Debug.Log("Is not MasterClient");
        }
       

 
	}

	[PunRPC]
	void PlayersJoined(){
	    //Function will be called when you join a room, this will send your deviceInfo to Master Client, who will pass on to NetWorkManager.

	    photonView.RPC ("InitializePlayer", PhotonTargets.MasterClient, deviceInfo);
	    
	}


	public void sendDirection(TileDirections direction){
	
		photonView.RPC ("receiveDirection", PhotonTargets.MasterClient, direction, deviceInfo);

	}

	[PunRPC]
	public void receiveDirection(TileDirections direction, string playerID){
		Debug.Log (direction + "Was sent from: " + playerID);	
	}

		

	/// <summary>
	/// Raises the photon random join failed event. Only for when no rooms could be joined.
	/// </summary>
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		Debug.Log ("Creating a new room!");

	}

	public void beginSession(){

		//Creating a room, if null is passed, a random name will be given to the room. Possible to set own room name later.
		PhotonNetwork.CreateRoom(null);

	}

	/// <summary>
	/// Raises the photon join room failed event. Only for when a room could be created.
	/// </summary>
	void OnPhotonJoinRoomFailed()
	{
		Debug.Log ("No room could be created!");
	}



	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}



}
