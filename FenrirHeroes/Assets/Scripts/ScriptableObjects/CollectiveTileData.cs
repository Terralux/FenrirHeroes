using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Chromecast Heroes/Tile", fileName = "New Tile", order = 1), System.Serializable]
public class CollectiveTileData : ScriptableObject {
	public List<TileData> tileData = new List<TileData> ();
}

[System.Serializable]
public class TileData {
	public GameObject graphicsObject;
}