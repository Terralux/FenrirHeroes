using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	[Range(0.2f, 1f)]
	public float delay = 1f;

	private TileTargetManager targetManager;

	private TileControllerState tileControllerState = TileControllerState.EMPTY;

	private int xOffset;
	private int yOffset;

	void Awake() {
		targetManager = GetComponent<TileTargetManager> ();
		if (SystemInfo.deviceType != DeviceType.Desktop) {
			this.enabled = false;
		}
	}

	void Update(){
		RaycastHit hit;
		Ray ray;

		switch (tileControllerState) {
		case TileControllerState.EMPTY:
			if (Input.GetMouseButtonDown (0)) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
						if (hit.collider.gameObject.transform.childCount > 0) {
							Debug.Log ("Start Selecting Object");
							targetManager.currentlySelectedObject = hit.collider.gameObject.transform.GetChild (0).gameObject;
							StartCoroutine (WaitForGrabActivation ());
						}
					}
				}
			}

			if (Input.GetMouseButtonUp (0)){
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit, 100f)) {
					if (hit.collider.gameObject.transform.childCount > 0) {
						if (targetManager.currentlySelectedObject == hit.collider.gameObject.transform.GetChild (0).gameObject) {
							Debug.Log ("Selected an Object");
							targetManager.selectedObject = targetManager.currentlySelectedObject;
							tileControllerState = TileControllerState.SINGULAR;
							StopAllCoroutines ();
						}
					}
				}
			}
			break;
		case TileControllerState.SINGULAR:
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
					targetManager.selectedObject.transform.position = hit.collider.transform.position;

					if (Input.GetMouseButtonUp (0)) {
						if (targetManager.selectedObject.transform.parent != hit.collider.gameObject) {
							targetManager.selectedObject.transform.parent.GetComponent<TemplateTile> ().TransferTileData (hit.collider.gameObject.GetComponent<TemplateTile> ());
							targetManager.selectedObject = null;
							tileControllerState = TileControllerState.EMPTY;
						} else {
							targetManager.selectedObject.transform.localPosition = new Vector3 (0, 0, 0);
							targetManager.selectedObject = null;
							tileControllerState = TileControllerState.EMPTY;
						}
					}
				}
			}
			break;
		case TileControllerState.DETAILED:
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100f)) {
				if (Input.GetMouseButtonUp (0)) {
					if (hit.collider.gameObject != targetManager.currentlySelectedObject.transform.parent.gameObject) {
						Vector3 offset = targetManager.selectedObject.transform.parent.position - hit.collider.gameObject.transform.position;
						xOffset = (int)(offset.x);
						yOffset = (int)(offset.z);
						tileControllerState = TileControllerState.MULTIPLE;

						GameObject target;
						GameObject rowTarget = targetManager.selectedObject;

						targetManager.markedObjects.Clear ();

						for (int i = 0; i < Mathf.Abs (yOffset) + 1; i++) {
							targetManager.markedObjects.Add (rowTarget);
							target = rowTarget;
							for (int j = 0; j < Mathf.Abs (xOffset); j++) {
								if (xOffset > 0) {
									if (target.transform.parent.GetComponent<TemplateTile> ().left.transform.childCount > 0) {
										targetManager.markedObjects.Add (target.transform.parent.GetComponent<TemplateTile> ().left.transform.GetChild (0).gameObject);
									}
								} else {
									if (target.transform.parent.GetComponent<TemplateTile> ().right.transform.childCount > 0) {
										targetManager.markedObjects.Add (target.transform.parent.GetComponent<TemplateTile> ().right.transform.GetChild (0).gameObject);
									}
								}
								target = targetManager.markedObjects [targetManager.markedObjects.Count - 1];
							}
							if (yOffset > 0) {
								if (rowTarget.transform.parent.GetComponent<TemplateTile> ().down.transform.childCount > 0) {
									rowTarget = rowTarget.transform.parent.GetComponent<TemplateTile> ().down.transform.GetChild (0).gameObject;
								}
							} else {
								if (rowTarget.transform.parent.GetComponent<TemplateTile> ().up.transform.childCount > 0) {
									rowTarget = rowTarget.transform.parent.GetComponent<TemplateTile> ().up.transform.GetChild (0).gameObject;
								}
							}
						}
					} else {
						Debug.Log ("Show UI Tile options");
						targetManager.ShowAdvancedOptionsForSelectedTile ();
						tileControllerState = TileControllerState.EMPTY;
					}
				}
			}
			break;
		case TileControllerState.MULTIPLE:
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.collider.gameObject.CompareTag ("TemplateTile")) {

					targetManager.selectedObject.transform.position = hit.collider.transform.position;

					MoveMarkedTiles ();

					if (Input.GetMouseButtonUp (0)) {
						targetManager.selectedObject.transform.parent.GetComponent<TemplateTile> ().TransferTileData(hit.collider.gameObject.GetComponent<TemplateTile> ());
						targetManager.selectedObject = null;
						tileControllerState = TileControllerState.EMPTY;
					}
				}
			}
			break;
		}
  	}

	void MoveMarkedTiles(){
		Vector3 offset = targetManager.selectedObject.transform.parent.position - targetManager.selectedObject.transform.position;
		foreach (GameObject g in targetManager.markedObjects) {
			g.transform.position = g.transform.parent.position - offset;
		}
	}

	IEnumerator WaitForGrabActivation(){
		yield return new WaitForSeconds (delay);
		targetManager.selectedObject = targetManager.currentlySelectedObject;
		tileControllerState = TileControllerState.DETAILED;
		Debug.Log ("Selected an advanced object");
	}

	public void SetControllerState(TileControllerState controllerState){
		Debug.Log (controllerState);
		tileControllerState = controllerState;
	}
}