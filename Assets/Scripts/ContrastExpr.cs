using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrastExpr : MonoBehaviour
{

    public int exprNo;
    public GameObject test;
    public float meanLuminance;
    public float Volatility;

    // Start is called before the first frame update
    void Start()
    {
        exprNo = 0;
        meanLuminance = 55;
        Volatility = 1;
    }

    // Update is called once per frame
    void Update()
    {
      float Hue, Saturation, Value;
      UnityEngine.Color.RGBToHSV(test.GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);
      float x;
      x = (Value - meanLuminance) / Volatility;

      // color
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        meanLuminance += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        Volatility += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
      }
      Value = meanLuminance + x * Volatility;
      test.GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
          meanLuminance = 55;
          Volatility = 1;
        }
      }
    }
}
