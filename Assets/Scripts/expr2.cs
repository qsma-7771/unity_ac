using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class expr2 : MonoBehaviour
{

    private int exprNo;
    private string id;
    public GameObject explanation;
    public float meanLuminance;
    public float Volatility;

    public GameObject filter;
    public GameObject fakefilter;
    public GameObject backfilter;

    public GameObject debug_log;

    // Start is called before the first frame update
    void Start()
    {
        exprNo = 0;
        id = "initialized";
        explanation.GetComponent<TextMesh>().text = "実験を始めます. \n 人差し指でトリガーを引いてください. ";
        meanLuminance = 0.55f;
        Volatility = 1.00f;
    }

    // Update is called once per frame
    void Update()
    {
      int[] tile_colors = { 3,-38,8,-33,-19,-26,-38,24, 24,-11,24,34,-5,30,-11,15, -26,3,8,-5,30,-38,34,-8, -33,30,15,-11,-26,38,24,-26, -38,-19,21,-33,-5,30,15,38, 34,15,-33,-38,-26,-5,-33,-26, -5,30,8,-19,8,-38,38,-33, 8,-38,34,8,15,3,-19,15 };

      // reset & data get
      if ( (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || (id != "initialized")) && !OVRInput.Get(OVRInput.RawButton.LHandTrigger) ) {
        // Instruction
        if (id == "initialized") {
          id = exprNo.ToString();
          // next instruction text
          explanation.GetComponent<TextMesh>().text = "左のタイルを右のタイルと同じ色に調整してもらいます. \n 人差し指でトリガーを引いて実験を始めてください. ";
        } else {
          exprNo++;
          // color initialize
          if (exprNo % 2 == 1) {
            meanLuminance = (float)new System.Random().NextDouble();
            Volatility = 0.50f;
          } else {
            meanLuminance = 0.55f;
            Volatility = (float)new System.Random().NextDouble() * 2;
          }
          id = "initialized";
          // next instruction text
          explanation.GetComponent<TextMesh>().text = "調整したら人差し指でトリガーを引いてください. ";
        }

        // output
        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/expr2.csv", append:true, System.Text.Encoding.UTF8);
        sw.WriteLine(
          DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
          id + "," +
          OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y + "," +
          OVRInput.Get(OVRInput.RawButton.LHandTrigger) + "," +
          OVRManager.instance.monoscopic + "," +
          GameObject.Find("OVRCameraRig").GetComponent<MotionParallax>().stationary + "," +
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
        // position & size
        if (exprNo % 6 < 2) {
          filter.SetActive(true);
          fakefilter.SetActive(false);
          backfilter.SetActive(false);
        } else if (exprNo % 6 < 4) {
          filter.SetActive(false);
          fakefilter.SetActive(true);
          backfilter.SetActive(false);
        } else {
          filter.SetActive(false);
          fakefilter.SetActive(true);
          backfilter.SetActive(true);
        }
        // disparity
        if (exprNo % 12 < 6) {
          OVRManager.instance.monoscopic = false;
        } else {
          OVRManager.instance.monoscopic = true;
        }
        // motion parallax
        if (exprNo % 24 < 12) {
          GameObject.Find("OVRCameraRig").GetComponent<MotionParallax>().stationary = true;
        } else {
          GameObject.Find("OVRCameraRig").GetComponent<MotionParallax>().stationary = false;
        }

        if (id == "24") {
          explanation.GetComponent<TextMesh>().text = "実験終了です. そのままお待ちください. ";
          SceneManager.LoadScene("main");
        }

        Debug.Log(exprNo);
        Debug.Log(id);
      }

      // color
      // input
      float coefficient = 0.003f;
      if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
        coefficient = coefficient/3;
      }
      var max_limit = 0.38f;
      var min_limit = -0.38f;
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        if (exprNo % 2 == 1) {
          meanLuminance += coefficient * Mathf.Pow(OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y,3);
          if (meanLuminance<0) {meanLuminance=0;}
          if (1.00f < meanLuminance + max_limit * Volatility) {
            meanLuminance = 1.00f - max_limit * Volatility;
          } else if (meanLuminance + min_limit * Volatility < 0.00f) {
            meanLuminance = -min_limit * Volatility;
          }
        }
        if (exprNo % 2 == 0) {
          Volatility += coefficient * Mathf.Pow(OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y,3);
          if (Volatility<0) {Volatility=0;}
          if (1.00f < meanLuminance + max_limit * Volatility) {
            Volatility = (1.00f - meanLuminance)/max_limit;
          } else if (meanLuminance + min_limit * Volatility < 0.00f) {
            Volatility = -meanLuminance/min_limit;
        }
        }
      }
      /*
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
        Volatility += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        if (Volatility<0) {Volatility=0;}
        if (1.00f < meanLuminance + max_limit * Volatility) {
          meanLuminance = 1.00f - max_limit * Volatility;
        } else if (meanLuminance + min_limit * Volatility < 0.00f) {
          meanLuminance = -min_limit * Volatility;
        }
      }
      */
      // all tile color change
      for (int i = 0; i < transform.childCount; i++) {
        GameObject child_tile = transform.GetChild(i).gameObject;

        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(child_tile.GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

        Value = meanLuminance + tile_colors[i]/100f * Volatility;
        child_tile.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);
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
