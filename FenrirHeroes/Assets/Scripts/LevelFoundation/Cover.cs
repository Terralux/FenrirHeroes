using UnityEngine;
using System.Collections;

[System.Serializable]
public class Cover : GraphicsObject {
	public int coverBonus = 1;
	public int health = 0;

	public Cover(int ID) {
		GraphicsID = ID;
	}
}