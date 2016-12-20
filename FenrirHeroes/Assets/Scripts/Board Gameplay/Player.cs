using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Stats myStats;

	public void Move(TileDirections direction){
		BaseTileObject bto = transform.parent.GetComponent<BaseTileObject> ();
		bto.MovePlayer (direction);
		bto.RemovePlayer ();

		transform.localPosition = Vector3.zero;
		//Debug.Log ("My move stats is: " + myStats.move);
	}

	public void AdjustHealth(int adjustmentValue){
		if (myStats.health.AdjustCurrentStat (adjustmentValue)) {
			Debug.Log ("Player died!");
		} else {
			Debug.Log ("HAHA! Still alive!");
		}
	}

}