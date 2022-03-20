using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/1019974/how-to-access-emission-color-of-a-material-in-scri.html

public class ColorLerp : MonoBehaviour
{
    [ColorUsage(true, true)]
    public Color color1 = new Color(0.0f, 1.35f, 3.0f);
    [ColorUsage(true, true)]
    public Color color2 = new Color(3.0f, 1.85f, 0.0f);

    public float duration = 30.0f;
    public float durationOffset = 0.0f;

    Renderer rend;

    public bool changeEmissionInstead = false;

    // Use this for initialization
    void Start()
    {

        rend = GetComponent<Renderer>();

        if (changeEmissionInstead)
        {
            rend.material.EnableKeyword("_EMISSION");
        }

    }

    // Update is called once per frame
    void Update()
    {

        // ping-pong between the colors over the duration
        //float lerp = Mathf.PingPong(Time.time, duration) / duration;
        float lerp = Mathf.PingPong(Time.time + durationOffset, duration) / duration;

        if (!changeEmissionInstead)
        {
            rend.material.color = Color.Lerp(color1, color2, lerp);
        }
        else
        {
            rend.material.SetColor("_EmissionColor", Color.Lerp(color1, color2, lerp));
        }


    }
}
