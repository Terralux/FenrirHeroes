using UnityEngine;
using System.Collections;

[System.Serializable]
public class Interactive : Structure{
	public GameObject target;

	public Interactive(int ID){
		GraphicsID = ID;
	}
}