using UnityEngine;
using System.Collections;

public class BaseStat {

	//Set to one(1) for testing purposes. Can be changed back later (0).
	protected int value = 1;

	public int getStat(){
		return value;
	}
		

	public void AdjustStat(int modifyingValue){
		value += modifyingValue;
	}

}