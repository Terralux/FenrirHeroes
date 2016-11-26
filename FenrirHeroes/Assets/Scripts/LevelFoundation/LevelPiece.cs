using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelPiece {
	public string name;
	public int OriginPointX = 0;
	public int OriginPointY = 0;
	public List<Tile> tiles = new List<Tile> ();	//A Tiles xPos and yPos will be relative to its parent LevelPiece

	public LevelPiece(){
		name = "New Level Piece";
	}
}