﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSave : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<Character>()) {
			Data.helper.Save ();
		}
	}
}
