using System.Collections;
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
		selectedObject.transform.localPosition = Vector3.zero;
		selectedObject = Instantiate (selectedObject, selectedObject.transform.position, selectedObject.transform.rotation) as GameObject;
		selectedObject.transform.SetParent (parent);

		SetControllerState (TileControllerState.SINGULAR);
		advancedUIHandler.gameObject.SetActive (false);
	}

	public void ConnectToActiveObject(){
		Debug.Log ("Interactivity not implemented yet!");
		throw new System.NotImplementedException ();
		Reset ();
	}
}