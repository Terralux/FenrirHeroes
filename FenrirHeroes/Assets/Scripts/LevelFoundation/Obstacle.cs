using UnityEngine;
using System.Collections;

[System.Serializable]
public class Obstacle : GraphicsObject {
	public int health = 0;

	public Obstacle(int ID) {
		GraphicsID = ID;
	}
}