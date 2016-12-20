using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Stats myStats = new Stats();

	public void Move(TileDirections direction){
		BaseTileObject bto = transform.parent.GetComponent<BaseTileObject> ();
		bto.MovePlayer (direction);
		bto.RemovePlayer ();

		transform.localPosition = Vector3.zero;

	}

	public void AdjustHealth(int adjustmentValue){
		if ((myStats.stats[0] as AdjustableStat).AdjustCurrentStat (adjustmentValue)) {
			Debug.Log ("Player died!");
		} else {
			Debug.Log ("HAHA! Still alive!");
		}
	}

	public Stats getStats(){
		return myStats;
	}

}