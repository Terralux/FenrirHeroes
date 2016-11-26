using UnityEngine;
using System.Collections;

public class BuilderManager : MonoBehaviour {

	private int floorIndex = 0;

	private GameObject[,] templateTileObjects = new GameObject[20, 20];
	private GameObject[,,] tileObjects = new GameObject[20, 20, 20];

	public GameObject templateTile;

	private Level currentLevel;

	private PieceManager pm;

	void Awake(){
		Toolbox.RegisterComponent<BuilderManager> (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			MoveUpAFloor ();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			MoveDownAFloor ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			ClearLevelData ();
		}
	}

	void InstantiateBuildingField(){
		for (int z = 0; z < 20; z++) {
			for (int x = 0; x < 20; x++) {
				templateTileObjects [x, z] = Instantiate (templateTile, new Vector3 (x, floorIndex, z), templateTile.transform.rotation) as GameObject;
				RecolorTemplateTile (x, z);
			}
		}

		for (int z = 0; z < 20; z++) {
			for (int x = 0; x < 20; x++) {
				TemplateTile tt = templateTileObjects [x, z].GetComponent<TemplateTile> ();
				if (x + 1 <= 19) {
					tt.right = templateTileObjects [x + 1, z];
				}
				if (x - 1 >= 0) {
					tt.left = templateTileObjects [x - 1, z];
				}
				if (z + 1 <= 19) {
					tt.up = templateTileObjects [x, z + 1];
				}
				if (z - 1 >= 0) {
					tt.down = templateTileObjects [x, z - 1];
				}
			}
		}
	}

	public void RecolorTemplateTile(int x, int z){
		if (currentLevel.tiles [x, floorIndex, z] != null) {
			if (currentLevel.tiles [x, floorIndex, z].myStructure != null) {
				templateTileObjects[x, z].GetComponent<MeshRenderer>().material.color = Color.red;
			} else {
				templateTileObjects[x, z].GetComponent<MeshRenderer>().material.color = Color.green;
			}
		} else {
			templateTileObjects[x, z].GetComponent<MeshRenderer>().material.color = Color.white;
		}
	}

	public void MoveUpAFloor(){
		floorIndex++;
		if (floorIndex > 19) {
			floorIndex = 19;
		}
		for (int z = 0; z < 20; z++) {
			for (int x = 0; x < 20; x++) {
				templateTileObjects [x, z].transform.DetachChildren ();
				templateTileObjects [x, z].transform.position = new Vector3 (x, floorIndex, z);

				if (tileObjects [x, floorIndex, z] != null) {
					tileObjects [x, floorIndex, z].transform.SetParent (templateTileObjects [x, z].transform);
					templateTileObjects [x, z].GetComponent<TemplateTile> ().SetTileData (currentLevel.tiles [x, floorIndex, z], tileObjects [x, floorIndex, z]);
				}

				RecolorTemplateTile (x, z);
			}
		}
	}

	public void MoveDownAFloor(){
		floorIndex--;
		if (floorIndex < 0) {
			floorIndex = 0;
		}
		for (int z = 0; z < 20; z++) {
			for (int x = 0; x < 20; x++) {
				templateTileObjects [x, z].transform.DetachChildren ();
				templateTileObjects [x, z].transform.position = new Vector3 (x, floorIndex, z);

				if (tileObjects [x, floorIndex, z] != null) {
					tileObjects [x, floorIndex, z].transform.SetParent (templateTileObjects [x, z].transform);
					templateTileObjects [x, z].GetComponent<TemplateTile> ().SetTileData (currentLevel.tiles [x, floorIndex, z], tileObjects [x, floorIndex, z]);
				}

				RecolorTemplateTile (x, z);
			}
		}
	}

	public void SaveLevel(){
		Debug.Log ("Saved Tile changes");
		Toolbox.FindComponent<LevelDataManager> ().Save (currentLevel);
	}

	public void UpdateTileGraphics(GameObject templateTileGraphics){
		tileObjects [(int)templateTileGraphics.transform.parent.position.x, floorIndex, (int)templateTileGraphics.transform.parent.position.z] = templateTileGraphics;
	}

	public void UpdateTile(Tile updatedTile){
		currentLevel.tiles [updatedTile.xPos, floorIndex, updatedTile.yPos] = updatedTile;
		SaveLevel ();
	}

	public void RemoveTile(int x, int y){
		currentLevel.tiles [x, floorIndex, y] = null;
		if (tileObjects [x, floorIndex, y] != null) {
			tileObjects [x, floorIndex, y] = null;
		}
		SaveLevel ();
	}

	public void SetLevel(Level selectedLevel) {
		currentLevel = selectedLevel;
		InstantiateBuildingField ();
		InstantiateLevelTiles ();
		MoveDownAFloor ();
	}

	public void InstantiateLevelTiles() {

		if (pm == null) {
			pm = Toolbox.FindComponent<PieceManager> ();
		}

		for (int z = 0; z < 20; z++) {
			for (int y = 0; y < 20; y++) {
				for (int x = 0; x < 20; x++) {
					if (currentLevel.tiles [x, y, z] != null) {
						GameObject tempGO = pm.GetPiece (currentLevel.tiles [x, y, z].TileGraphicID, ObjectTypes.TILE);
						tileObjects[x,y,z] = Instantiate(tempGO, new Vector3(x, y, z), tempGO.transform.rotation) as GameObject;

						if (currentLevel.tiles [x, y, z].myStructure != null) {
							tempGO = pm.GetPiece (currentLevel.tiles [x, y, z].myStructure.GraphicsID, ObjectTypes.OBSTACLE);
							(Instantiate(tempGO, new Vector3(x, y, z), tempGO.transform.rotation) as GameObject).transform.SetParent(tileObjects[x,y,z].transform);
						}
					}
				}
			}
		}
	}

	public void ClearLevelData(){
		currentLevel = new Level ();
		SaveLevel ();
	}
}