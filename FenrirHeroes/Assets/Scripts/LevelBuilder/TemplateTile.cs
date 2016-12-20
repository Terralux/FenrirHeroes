using UnityEngine;
using System.Collections;

public class TemplateTile : MonoBehaviour {

	private Tile myTile;
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

	public void SetTileData(Tile tempTile, GameObject tempTileGO){
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
			tempTile.myTile = new Tile (myTile.TileGraphicID, (int)tempTile.transform.position.x, (int)tempTile.transform.position.z);

			tempTile.tile = tile;
			tempTile.tile.transform.SetParent (tempTile.transform);

			if (structure != null) {
				tempTile.structure = structure;
				tempTile.structure.transform.SetParent (tempTile.transform);
			}

			if (creature != null) {
				tempTile.creature = creature;
				tempTile.creature.transform.SetParent (tempTile.transform);
			}

			BM.UpdateTileGraphics (tempTile.tile);
			BM.UpdateTile (tempTile.myTile);

			if (tempTile.transform.childCount == 0) {
				RemoveTile ();
			}
			tile = null;
			structure = null;
			creature = null;
		}
	}

	public void Add(GameObject go, ObjectTypes objectType, int ID) {
		//Debug.Log ("Added an object of type " + objectType + " and current tile is " + myTile);
		switch (objectType) {
		case ObjectTypes.TILE:
			if (myTile == null) {
				myTile = new Tile (ID, (int)transform.position.x, (int)transform.position.z);
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
					structure.transform.SetParent (transform);
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
					structure.transform.SetParent (transform);
				} else {
					Destroy (go);
				}
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.ACTIVE:
			if (myTile != null) {
				if (myTile.myStructure == null) {
					myTile.myStructure = new ActiveObject (ID);
					structure = go;
					structure.transform.SetParent (transform);
				} else {
					Destroy (go);
				}
			} else {
				Destroy (go);
			}
			break;
		case ObjectTypes.INTERACTIVE:
			if (myTile != null) {
				if (myTile.myStructure == null) {
					myTile.myStructure = new Interactive (ID);
					structure = go;
					structure.transform.SetParent (transform);
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
					structure.transform.SetParent (transform);
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

		BM.UpdateTile (myTile);
	}

	public void RemoveTile(){
		BM.RemoveTile (myTile.xPos, myTile.yPos);
		if (myTile != null) {
			myTile = null;
		}
	}

	public Tile GetTile(){
		return myTile;
	}

	public void SwitchStartTileOption(){
		myTile.isAPlayerStartTile = !myTile.isAPlayerStartTile;
		BM.UpdateTileGraphics (tile);
		BM.UpdateTile (myTile);
	}
}