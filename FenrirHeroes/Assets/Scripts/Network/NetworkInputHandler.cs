using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkInputHandler : Photon.PunBehaviour {

	public bool allPlayersReadyBool = false;

	private List<NetworkEntity> playerEntities = new List<NetworkEntity> ();
	private bool playerReady;

	void Awake() {
		Toolbox.RegisterComponent<NetworkInputHandler> (this);
	}

	void Start(){

	}

	public void SendDirection(int direction) {
		photonView.RPC ("ReceiveDirection", PhotonTargets.MasterClient, (TileDirections)direction, SystemInfo.deviceUniqueIdentifier);
	}

	public void SendReady(){
		playerReady = !playerReady;
		photonView.RPC ("ReceiveReady", PhotonTargets.MasterClient, (playerReady), SystemInfo.deviceUniqueIdentifier);
		playerReady = false;
	}

	[PunRPC]
	public void ReceiveDirection(TileDirections direction, string playerID) {
		foreach (NetworkEntity ne in playerEntities) {
			if (ne.deviceID == playerID) {
				ne.MoveInDirection (direction);
			}
		}
		Debug.Log (direction + "Was sent from: " + playerID);
	}

	[PunRPC]
	public void ReceiveReady(bool playerReady, string playerID) {
		 foreach (NetworkEntity ne in playerEntities) {
			if (ne.deviceID == playerID && ne.playerReady == false) {
				ne.playerReady = true;
			} else if (ne.deviceID == playerID && ne.playerReady == true) {
				ne.playerReady = false;
			}
		}  
		Debug.Log ("Ready is now: -> " + playerReady + " -> For the following ID: " + playerID);
		allPlayersReady ();
	}

	public void allPlayersReady(){
		// Check if all players are ready, if they are, run their moves + attacks

		foreach (NetworkEntity ne in playerEntities) {

			if (ne.playerReady == true) {
				Debug.Log ("All clients ready");
				allPlayersReadyBool = true;
				ne.playerReady = false;
			} else {
				Debug.Log ("Not all clients are ready yet");
				allPlayersReadyBool = false;
			}
	 }
	}


	public void AddEntity(NetworkEntity newEntity){
		playerEntities.Add (newEntity);
	}
		

}