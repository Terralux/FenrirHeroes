using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QueableBaseAction : ScriptableObject {
	public string name = "Empty Action";
}

public abstract class QueableSingleTargetAction : QueableBaseAction {
	public abstract void Action ();
}

public abstract class QueableMultiTargetAction : QueableBaseAction {
	public abstract void Action (List<BaseTileObject> targetTiles);
}

public class QueableSingleTargetMovement : QueableSingleTargetAction {

	public TileDirections movementDirection;
	public BaseTileObject target;

	public QueableSingleTargetMovement (BaseTileObject target, TileDirections newDirection) {
		name = "Move";
		this.target = target;
		movementDirection = newDirection;
	}

	public override void Action () {
		target.myPlayer.Move (movementDirection);
	}
}

public class QueableSingleTargetDamage : QueableSingleTargetAction {
	
	public override void Action () {
		throw new System.NotImplementedException ();
	}
	
}