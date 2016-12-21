using UnityEngine;
using System.Collections;

public class TemplateTile : MonoBehaviour {

	private BaseTile myTile;
	public GameObject tile;
	public GameObject structure;
	public GameObject creature;

	private BuilderManager BM;

	public GameObject left;
	public GameObject right;
	public GameObject up;
	public GameObject down;

	void Awake(){
		BM = Toolbox.FindComponent<BuilderManager> ();
	}

	public void SetTileData(BaseTile tempTile, GameObject tempTileGO){
		myTile = tempTile;
		tile = tempTileGO;

		if (myTile.myStructure != null) {
			if (myTile.myStructure as Creature != null) {
				creature = tile.transform.GetChild (0).gameObject;
			} else {
				structure = tile.transform.GetChild (0).gameObject;
			}
		}
	}

	public void TransferTileData(TemplateTile tempTile){
		
		if (this != tempTile) {
			tempTile.myTile = new BaseTile (myTile.TileGraphicID, (int)tempTile.transform.position.x, (int)tempTile.transform.position.z);

			tempTile.tile = tile;
			tempTile.tile.transform.SetParent (tempTile.transform);

			if (structure != null) {
				tempTile.structure = structure;
				tempTile.structure.transform.SetParent (tempTile.tile.transform);
				tempTile.myTile.myStructure = myTile.myStructure;
			}

			if (creature != null) {
				tempTile.creature = creature;
				tempTile.creature.transform.SetParent (tempTile.tile.transform);
				//tempTile.myTile.myStructure = myTile.myStructure;
			}

			BM.UpdateTileGraphics (tempTile.tile);
			BM.UpdateTile (tempTile.myTile);

			if (transform.childCount == 0) {
				Debug.Log ("I am getting removed!");
				RemoveTile ();
			}
			tile = null;
			structure = null;
			creature = null;
		}
	}

	public void Add(GameObject go, ObjectTypes objectType, int ID) {
		Debug.Log (go.name + " : " + objectType + " -- " + ID);
		switch (objectType) {
		case ObjectTypes.TILE:
			if (myTile == null) {
				myTile = new BaseTile (ID, (int)transform.position.x, (int)transform.position.z);
				tile = go;
				tile.transform.SetParent (transform);
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.OBSTACLE:
			if (myTile != null) {
				if (myTile.myStructure == null) {
					myTile.myStructure = new Obstacle (ID);
					structure = go;
					structure.transform.SetParent (tile.transform);
				} else {
					Destroy (go);
				}
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.COVER:
			if (myTile != null) {
				if (myTile.myStructure == null) {
					myTile.myStructure = new Cover (ID);
					structure = go;
					structure.transform.SetParent (tile.transform);
				} else {
					Destroy (go);
				}
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.CREATURE:
			if (myTile != null) {
				if (myTile.myStructure == null) {
					myTile.myStructure = new Creature (ID);
					structure = go;
					structure.transform.SetParent (tile.transform);
				} else {
					Destroy (go);
				}
			} else {
				Destroy (go);
			}
			break;
		}

		BM.UpdateTileGraphics (tile);
		BM.UpdateTile (myTile);
	}

	public void RemoveStructure(){
		if (myTile.myStructure != null) {
			myTile.myStructure = null;
		}

		Destroy (structure);
		BM.UpdateTile (myTile);
	}

	public void RemoveTile(){
		BM.RemoveTile (myTile.xPos, myTile.yPos);
		if (myTile != null) {
			myTile = null;
		}
	}

	public BaseTile GetTile(){
		return myTile;
	}

	public void SwitchStartTileOption(){
		(myTile as PlayerStartTile).isAPlayerStartTile = !(myTile as PlayerStartTile).isAPlayerStartTile;
		BM.UpdateTileGraphics (tile);
		BM.UpdateTile (myTile);
	}

	public void AddNewActionToTile(TileAction tileAction){
		if (myTile as ActionTile == null) {
			ActionTile at = new ActionTile (myTile.TileGraphicID, myTile.myStructure, (myTile as PlayerStartTile) == null ? false : (myTile as PlayerStartTile).isAPlayerStartTile, myTile.xPos, myTile.yPos);
			at.myTilesActions.Add (tileAction);
			myTile = at as BaseTile;
			Debug.Log ("Wasn't a derived ActionTile");
		} else {
			(myTile as ActionTile).myTilesActions.Add (tileAction);
			Debug.Log ("This time I was a derived ActionTile");
		}
	}

	public void MakeEventTile(int triggersBeforeActivation){
		(myTile as EventTile).interactionsBeforeActivation = triggersBeforeActivation;
	}

	public void Rotate(){
		if (myTile.myStructure != null) {
			System.Array A = System.Enum.GetValues (typeof(TileDirections));
			if ((int)myTile.myStructure.myDirection < 3) {
				myTile.myStructure.myDirection = (TileDirections)A.GetValue ((int)myTile.myStructure.myDirection + 1);
			} else {
				myTile.myStructure.myDirection = (TileDirections)A.GetValue (0);
			}
			structure.transform.Rotate (new Vector3 (0, 90, 0));
		} else {
			System.Array A = System.Enum.GetValues (typeof(TileDirections));
			if ((int)myTile.myDirection < 3) {
				myTile.myDirection = (TileDirections)A.GetValue ((int)myTile.myDirection + 1);
			} else {
				myTile.myDirection = (TileDirections)A.GetValue (0);
			}
			tile.transform.Rotate (new Vector3 (0, 90, 0));
		}
	}
}