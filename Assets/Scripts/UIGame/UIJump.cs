using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJump : MonoBehaviour {
	bool isPressed = false;

	public void onPointerDownRaceButton(){
		if (!isPressed) {
			isPressed = true;
			Data.character.Jump ();
		}
	}

	public void onPointerUpRaceButton(){
		isPressed = false;
	}
}
