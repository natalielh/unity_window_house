using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureOffsetter : MonoBehaviour {

	//array to store original offset data of textures set in the inspector
	private Vector2[] scroll_origoffsets;

	//inspector-settable scroll speed that acts as a manual multiplier
	[Range(-3.0f,3.0f)]
	public float local_scroll_speed;
	//scroll speed multiplier set through player interaction
	private float multiplier;

	//material information
	private Material[] mats;
	//number of textures applied to this GameObject (the thing this script is attached to)
	private int num_mats;

	//inspector-settable array to set the speeds for the scrolling textures
	[Range(-0.5f,0.5f)]
	public float[] scroll_speeds;

	//stored original position on the x-z plane of this gameobject
	private Vector2 position_horiz;
	//position of the player on the x-z plane (use main camera position)
	private Vector2 player_horiz;

	// Use this for initialization
	void Start () {
		
		Renderer renderer = GetComponent<Renderer>();
		mats = renderer.materials;
		num_mats = renderer.materials.Length;

		scroll_origoffsets = new Vector2[num_mats];

		position_horiz = new Vector2(transform.position.x, transform.position.z);

		//get original offsets set in the editor for each texture attached
		for(int i=0; i<num_mats; i++){
			scroll_origoffsets[i] = mats[i].GetTextureOffset("_MainTex");
		}
	}

	// Update is called once per frame
	void Update () {

		player_horiz = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z);
		multiplier = 1.0f/(Vector2.Distance(position_horiz, player_horiz));

		//float offset_previous = ;
		//float offset_new = ;

		for(int i=0; i<num_mats; i++){
			float offset = (mats[i].GetTextureOffset("_MainTex").x) + (Time.deltaTime * multiplier * local_scroll_speed * scroll_speeds[i]);
			mats[i].SetTextureOffset("_MainTex", new Vector2(offset, scroll_origoffsets[i].y));
		}
		
	}
}
