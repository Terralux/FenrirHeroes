using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTileUIHandler : MonoBehaviour {

	public GameObject deleteStructure;
	public GameObject deleteTile;
	public GameObject markAsStartTile;
	public GameObject duplicate;

	public GameObject makeEventTile;
	public GameObject addActionTile;

	/*
	 * DeleteStructure
	 * DeleteTile
	 * MarkAsStartTile
	 * ConnectToAction
	 * Duplicate
	*/

	public void ShowOptions(BaseTile myTile){
		gameObject.SetActive (true);
		deleteTile.SetActive (true);
		duplicate.SetActive (true);

		if (myTile.myStructure != null) {
			deleteStructure.SetActive (true);
		} else {
			if (myTile.myStructure as Obstacle != null) {
				markAsStartTile.SetActive (true);
				addActionTile.SetActive (true);
				makeEventTile.SetActive (true);
			}
		}
	}
}