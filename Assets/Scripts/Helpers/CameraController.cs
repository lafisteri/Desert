using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float speed = 2.0F;

	[SerializeField]
	private Transform target;

	private void Update(){
		
		if (!Data.character)
			return;
		if (!target)
			target = Data.character.transform;
		
		Vector3 position = target.position;
		position.z = -10.0F;
		position.y = 0.0f;//что бы камера не пригала вверх вниз
		transform.position = Vector3.Lerp (transform.position, position, speed * Time.deltaTime);

	}
}