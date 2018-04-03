using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour {

	[Range(-20.0f,20.0f)]
	public float speed;

	//current rotation
	private float curRot = 0;

	void Update () {
		curRot += speed * Time.deltaTime;
		curRot %= 360;
		RenderSettings.skybox.SetFloat("_Rotation", curRot);
	}


}
