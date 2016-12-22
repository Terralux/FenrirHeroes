using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputHandler : MonoBehaviour {
    
	int moveCounts;

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
				MoveInDirection (TileDirections.UpLeft, moveCounts);
			}
			if (Input.GetKeyDown (KeyCode.W)) {
				MoveInDirection (TileDirections.UpRight, moveCounts);
			}
			if (Input.GetKeyDown (KeyCode.D)) {
				MoveInDirection (TileDirections.DownRight, moveCounts);
			}
			if (Input.GetKeyDown (KeyCode.S)) {
				MoveInDirection (TileDirections.DownLeft, moveCounts);
			}
				
			if (Toolbox.FindComponent<NetworkInputHandler> ().allPlayersReadyBool == true) {
				StartCoroutine (ExecuteActions ());
				Toolbox.FindComponent<NetworkInputHandler> ().allPlayersReadyBool = false;
			}
		}
	}
		

	public void MoveInDirection(TileDirections currentDirection, int moveLimit){
		Debug.Log ("I moved!");
		for (int i = 0 ; i < queuedActions.Count ; i++){
			if (queuedActions[i] as QueableSingleTargetMovement != null) {
				moveCounts++;
			}
		}

		if (moveCounts < moveLimit) {
			switch (currentDirection) {
			case TileDirections.UpRight:
				if (currentBTOTarget.North != null) {
					if (currentBTOTarget.North.currentGameState == TileGameplayState.PASSABLE) {
						queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.UpRight));
						currentBTOTarget = currentBTOTarget.North;
						break;
					} else {
						Debug.Log ("The tile you are trying to access is IMPASSABLE");
					}
				} else {
					Debug.Log ("There is no tile!");

				}
				break;

			case TileDirections.UpLeft:
				if (currentBTOTarget.West != null) {
					if (currentBTOTarget.West.currentGameState == TileGameplayState.PASSABLE) {
						queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.UpLeft));
						currentBTOTarget = currentBTOTarget.West;
						break;
					} else {
						Debug.Log ("The tile you are trying to access is IMPASSABLE");
					}
				} else {
					Debug.Log ("There is no tile!");

				}
				break;
			case TileDirections.DownRight:
				if (currentBTOTarget.East != null) {
					if (currentBTOTarget.East.currentGameState == TileGameplayState.PASSABLE) {
						queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.DownRight));
						currentBTOTarget = currentBTOTarget.East;
						break;
					} else {
						Debug.Log ("The tile you are trying to access is IMPASSABLE");
					}
				} else {
					Debug.Log ("There is no tile!");

				}
				break;
			case TileDirections.DownLeft:
				if (currentBTOTarget.South != null) {
					if (currentBTOTarget.South.currentGameState == TileGameplayState.PASSABLE) {
						queuedActions.Add (new QueableSingleTargetMovement (currentBTOTarget, TileDirections.DownLeft));
						currentBTOTarget = currentBTOTarget.South;
						break;
					} else {
						Debug.Log ("The tile you are trying to access is IMPASSABLE");
					}
				} else {
					Debug.Log ("There is no tile!");

				}
				break;
			}
		} else {
			Debug.Log ("Players movement count already used up!!");
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
			moveCounts = 0;
		} else {
			Debug.Log ("Finished Actions");
		}
	}

}