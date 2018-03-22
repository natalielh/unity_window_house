using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandOffsetter : MonoBehaviour {

	/*
		This script is used for offsetting 
	*/

	//stores the original offset of the texture
	private Vector2 scroll_origoffset;

	//inspector-settable values that acts as multipliers
	[Range(-3.0f,3.0f)]
	public float displacement_x;
	[Range(-3.0f,3.0f)]
	public float displacement_y;

	//material attached to this gameobject
	private Material mat;

	// Use this for initialization
	void Start () {

		Renderer renderer = GetComponent<Renderer>();
		mat = renderer.material;

		//get the original texture offset set in the Unity editor
		scroll_origoffset = mat.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 offset = new Vector2(
			//the previous texture offset must be saved, and the new offset is added
			mat.GetTextureOffset("_MainTex").x + Random.Range(-1.0f, 1.0f) * displacement_x,
			mat.GetTextureOffset("_MainTex").y + Random.Range(-1.0f, 1.0f) * displacement_y
		);

		mat.SetTextureOffset("_MainTex", Time.deltaTime * offset);
		
	}
}
