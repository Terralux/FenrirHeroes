using UnityEngine;
using System.Collections;

public class BaseStat {
	protected int value;

	public void AdjustStat(int modifyingValue){
		value += modifyingValue;
	}

}