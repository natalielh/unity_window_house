using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTextureOffsetter : MonoBehaviour {

	// array to store original offset data of textures set in the inspector
	private Vector2[] scroll_origoffsets;

	// inspector-settable scroll speed that acts as a manual multiplier
	//[Range(-3.0f,3.0f)]
	//public float local_scroll_speed;

	// scroll speed multiplier set through player interaction
	private float multiplier;
	private readonly float multi_min_dist = 0.1f;
	private readonly float multi_max_dist = 6.0f;
	private readonly float angle_cutoff = 45.0f;

	// material information
	private Material[] mats;
	// number of textures applied to this GameObject (the thing this script is attached to)
	private int num_mats;

	// inspector-settable array to set the speeds for the scrolling textures (allows for achieving a parallax effect manually)
	// **default: stay within the 0.00f-0.05f range**
	[Range(-0.1f,0.1f)]
	public float[] scroll_speeds;

	// stored reference to the player for reading transform data
	private GameObject player;
	// stored original position on the x-z plane of this gameobject
	private Vector2 pos_thispot;
	// position of the player on the x-z plane
	private Vector2 pos_player;

	void Start () {

		player = GameObject.FindWithTag("Player");
		Renderer renderer = GetComponent<Renderer>();
		mats = renderer.materials;
		num_mats = renderer.materials.Length;

		scroll_origoffsets = new Vector2[num_mats];
		//get original offsets set in the editor for each texture attached
		for(int i=0; i<num_mats; i++){
			scroll_origoffsets[i] = mats[i].GetTextureOffset("_MainTex");
		}

		// get the original position of this pot set in the inspector
		pos_thispot = new Vector2(transform.position.x, transform.position.z);
	}
		
	void Update () {

		// get the new player position every frame
		pos_player = new Vector2(player.transform.position.x, player.transform.position.z);

		// get the new camera FORWARD VECTOR every frame
//		Vector3 vec_camerarot = player.transform.forward;
		Vector2 vec_camera_forward = new Vector2(player.transform.forward.x, player.transform.forward.z);
//		float camerarot = player.transform.rotation.y;

		// the vector going from the player to the pot (obtained with vector subtraction)
		Vector2 vec_playertopot = pos_thispot - pos_player;

		// get the new distance between the pot and player every frame
		float distance = Vector2.Distance(pos_thispot, pos_player);


		// **modify the multiplier every frame**
		// calculate and compare the (camera forward vector) with the (vector going from player to pot)
		if (Vector2.Angle (vec_playertopot, vec_camera_forward) < angle_cutoff) {
			multiplier = Map (
				Vector2.Angle (vec_playertopot, vec_camera_forward),
				0.0f,
				angle_cutoff,
				10.0f,
				0.1f
			);
		} else {
			multiplier = 0.1f;
		}

		// check distance parameter
		// check lower boundary
		if (distance < multi_min_dist) {
			multiplier *= 1.0f;

		// check upper boundary
		} else if (distance > multi_max_dist) {
			multiplier *= 0.1f;
		
		// then it falls within the boundary
		} else {
			// distance calculation
			multiplier *= Map(
				distance,
				multi_min_dist,
				multi_max_dist,
				1.0f,
				0.1f
			);

		}
			
			
		for(int i=0; i<num_mats; i++){
			//float offset = (mats[i].GetTextureOffset("_MainTex").x) + (Time.deltaTime * multiplier * local_scroll_speed * scroll_speeds[i]);
            float offset = (mats[i].GetTextureOffset("_MainTex").x) + (Time.deltaTime * multiplier * scroll_speeds[i]);
            mats[i].SetTextureOffset("_MainTex", new Vector2(offset, scroll_origoffsets[i].y));
		}

	}

	// (value, fromLow, fromHigh, toLow, toHigh)
	// referenced from Arduino Map() documentation
	public float Map(float x, float in_min, float in_max, float out_min, float out_max) {
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}

}
