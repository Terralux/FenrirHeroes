using UnityEngine;
using System.Collections;

public class GameStateHandler : MonoBehaviour {

	private GameStates currentState = GameStates.Intro;

	public delegate void SwitchPlayerInputHandlers ();
	public SwitchPlayerInputHandlers EnableInputHandlers;
	public SwitchPlayerInputHandlers DisableInputHandlers;

	// Use this for initialization
	void Awake () {
		Toolbox.RegisterComponent<GameStateHandler> (this);
		Toolbox.FindComponent<EventManager> ().OnLevelWasLoaded += StartPlayerTurn;
	}

	public void StartPlayerTurn() {
		currentState = GameStates.PlayerTurn;
		if (EnableInputHandlers != null) {
			EnableInputHandlers ();
		}
	}
	
	void Update () {
		switch (currentState) {
		case GameStates.Intro:
			//Build the scenery with code tile after tile
			break;
		case GameStates.GMBuildPhase:
			//wait for the GM to swap rooms, place traps amongst else
			break;
		case GameStates.GMTurn:
			//wait for the GM to queue all actions
			break;
		case GameStates.PlayerTurn:
			//Enable Player Input Handlers
			//Wait for the playerInputHandler to record all players ready
			break;
		case GameStates.PlayersLose:
			//Show a lose screen for the players
			break;
		case GameStates.PlayersWin:
			//Show a win screen for the players
			break;
		}
	}
}
