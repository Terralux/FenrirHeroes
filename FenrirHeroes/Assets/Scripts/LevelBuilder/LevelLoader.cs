﻿using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	protected GameObject[,] templateTileObjects = new GameObject[20, 20];
	protected GameObject[,,] tileObjects = new GameObject[20, 20, 20];

	protected Level currentLevel;

	protected PieceManager pm;

	public GameObject playerPrefab;

	void Awake(){
		Toolbox.RegisterComponent<LevelLoader> (this);
	}

	public void SetLevel(Level selectedLevel) {
		currentLevel = selectedLevel;

		InstantiateLevelTiles ();
		Toolbox.FindComponent<GameStateHandler> ().StartPlayerTurn();
	}

	protected void InstantiateLevelTiles() {

		bool hasFoundFirstTile = false;

		if (pm == null) {
			pm = Toolbox.FindComponent<PieceManager> ();
		}

		for (int z = 0; z < 20; z++) {
			for (int y = 0; y < 20; y++) {
				for (int x = 0; x < 20; x++) {
					if (currentLevel.tiles [x, y, z] != null) {
						GameObject tempGO = pm.GetPiece (currentLevel.tiles [x, y, z].TileGraphicID, ObjectTypes.TILE);
						tileObjects [x, y, z] = Instantiate(tempGO, new Vector3(x, y, z), tempGO.transform.rotation) as GameObject;
						BaseTileObject bto = tileObjects [x, y, z].AddComponent<BaseTileObject> ();

						if (tileObjects [x + 1, y, z] != null) {
							BaseTileObject tempBTO = tileObjects [x + 1, y, z].GetComponent<BaseTileObject> ();
							bto.East = tempBTO;
							tempBTO.West = bto;
						}
						if (tileObjects [x - 1, y, z] != null) {
							BaseTileObject tempBTO = tileObjects [x - 1, y, z].GetComponent<BaseTileObject> ();
							bto.West = tempBTO;
							tempBTO.East = bto;
						}
						if (tileObjects [x, y, z + 1] != null) {
							BaseTileObject tempBTO = tileObjects [x, y, z + 1].GetComponent<BaseTileObject> ();
							bto.North = tempBTO;
							tempBTO.South = bto;
						}
						if (tileObjects [x, y, z - 1] != null) {
							BaseTileObject tempBTO = tileObjects [x, y, z - 1].GetComponent<BaseTileObject> ();
							bto.South = tempBTO;
							tempBTO.North = bto;
						}

						if (currentLevel.tiles [x, y, z].myStructure != null) {
							tempGO = pm.GetPiece (currentLevel.tiles [x, y, z].myStructure.GraphicsID, ObjectTypes.OBSTACLE);
							(Instantiate (tempGO, new Vector3 (x, y, z), tempGO.transform.rotation) as GameObject).transform.SetParent (tileObjects [x, y, z].transform);
							bto.currentGameState = TileGameplayState.IMPASSABLE;
						} else {
							if (!hasFoundFirstTile) {
								GameObject tempPlayer = Instantiate (playerPrefab, new Vector3 (x, y, z), playerPrefab.transform.rotation) as GameObject; 
								tempPlayer.transform.SetParent(tileObjects [x, y, z].transform);
								tempPlayer.AddComponent<PlayerInputHandler> ();
								bto.myPlayer = tempPlayer.AddComponent<Player> ();
								hasFoundFirstTile = true;
							}
							bto.currentGameState = TileGameplayState.PASSABLE;
						}
					}
				}
			}
		}
	}
}