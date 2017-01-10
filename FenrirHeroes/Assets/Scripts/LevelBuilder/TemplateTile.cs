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

		if (myTile.myGraphics != null) {
			structure = tile.transform.GetChild (0).gameObject;
		}

	}

	public void TransferTileData(TemplateTile tempTile){
		
		if (this != tempTile) {
			tempTile.myTile = new BaseTile (myTile.TileGraphicID, myTile.myDirection, (int)tempTile.transform.position.x, (int)tempTile.transform.position.z);

			tempTile.tile = tile;
			tempTile.tile.transform.SetParent (tempTile.transform);

			if (structure != null) {
				tempTile.structure = structure;
				tempTile.structure.transform.SetParent (tempTile.tile.transform);
				tempTile.myTile.myGraphics = myTile.myGraphics;
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
				myTile = new BaseTile (ID, TileDirections.UpRight, (int)transform.position.x, (int)transform.position.z);
				tile = go;
				tile.transform.SetParent (transform);
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.OBSTACLE:
			if (myTile != null) {
				if (myTile.myGraphics == null) {
					myTile.myGraphics = new Obstacle (ID) as GraphicsObject;
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
				if (myTile.myGraphics == null) {
					myTile.myGraphics = new Cover (ID) as GraphicsObject;
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
				if (myTile.myGraphics == null) {
					myTile.myGraphics = new Creature (ID) as GraphicsObject;
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
		myTile.myGraphics = null;

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
		myTile.isPlayerStartTile = !myTile.isPlayerStartTile;
		BM.UpdateTileGraphics (tile);
		BM.UpdateTile (myTile);
	}

	public void AddNewActionToTile(TileAction tileAction){
		myTile.myTilesActions.Add (tileAction);
	}

	public void MakeEventTile(int triggersBeforeActivation){
		myTile.isEventTile = true;
	}

	public void Rotate(){
		if (myTile.myGraphics != null) {
			System.Array A = System.Enum.GetValues (typeof(TileDirections));
			if ((int)myTile.myGraphics.myDirection < 3) {
				myTile.myGraphics.myDirection = (TileDirections)A.GetValue ((int)myTile.myGraphics.myDirection + 1);
			} else {
				myTile.myGraphics.myDirection = (TileDirections)A.GetValue (0);
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
		BM.UpdateTile (myTile);
	}
}