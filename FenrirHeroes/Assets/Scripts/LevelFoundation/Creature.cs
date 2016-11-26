using UnityEngine;
using System.Collections;

public class Creature : Structure {
	public string name;
	public string speech;

	public Creature(int ID) {
		GraphicsID = ID;
	}
}