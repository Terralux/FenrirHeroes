using UnityEngine;
using System.Collections;

public class AdjustableStat : BaseStat {
	int currentValue;

	public int GetCurrentStat(){
		return currentValue;
	}

	public bool AdjustCurrentStat(int modifyingValue){
		currentValue += modifyingValue;
		return currentValue <= 0;
	}
}
