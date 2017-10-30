using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAttack : MonoBehaviour {
	bool isPressed = false;

	public void onPointerDownRaceButton(){
		if (!isPressed) {
			isPressed = true;
			Data.character.Attack ();
		}
	}

	public void onPointerUpRaceButton(){
		isPressed = false;
	}
}
