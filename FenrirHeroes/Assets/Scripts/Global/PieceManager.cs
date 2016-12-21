using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PieceManager : MonoBehaviour {

	public CollectiveTileData tileData;
	public CollectiveStructureData structureData;
	//public GameObject creatureData;

	public GameObject tilePiecePanel;
	public GameObject structurePiecePanel;
	public GameObject creaturePiecePanel;

	public GameObject pieceButtonPrefab;

	void Awake(){
		Toolbox.RegisterComponent<PieceManager> (this);

		if (tilePiecePanel != null) {
			int count = 0;
			Debug.Log (count);
			foreach (TileData td in tileData.tileData) {
				GameObject go = Instantiate (pieceButtonPrefab, tilePiecePanel.transform) as GameObject;
				go.GetComponentInChildren<Text> ().text = td.graphicsObject.name;
				DragableLevelPiece dlp = go.GetComponent<DragableLevelPiece> ();
				dlp.myPrefab = td.graphicsObject;
				dlp.ID = count;
				dlp.ObjectType = ObjectTypes.TILE;
				count++;
			}

		}
		if(structurePiecePanel != null){
			int count = 0;
			foreach (StructureData sd in structureData.structureData) {
				GameObject go = Instantiate (pieceButtonPrefab, structurePiecePanel.transform) as GameObject;
				go.GetComponentInChildren<Text> ().text = sd.graphicsObject.name;
				DragableLevelPiece dlp = go.GetComponent<DragableLevelPiece> ();
				dlp.myPrefab = sd.graphicsObject;
				dlp.ID = count;
				dlp.ObjectType = ObjectTypes.OBSTACLE;
				count++;
			}
		}
	}

	public GameObject GetPiece(int id, ObjectTypes type){
		switch (type) {
		case ObjectTypes.TILE:
			return tileData.tileData[id].graphicsObject;
		case ObjectTypes.OBSTACLE:
			return structureData.structureData[id].graphicsObject;
			break;
		case ObjectTypes.CREATURE:
			//return tileData.tileData[id].graphicsObject;
			break;
		}

		return null;
	}
}