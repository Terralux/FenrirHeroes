using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseTile : PlacableObject{
	public int TileGraphicID;
	public Structure myStructure;

	public BaseTile(int ID, int x, int y) : base (x, y) {
		TileGraphicID = ID;
	}

	public BaseTile(int ID, Structure s, int x, int y) : base (x, y) {
		TileGraphicID = ID;
		myStructure = s;
	}
}

[System.Serializable]
public class PlayerStartTile : BaseTile{
	
	public bool isAPlayerStartTile;

	public PlayerStartTile(int ID, Structure s, bool isPlayerStartPosition, int x, int y) : base (ID, s, x, y) {
		isAPlayerStartTile = isPlayerStartPosition;
	}
}

[System.Serializable]
public class ActionTile : PlayerStartTile{

	public List<TileAction> myTilesActions = new List<TileAction>();

	public ActionTile(int ID, Structure s, bool isNewPlayerStartPosition, int x, int y) : base (ID, s, isNewPlayerStartPosition, x, y) {
		
	}
}

[System.Serializable]
public class TileAction{
	//Base class used for listing all the possible outcomes
}

[System.Serializable]
public class MultipleUseTileAction : TileAction{
	//Used to determine activation based on int
	public int activationTimes = 1;
	public bool activatesUponCountReached = false;

	/// <summary>
	/// Initializes a new instance of the <see cref="MultipleUseTileAction"/> class.
	/// </summary>
	/// <param name="activationSteps">Activation steps, if less than 0, will be active forever</param>
	/// <param name="willTriggerUponCountReached">will trigger multiple times if true will activate once on count reached otherwise</param>
	public MultipleUseTileAction(int activationSteps, bool willTriggerUponCountReached){
		if (activationSteps < 1) {
			activationTimes = 100000;
		} else {
			activationTimes = activationSteps;
		}

		activatesUponCountReached = willTriggerUponCountReached;
	}

	public MultipleUseTileAction(){
		
	}
}

[System.Serializable]
public class DeleteObjectTileAction : TileAction{
	//Used to delete the structure upon activation
	public PlacableObject objectToDelete;

	public DeleteObjectTileAction(PlacableObject target){
		objectToDelete = target;
	}

	public DeleteObjectTileAction(){
		
	}
}

[System.Serializable]
public class SwitchStateTileAction : TileAction{
	public PlacableObject objectToChangeState;

	public SwitchStateTileAction(PlacableObject target){
		objectToChangeState = target;
	}

	public SwitchStateTileAction(){
		
	}
}

[System.Serializable]
public class SpawnObjectTileAction : TileAction{
	//Used to spawn object upon activation
	public TileDirections spawnDirection = TileDirections.UpRight;
	//Needs the object to be instantiated
	public SpawnObjectTileAction(){
		
	}
}

[System.Serializable]
public class StatsTileAction : TileAction{
	public PlacableObject targetCreature;
	public int targetStat;

	public StatsTileAction(PlacableObject target, int statIndex){
		targetCreature = target;
		targetStat = statIndex;
	}

	public StatsTileAction(){
		
	}
}

[System.Serializable]
public class MoveCharacterTileAction : TileAction{
	public PlacableObject creaturePosition;
	public int targetPositionX;
	public int targetPositionY;

	public MoveCharacterTileAction(PlacableObject target, int Xdestination, int Ydestination){
		creaturePosition = target;
		targetPositionX = Xdestination;
		targetPositionY = Ydestination;
	}

	public MoveCharacterTileAction(){
		
	}
}

[System.Serializable]
public class EventTile : PlayerStartTile{

	public delegate void voidEvent();
	public voidEvent myEvent;

	public int interactionsBeforeActivation = 1;

	public EventTile(int ID, Structure s, bool isNewPlayerStartPosition, int x, int y) : base (ID, s, isNewPlayerStartPosition, x, y) {
		
	}
}