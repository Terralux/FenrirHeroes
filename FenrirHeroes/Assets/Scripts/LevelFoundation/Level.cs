using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Level {
	public string name;
	public BaseTile[,,] tiles = new BaseTile[20,20,20];

	public List<LevelPiece> levelPieces = new List<LevelPiece>();

	public Level(){
		name = "New Level";
	}

	public Level(string name){
		this.name = name;
	}
}