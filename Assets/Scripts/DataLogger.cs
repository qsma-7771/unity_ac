using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class DataLogger : MonoBehaviour
{
    public int exprNo;
    public GameObject explanation;
    public Material initial_material;

    // Start is called before the first frame update
    void Start()
    {
      exprNo = 0;
      explanation.GetComponent<TextMesh>().text = "実験を始めます. \n 右手人差し指で決定を押してください. ";
    }

    // Update is called once per frame
    void Update()
    {
      float Hue, Saturation, Value;
      UnityEngine.Color.RGBToHSV(GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

      // reset & get data
      if ( OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && !OVRInput.Get(OVRInput.RawButton.LHandTrigger) ) {
        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position.csv", append:true, System.Text.Encoding.UTF8);
        sw.WriteLine(
          DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
          exprNo + "," +
          OVRInput.Get(OVRInput.RawButton.A) + "," +
          OVRInput.Get(OVRInput.RawButton.B) + "," +
          OVRManager.instance.monoscopic + "," +
          (transform.position.x) + "," +
          (transform.position.y) + "," +
          (transform.position.z) + "," +
          transform.localScale.x + "," +
          transform.localScale.y + "," +
          transform.localScale.z + "," +
          GetComponent<Renderer>().material.color + "," +
          Hue + "," +
          Saturation + "," +
          Value
        );
        sw.Flush();
        sw.Close();

        exprNo++;
        // Initialize
        transform.position = new Vector3(0.5f,0f,3.5f);
        transform.localScale = new Vector3(1f,0.1f,1f);
        GetComponent<Renderer>().material.color = new Color32(65,65,65,1);

        // target
        if (exprNo % 2 == 1) {
          explanation.GetComponent<TextMesh>().text = "Aのタイルの色と同じ色に調整してください. \n 調整後右手人差し指でトリガーを引いてください. ";
        } else {
          explanation.GetComponent<TextMesh>().text = "Cのタイルの色と同じ色に調整してください. \n 調整後右手人差し指でトリガーを引いてください. ";
        }
        // position & size
        if (exprNo % 6 < 2) {
          transform.position = new Vector3(0.5f,0f,3.5f);
        } else {
          transform.position = new Vector3(0.5f,0f,3.5f) + new Vector3(-0.5f,3f,-3.5f)/2;
        }
        if (exprNo % 6 < 4) {
          transform.localScale = new Vector3(1f,0.1f,1f) * (1f-transform.position.y/3f);
        } else {
          transform.localScale = new Vector3(1f,0.1f,1f);
        }
        // disparity
        if (exprNo % 12 < 6) {
          OVRManager.instance.monoscopic = false;
        } else {
          OVRManager.instance.monoscopic = true;
        }
        // initial color randomize
        Value = (float)new System.Random().NextDouble();

        if (exprNo > 12) {
          explanation.GetComponent<TextMesh>().text = "実験終了です";
        } else {
          StreamWriter sw2 = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position.csv", append:true, System.Text.Encoding.UTF8);
          sw2.WriteLine(
            DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
            "Initialized" + "," +
            Value
          );
          sw2.Flush();
          sw2.Close();
        }

        Debug.Log(exprNo);

      }

      // color
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        Value += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        Saturation += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
        Hue += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
      }
      GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
          GetComponent<Renderer>().material = initial_material;
        }
      }

    }
}
