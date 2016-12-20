using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTileUIHandler : MonoBehaviour {

	public GameObject deleteStructure;
	public GameObject deleteTile;
	public GameObject markAsStartTile;
	public GameObject connectToAction;
	public GameObject duplicate;

	public void ShowOptions(Tile myTile){
		gameObject.SetActive (true);
		deleteTile.SetActive (true);
		duplicate.SetActive (true);

		if (myTile.myStructure != null) {
			deleteStructure.SetActive (true);
		} else {
			markAsStartTile.SetActive (true);
			connectToAction.SetActive (true);
		}
	}
}