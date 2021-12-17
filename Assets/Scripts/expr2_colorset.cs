using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class expr2_colorset : MonoBehaviour
{

    public float meanLuminance;
    public float Volatility;

    // Start is called before the first frame update
    void Start()
    {
        meanLuminance = 0.55f;
        Volatility = 0.50f;
    }

    // Update is called once per frame
    void Update()
    {
      int[] tile_colors = { 3,-38,8,-33,-19,-26,-38,24, 24,-11,24,34,-5,30,-11,15, -26,3,8,-5,30,-38,34,-8, -33,30,15,-11,-26,38,24,-26, -38,-19,21,-33,-5,30,15,38, 34,15,-33,-38,-26,-5,-33,-26, -5,30,8,-19,8,-38,38,-33, 8,-38,34,8,15,3,-19,15 };

      // color
      // all tile color change
      for (int i = 0; i < transform.childCount; i++) {
        GameObject child_tile = transform.GetChild(i).gameObject;

        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(child_tile.GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

        Value = meanLuminance + tile_colors[i]/100f * Volatility;
        child_tile.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);
      }
    }
}
