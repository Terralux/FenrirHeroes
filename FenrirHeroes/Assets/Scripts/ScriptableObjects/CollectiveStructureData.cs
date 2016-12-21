using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Chromecast Heroes/Structure Data", fileName = "New Structure", order = 1), System.Serializable]
public class CollectiveStructureData : ScriptableObject {
	public List<StructureData> structureData = new List<StructureData> ();
}

[System.Serializable]
public class StructureData {
	public GameObject graphicsObject;
	public ObjectTypes myType;
}