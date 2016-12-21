using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlacableObject {
	public int xPos;
	public int yPos;

	public TileDirections myDirection = TileDirections.UpRight;

	public PlacableObject(TileDirections direction, int x, int y){
		myDirection = direction;
		xPos = x;
		yPos = y;
	}
}