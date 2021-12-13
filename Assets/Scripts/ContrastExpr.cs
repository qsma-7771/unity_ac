using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ContrastExpr : MonoBehaviour
{

    public int exprNo;
    public GameObject explanation;
    public float meanLuminance;
    public float Volatility;

    //public GameObject debug_log;

    // Start is called before the first frame update
    void Start()
    {
        exprNo = 0;
      explanation.GetComponent<TextMesh>().text = "実験を始めます. \n この実験では左のタイルの色を右手のスティック(上下)で調整してもらいます. \n 右手人差し指でトリガーを引いてください. ";
        meanLuminance = 0.55f;
        Volatility = 1.00f;
    }

    // Update is called once per frame
    void Update()
    {
      int[] tile_colors = { 3,-38,8,-33,-19,-26,-38,24, 24,-11,24,34,-5,30,-11,15, -26,3,8,-5,30,-38,34,-8, -33,30,15,-11,-26,38,24,-26, -38,-19,21,-33,-5,30,15,38, 34,15,-33,-38,-26,-5,-33,-26, -5,30,8,-19,8,-38,38,-33, 8,-38,34,8,15,3,-19,15 };

      // reset & data get
      if ( OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && !OVRInput.Get(OVRInput.RawButton.LHandTrigger) ) {
        string id;
        id = "test";

        // Instruction

        // output
        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position.csv", append:true, System.Text.Encoding.UTF8);
        sw.WriteLine(
          DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
          id + "," +
          OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y + "," +
          OVRInput.Get(OVRInput.RawButton.LHandTrigger) + "," +
          OVRManager.instance.monoscopic + "," +
          (transform.position.x) + "," +
          (transform.position.y) + "," +
          (transform.position.z) + "," +
          transform.localScale.x + "," +
          transform.localScale.y + "," +
          transform.localScale.z + "," +
          meanLuminance + "," +
          Volatility
        );
        sw.Flush();
        sw.Close();

        // target
      }

      // color
      // input
      var max_limit = 0.38f;
      var min_limit = -0.38f;
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        meanLuminance += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        if (meanLuminance<0) {meanLuminance=0;}
        if (1.00f < meanLuminance + max_limit * Volatility) {
          Volatility = (1.00f - meanLuminance)/max_limit;
        } else if (meanLuminance + min_limit * Volatility < 0.00f) {
          Volatility = -meanLuminance/min_limit;
        }
        Volatility += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
        if (1.00f < meanLuminance + max_limit * Volatility) {
          meanLuminance = 1.00f - max_limit * Volatility;
        } else if (meanLuminance + min_limit * Volatility < 0.00f) {
          meanLuminance = -min_limit * Volatility;
        }
      }
      // all tile color change
      for (int i = 0; i < transform.childCount; i++) {
        GameObject child_tile = transform.GetChild(i).gameObject;

        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(child_tile.GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

        Value = meanLuminance + tile_colors[i]/100f * Volatility;
        child_tile.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);

        Debug.Log(tile_colors[i]/100f);
        Debug.Log(Value);
      }

      // reset color
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
          meanLuminance = 0.55f;
          Volatility = 1.00f;
        }
      }

      // debug
      //debug_log.GetComponent<TextMesh>().text = $"meanLuminance: {meanLuminance:F3}\n Volatility: {Volatility:F3}\n tile x {transform.childCount}\n" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "\n";
    }
}
