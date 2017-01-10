using UnityEngine;
using System.Collections;

public class Creature : GraphicsObject {
	public string name;
	public string speech;

	public Creature(int ID) {
		GraphicsID = ID;
	}
}