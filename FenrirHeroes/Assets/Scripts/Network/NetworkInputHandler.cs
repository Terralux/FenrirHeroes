using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkInputHandler : Photon.PunBehaviour {

	private List<NetworkEntity> playerEntities = new List<NetworkEntity> ();

	void Awake() {
		Toolbox.RegisterComponent<NetworkInputHandler> (this);
	}

	public void SendDirection(int direction) {
		photonView.RPC ("ReceiveDirection", PhotonTargets.MasterClient, (TileDirections)direction, SystemInfo.deviceUniqueIdentifier);
	}

	[PunRPC]
	public void ReceiveDirection(TileDirections direction, string playerID) {
		foreach (NetworkEntity ne in playerEntities) {
			if (ne.deviceID == playerID) {
				ne.inputHandler.MoveInDirection (direction);
			}
		}
		Debug.Log (direction + "Was sent from: " + playerID);
	}

	public void AddEntity(NetworkEntity newEntity){
		playerEntities.Add (newEntity);
	}
}