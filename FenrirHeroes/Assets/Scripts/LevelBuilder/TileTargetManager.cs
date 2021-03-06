﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTargetManager : MonoBehaviour {

	[HideInInspector]
	public GameObject currentlySelectedObject;
	[HideInInspector]
	public GameObject selectedObject;

	[HideInInspector]
	public List<GameObject> markedObjects = new List<GameObject>();

	private MouseController mouseController;
	private TouchController touchController;

	public AdvancedTileUIHandler advancedUIHandler;

	void Awake(){
		mouseController = GetComponent<MouseController> ();
		touchController = GetComponent<TouchController> ();
	}

	public void DeleteSelectedObject(bool isDeletingEntireTile){
		if (isDeletingEntireTile) {
			selectedObject.transform.parent.GetComponent<TemplateTile> ().RemoveTile ();
			Destroy (selectedObject);
		} else {
			selectedObject.transform.parent.GetComponent<TemplateTile> ().RemoveStructure ();
		}
		Reset ();
	}

	public void DeleteSelectedObjects(bool isDeletingEntireTiles){
		foreach (GameObject go in markedObjects) {
			if (isDeletingEntireTiles) {
				go.transform.parent.GetComponent<TemplateTile> ().RemoveTile ();
				Destroy (go);
			} else {
				go.transform.parent.GetComponent<TemplateTile> ().RemoveStructure ();
			}
		}
		Reset ();
	}

	private void SetControllerState(TileControllerState controllerState){
		if (mouseController != null) {
			mouseController.SetControllerState(controllerState);
		}
		if (touchController != null) {
			touchController.SetControllerState(controllerState);
		}
	}

	private void Reset(){
		SetControllerState (TileControllerState.EMPTY);
		advancedUIHandler.gameObject.SetActive (false);
	}

	public void ShowAdvancedOptionsForSelectedTile(){
		advancedUIHandler.ShowOptions (selectedObject.transform.parent.GetComponent<TemplateTile>().GetTile());
		SetControllerState (TileControllerState.EMPTY);
	}

	public void MarkPlayerStartTile(){
		selectedObject.transform.parent.GetComponent<TemplateTile> ().SwitchStartTileOption ();
		Reset ();
	}

	public void DuplicateTile(){
		Transform parent = selectedObject.transform.parent;
		BaseTile bt = selectedObject.transform.parent.GetComponent<TemplateTile>().GetTile();
		BaseTile neoBT;

		neoBT = new BaseTile (bt.myTilesActions, bt.triggerSteps, bt.myEventType, bt.myEventTrigger, bt.myGraphics, bt.isPlayerStartTile, bt.myType, bt.TileGraphicID, bt.myDirection, bt.xPos, bt.yPos);

		selectedObject = Instantiate (selectedObject, selectedObject.transform.position, selectedObject.transform.rotation) as GameObject;
		selectedObject.transform.SetParent (parent);
		selectedObject.transform.localPosition = Vector3.zero;

		//selectedObject

		SetControllerState (TileControllerState.SINGULAR);
		advancedUIHandler.gameObject.SetActive (false);
	}

	public void AddActionToObject(int index){
		TileActions action = (TileActions) index;

		switch (action) {
		case TileActions.DeleteObject:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new DeleteObjectTileAction ());
			break;
		case TileActions.MoveCharacter:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new MoveCharacterTileAction ());
			break;
		case TileActions.MultipleUse:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new MultipleUseTileAction ());
			break;
		case TileActions.SpawnObject:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new SpawnObjectTileAction ());
			break;
		case TileActions.Stats:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new StatsTileAction ());
			break;
		case TileActions.SwitchState:
			selectedObject.transform.parent.GetComponent<TemplateTile> ().AddNewActionToTile (new SwitchStateTileAction ());
			break;
		}
		Reset ();
	}

	public void MakeEventTile(){

		Reset ();
	}
}