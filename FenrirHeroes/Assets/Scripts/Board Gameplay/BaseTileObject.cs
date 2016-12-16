using UnityEngine;
using System.Collections;

public class BaseTileObject : MonoBehaviour {

	public BaseTileObject North;
	public BaseTileObject East;
	public BaseTileObject South;
	public BaseTileObject West;

	public TileGameplayState currentGameState = TileGameplayState.PASSABLE;

	public Player myPlayer;

	public void RemovePlayer(){
		myPlayer = null;
		currentGameState = TileGameplayState.PASSABLE;
	}

	public void MovePlayer(TileDirections direction){
		switch (direction) {
		case TileDirections.UpRight:
			North.SetPlayer (myPlayer);
			break;
		case TileDirections.UpLeft:
			West.SetPlayer (myPlayer);
			break;
		case TileDirections.DownRight:
			East.SetPlayer (myPlayer);
			break;
		case TileDirections.DownLeft:
			South.SetPlayer (myPlayer);
			break;
		}
	}

	public void SetPlayer(Player player){
		myPlayer = player;
		myPlayer.transform.SetParent (transform);
	}

}