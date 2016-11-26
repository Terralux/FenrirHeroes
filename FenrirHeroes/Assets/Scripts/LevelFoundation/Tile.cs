using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tile : PlacableObject{
	public int TileGraphicID;
	public Structure myStructure;

	public Tile(int ID, int x, int y) : base (x, y) {
		TileGraphicID = ID;
	}

	public Tile(int ID, Structure s, int x, int y) : base (x, y) {
		TileGraphicID = ID;
		myStructure = s;
	}
}