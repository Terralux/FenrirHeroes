using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTestScript : Photon.PunBehaviour {

	public RandomMatchmaker randomMatch;

	public void giveDirection(int direction){
	
		randomMatch.sendDirection ((TileDirections)direction);

	}

}
