using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifiableStat : BaseStat {
	private int totalModValue;
	private List<ModValue> modValues = new List<ModValue> ();

	public int GetStat(){
		return value + totalModValue;
	}

	public void AddNewModification(ModValue newMod){
		modValues.Add (newMod);
	}

	public void TurnEnd(){
		totalModValue = 0;
		for (int i = 0; i < modValues.Count; i++) {
			if (modValues [i].TurnEnd ()) {
				modValues.RemoveAt (i);
				i--;
			} else {
				totalModValue += modValues [i].modValue;
			}
		}
	}
}

public class ModValue {
	private int activeTurns = 0;
	public int modValue = 0;

	public ModValue(int activeTurns, int modValue){
		this.activeTurns = activeTurns;
		this.modValue = modValue;
	}

	public bool TurnEnd(){
		activeTurns--;
		if (activeTurns == 0) {
			return true;
		} else {
			return false;
		}
	}
}