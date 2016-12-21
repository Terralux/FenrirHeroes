using UnityEngine;
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
		Toolbox.FindComponent<EventManager> ().OnLevelWasLoaded ();
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

						if (currentLevel.tiles [x, y, z].myStructure != null) {
							tempGO = pm.GetPiece (currentLevel.tiles [x, y, z].myStructure.GraphicsID, ObjectTypes.OBSTACLE);
							Quaternion rotationWithOffset = Quaternion.Euler (0, 90 * (int)currentLevel.tiles[x, y, z].myStructure.myDirection, 0);
							(Instantiate (tempGO, new Vector3 (x, y, z), rotationWithOffset) as GameObject).transform.SetParent (tileObjects [x, y, z].transform);
							bto.currentGameState = TileGameplayState.IMPASSABLE;
						} else {
							bto.currentGameState = TileGameplayState.PASSABLE;
						}

						if (Toolbox.FindComponent<SceneMaster> ().IsGameScene ()) {
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

							if (!hasFoundFirstTile) {
								//if (currentLevel.tiles [x, y, z].isAPlayerStartTile) {
								GameObject tempPlayer = Instantiate (playerPrefab, new Vector3 (x, y, z), playerPrefab.transform.rotation) as GameObject; 
								tempPlayer.transform.SetParent (tileObjects [x, y, z].transform);
								tempPlayer.AddComponent<PlayerInputHandler> ();
								bto.myPlayer = tempPlayer.AddComponent<Player> ();
								tempPlayer.AddComponent<NetworkEntity> ();
								hasFoundFirstTile = true;
							}
						}
					}
				}
			}
		}
	}
}