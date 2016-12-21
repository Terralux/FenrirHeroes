using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlacableObject {
	public int xPos;
	public int yPos;

	public TileDirections myDirection = TileDirections.UpRight;

	public PlacableObject(int x, int y){
		xPos = x;
		yPos = y;
	}
}