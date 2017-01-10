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

		/*
		 * actiontile
		 * passable and impassable
		 * event
		 * cover and prop
		*/

		switch (myTile.myType) {
		case ObjectTypes.TILE:
			markAsStartTile.SetActive (true);
			addActionTile.SetActive (true);
			makeEventTile.SetActive (true);
			deleteStructure.SetActive (false);
			break;
		case ObjectTypes.PROP:
			markAsStartTile.SetActive (true);
			addActionTile.SetActive (true);
			makeEventTile.SetActive (true);
			deleteStructure.SetActive (true);
			break;
		case ObjectTypes.COVER:
			markAsStartTile.SetActive (true);
			addActionTile.SetActive (true);
			makeEventTile.SetActive (true);
			deleteStructure.SetActive (true);
			break;
		case ObjectTypes.OBSTACLE:
			markAsStartTile.SetActive (false);
			addActionTile.SetActive (false);
			makeEventTile.SetActive (false);
			deleteStructure.SetActive (true);
			break;
		case ObjectTypes.CREATURE:
			markAsStartTile.SetActive (false);
			addActionTile.SetActive (false);
			makeEventTile.SetActive (false);
			deleteStructure.SetActive (true);
			break;
		}
	}
}