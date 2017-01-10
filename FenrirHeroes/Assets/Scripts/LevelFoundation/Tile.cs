using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseTile : PlacableObject{

	public int TileGraphicID;
	public ObjectTypes myType = ObjectTypes.TILE;
	public bool isPlayerStartTile = false;
	public bool isEventTile = false;
	public GraphicsObject myGraphics;

	public List<TileAction> myTilesActions = new List<TileAction>();

	public EventTrigger myEventTrigger = EventTrigger.ON_ENTER_TILE;
	public EventTypes myEventType = EventTypes.UNLIMITED_USES;

	public int triggerSteps = -1;

	public BaseTile(int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
	}

	public BaseTile(ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
	}

	public BaseTile(bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
	}

	public BaseTile(GraphicsObject neoGraphics, bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
		myGraphics = neoGraphics;
	}

	public BaseTile(EventTrigger trigger, GraphicsObject neoGraphics, bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
		myGraphics = neoGraphics;
		myEventTrigger = trigger;
	}

	public BaseTile(EventTypes eventType, EventTrigger trigger, GraphicsObject neoGraphics, bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
		myGraphics = neoGraphics;
		myEventTrigger = trigger;
		myEventType = eventType;
	}

	public BaseTile(int triggerSteps, EventTypes eventType, EventTrigger trigger, GraphicsObject neoGraphics, bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
		myGraphics = neoGraphics;
		myEventTrigger = trigger;
		myEventType = eventType;
		this.triggerSteps = triggerSteps;
	}

	public BaseTile(List<TileAction> actions, int triggerSteps, EventTypes eventType, EventTrigger trigger, GraphicsObject neoGraphics, bool isPlayerSpawnTile, ObjectTypes objectType, int ID, TileDirections direction, int x, int y) : base (direction, x, y) {
		TileGraphicID = ID;
		myType = objectType;
		isPlayerStartTile = isPlayerSpawnTile;
		myGraphics = neoGraphics;
		myEventTrigger = trigger;
		myEventType = eventType;
		this.triggerSteps = triggerSteps;
		myTilesActions = actions;
	}

	public bool GetIsTilePassable() {
		if (myType == ObjectTypes.TILE || myType == ObjectTypes.PROP || myType == ObjectTypes.COVER) {
			return true;
		}
		return false;
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