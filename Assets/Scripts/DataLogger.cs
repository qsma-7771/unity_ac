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

    // Start is called before the first frame update
    void Start()
    {
      exprNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
      float Hue, Saturation, Value;
      UnityEngine.Color.RGBToHSV(GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

      // reset & get data
      if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.B) ) {
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
          explanation.GetComponent<TextMesh>().text = "Aのタイルの色と同じ色に調整してください. \n 調整後Bボタン(奥)を押してください. ";
        } else {
          explanation.GetComponent<TextMesh>().text = "Cのタイルの色と同じ色に調整してください. \n 調整後Aボタン(手前)を押してください. ";
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
          sw.WriteLine(
            DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
            "Initialized" + "," +
            Value
          );
          sw.Flush();
          sw.Close();
        }

        Debug.Log(exprNo);

      }

      // color
      if (OVRInput.Get(OVRInput.RawButton.RThumbstickRight)) {
        Value -= 0.001f;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickLeft)) {
        Value += 0.001f;
      }
      if (false) { // lock
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
          Saturation += 0.001f;
        } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
          Saturation -= 0.001f;
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight)) {
          Hue += 0.001f;
        } else if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft)) {
          Hue -= 0.001f;
        }
      }
      GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);

    }
}
