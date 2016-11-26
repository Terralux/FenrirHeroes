using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchController : MonoBehaviour {

	[Range(0.2f, 1f)]
	public float delay = 1f;

	private GameObject currentlySelectedObject;

	private GameObject selectedObject;
	private GameObject furthestSelectedObject;

	private enum TileControllerState
	{
		EMPTY,
		SINGULAR,
		DETAILED,
		MULTIPLE
	}

	private TileControllerState tileControllerState = TileControllerState.EMPTY;

	private int xOffset;
	private int yOffset;

	void Update(){
		RaycastHit hit;
		Ray ray;

		switch (tileControllerState) {
		case TileControllerState.EMPTY:
			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);

					if (Physics.Raycast (ray, out hit, 100f)) {
						if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
							if (hit.collider.gameObject.transform.childCount > 0) {
								Debug.Log ("Start Selecting Object");
								currentlySelectedObject = hit.collider.gameObject.transform.GetChild (0).gameObject;
								StartCoroutine (WaitForGrabActivation ());
							}
						}
					}
				}
			}

			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);

					if (Physics.Raycast (ray, out hit, 100f)) {
						if (hit.collider.gameObject.transform.childCount > 0) {
							if (currentlySelectedObject == hit.collider.gameObject.transform.GetChild (0).gameObject) {
								Debug.Log ("Selected an Object");
								selectedObject = currentlySelectedObject;
								tileControllerState = TileControllerState.SINGULAR;
								StopAllCoroutines ();
							}
						}
					}
				}
			}
			break;
		case TileControllerState.SINGULAR:
			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				
			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.collider.gameObject.CompareTag ("TemplateTile")) {
					selectedObject.transform.position = hit.collider.transform.position;

					if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Ended) {
							if (selectedObject.transform.parent != hit.collider.gameObject) {
								selectedObject.transform.parent.GetComponent<TemplateTile> ().TransferTileData (hit.collider.gameObject.GetComponent<TemplateTile> ());
								selectedObject = null;
								tileControllerState = TileControllerState.EMPTY;
							} else {
								selectedObject.transform.localPosition = new Vector3 (0, 0, 0);
								selectedObject = null;
								tileControllerState = TileControllerState.EMPTY;
							}
						}
					}
				}
			}
			break;
		case TileControllerState.DETAILED:
			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);

			if (Physics.Raycast (ray, out hit, 100f)) {
				if (Input.touchCount > 0) {
					if (Input.GetTouch (0).phase == TouchPhase.Ended) {
						if (hit.collider.gameObject != currentlySelectedObject) {
							Vector3 offset = selectedObject.transform.parent.position - hit.collider.gameObject.transform.position;
							xOffset = (int)(offset.x);
							yOffset = (int)(offset.z);
							tileControllerState = TileControllerState.MULTIPLE;

							GameObject target;
							GameObject rowTarget = selectedObject;

							markedObjects.Clear ();

							for (int i = 0; i < Mathf.Abs (yOffset) + 1; i++) {
								markedObjects.Add (rowTarget);
								target = rowTarget;
								for (int j = 0; j < Mathf.Abs (xOffset); j++) {
									if (xOffset > 0) {
										if (target.transform.parent.GetComponent<TemplateTile> ().left.transform.childCount > 0) {
											markedObjects.Add (target.transform.parent.GetComponent<TemplateTile> ().left.transform.GetChild (0).gameObject);
										}
									} else {
										if (target.transform.parent.GetComponent<TemplateTile> ().right.transform.childCount > 0) {
											markedObjects.Add (target.transform.parent.GetComponent<TemplateTile> ().right.transform.GetChild (0).gameObject);
										}
									}
									target = markedObjects [markedObjects.Count - 1];
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
							// show UI TILE OPTIONS ----------------------------------------------------------------------------------------------------------
						}
					}
				}
			}
			break;
		case TileControllerState.MULTIPLE:
			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);

			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.collider.gameObject.CompareTag ("TemplateTile")) {

					selectedObject.transform.position = hit.collider.transform.position;

					MoveMarkedTiles ();

					if (Input.touchCount > 0) {
						if (Input.GetTouch (0).phase == TouchPhase.Ended) {
							selectedObject.transform.parent.GetComponent<TemplateTile> ().TransferTileData (hit.collider.gameObject.GetComponent<TemplateTile> ());
							selectedObject = null;
							tileControllerState = TileControllerState.EMPTY;
						}
					}
				}
			}
			break;
		}
	}

	private List<GameObject> markedObjects = new List<GameObject>();

	void OnDrawGizmos(){
		foreach (GameObject g in markedObjects) {
			if (g != null) {
				Gizmos.DrawWireSphere (g.transform.position, 0.4f);
			}
		}
	}

	void OnGUI(){
		GUI.Box (new Rect (10, 10, 100, 30), markedObjects.Count.ToString());
	}

	void MoveMarkedTiles(){
		Vector3 offset = selectedObject.transform.parent.position - selectedObject.transform.position;
		foreach (GameObject g in markedObjects) {
			g.transform.position = g.transform.parent.position - offset;
		}
	}

	IEnumerator WaitForGrabActivation(){
		yield return new WaitForSeconds (delay);
		selectedObject = currentlySelectedObject;
		tileControllerState = TileControllerState.DETAILED;
		Debug.Log ("Selected an advanced object");
	}

	public void DeleteSelectedObjects(bool isDeletingEntireTile){
		if (tileControllerState != TileControllerState.MULTIPLE) {
			if (isDeletingEntireTile) {
				selectedObject.transform.parent.GetComponent<TemplateTile> ().RemoveTile ();
				Destroy (selectedObject);
			} else {
				selectedObject.transform.parent.GetComponent<TemplateTile> ().RemoveStructure ();
			}
		} else {
			foreach (GameObject go in markedObjects) {
				if (isDeletingEntireTile) {
					go.transform.parent.GetComponent<TemplateTile> ().RemoveTile ();
					Destroy (go);
				} else {
					go.transform.parent.GetComponent<TemplateTile> ().RemoveStructure ();
				}
			}
		}
		tileControllerState = TileControllerState.EMPTY;
	}
}
