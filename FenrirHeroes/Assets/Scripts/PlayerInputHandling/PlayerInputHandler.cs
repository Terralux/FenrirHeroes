using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputHandler : MonoBehaviour {

	private bool isActive = false;

	private List<QueableBaseAction> queuedActions = new List<QueableBaseAction> ();

	private BaseTileObject currentBTOTarget;

	void Awake(){
		Toolbox.FindComponent<GameStateHandler> ().EnableInputHandlers += TurnOn;
		Toolbox.FindComponent<GameStateHandler> ().DisableInputHandlers += TurnOff;


		currentBTOTarget = transform.parent.GetComponent<BaseTileObject> ();
	}

	void Update() {
		if (isActive) {
			if (Input.GetKeyDown (KeyCode.A)) {
				MoveInDirection (TileDirections.UpLeft);
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				MoveInDirection (TileDirections.UpRight);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				MoveInDirection (TileDirections.DownRight);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				MoveInDirection (TileDirections.DownLeft);
			}
				
			// Need to find another way to call this, cause it only works if i give it a trigger!
			if (Toolbox.FindComponent<NetworkInputHandler> ().allPlayersReadyBool == true) {
				StartCoroutine (ExecuteActions ());
				Toolbox.FindComponent<NetworkInputHandler> ().allPlayersReadyBool = false;
			}
		}
	}
		

	public void MoveInDirection(TileDirections currentDirection){
		Debug.Log ("I moved!");
		switch (currentDirection) {
		case TileDirections.UpRight:
			queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.UpRight));
			currentBTOTarget = currentBTOTarget.North;
			break;
		case TileDirections.UpLeft:
			queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.UpLeft));
			currentBTOTarget = currentBTOTarget.West;
			break;
		case TileDirections.DownRight:
			queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.DownRight));
			currentBTOTarget = currentBTOTarget.East;
			break;
		case TileDirections.DownLeft:
			queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.DownLeft));
			currentBTOTarget = currentBTOTarget.South;
			break;
		}
	}

	public void TurnOn(){
		isActive = true;
	}

	public void TurnOff(){
		isActive = false;
	}

	IEnumerator ExecuteActions () {
		yield return new WaitForSeconds (1f);
		if (queuedActions.Count > 0) {
			(queuedActions [0] as QueableSingleTargetAction).Action ();
			queuedActions.RemoveAt (0);
			StartCoroutine (ExecuteActions ());
		} else {
			Debug.Log ("Finished Actions");
		}
	}

}