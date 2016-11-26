using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelDataManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save(Level level){
		BinaryFormatter bf = new BinaryFormatter ();

		if (!File.Exists (Application.persistentDataPath + "/Levels/" + level.name + ".dat")) {
			File.Create (Application.persistentDataPath + "/Levels/" + level.name + ".dat").Dispose();
		}

		FileStream file = File.Open (Application.persistentDataPath + "/Levels/" + level.name + ".dat", FileMode.Open);

		bf.Serialize (file, level);
		file.Close ();
	}

	public void Save(LevelPiece levelPiece){
		BinaryFormatter bf = new BinaryFormatter ();

		if (!File.Exists (Application.persistentDataPath + "/LevelPieces/" + levelPiece.name + ".dat")) {
			File.Create (Application.persistentDataPath + "/LevelPieces/" + levelPiece.name + ".dat").Dispose();
		}

		FileStream file = File.Open (Application.persistentDataPath + "/LevelPieces/" + levelPiece.name + ".dat", FileMode.Open);

		bf.Serialize (file, levelPiece);
		file.Close ();
	}

	public List<Level> LoadLevels(){
		List<Level> levels = new List<Level> ();

		if (!Directory.Exists (Application.persistentDataPath + "/Levels")) {
			CreateDirectories ();
		}

		foreach(string fileName in Directory.GetFiles(Application.persistentDataPath + "/Levels/", "*.dat")){
			BinaryFormatter bf = new BinaryFormatter ();
			Debug.Log (fileName);
			FileStream file = File.Open (fileName, FileMode.Open);
			levels.Add((Level) bf.Deserialize (file));
			file.Close ();
		}

		return levels;
	}

	public void CreateDirectories(){
		Directory.CreateDirectory (Application.persistentDataPath + "/Levels");
		Directory.CreateDirectory (Application.persistentDataPath + "/LevelPieces");
	}
}
