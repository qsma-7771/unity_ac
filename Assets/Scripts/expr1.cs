using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class expr1 : MonoBehaviour
{
    public int exprNo;
    public GameObject explanation;
    public GameObject[] descriptions;
    public GameObject[] tiles;
    public Material initial_material;

    // Start is called before the first frame update
    void Start()
    {
      exprNo = 0;
      explanation.GetComponent<TextMesh>().text = "実験を始めます. \n この実験ではBタイルの色を右手のスティック(上下)で調整してもらいます. \n 右手人差し指でトリガーを引いてください. ";
    }

    // Update is called once per frame
    void Update()
    {
      float Hue, Saturation, Value;
      UnityEngine.Color.RGBToHSV(GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);

      // reset & get data
      if ( OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && !OVRInput.Get(OVRInput.RawButton.LHandTrigger) ) {
        string id = "initialized";

        // Instruction
        if (id == "initialized") {
          foreach (GameObject description in descriptions) {
            description.SetActive(true); // guide is visible
          }
          id = exprNo.ToString();
          // next instruction text
          explanation.GetComponent<TextMesh>().text = "BタイルをAタイルと同じ色に調整してもらいます. \n AタイルとBタイルの位置を確認したら, 右手人差し指でトリガーを引いてください. ";
        } else {
          foreach (GameObject description in descriptions) {
            description.SetActive(false); // guide is imvisible
          }
          exprNo++;
          // color initialize
          Value = (float)new System.Random().NextDouble();
          id = "initialized";
          // next instruction text
          explanation.GetComponent<TextMesh>().text = "BタイルをAタイルと同じ色に調整してください. \n 調整したら右手人差し指でトリガーを引いてください. ";
        }
        if (exprNo % 2 == 1) {
          // experiment
          foreach (GameObject description in descriptions) {
            description.SetActive(false);
          }
          explanation.GetComponent<TextMesh>().text = "BタイルをAタイルと同じ色に調整してください. \n 調整したら右手人差し指でトリガーを引いてください. ";
          // initial color randomize
          Value = (float)new System.Random().NextDouble();
          id = "initialized";
        } else {
          // explanation
          foreach (GameObject description in descriptions) {
            description.SetActive(true);
          }
          explanation.GetComponent<TextMesh>().text = "BタイルをAタイルと同じ色に調整してもらいます. \n AタイルとBタイルの位置を確認したら, 右手人差し指でトリガーを引いてください. ";
          id = (exprNo/2).ToString();
        }

        // output
        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position_expr1.csv", append:true, System.Text.Encoding.UTF8);
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
          GetComponent<Renderer>().material.color + "," +
          Hue + "," +
          Saturation + "," +
          Value
        );
        sw.Flush();
        sw.Close();

        // Initialize
        transform.position = new Vector3(0.5f,0f,3.5f);
        transform.localScale = new Vector3(1f,0.1f,1f);
        GetComponent<Renderer>().material = initial_material;


        // target
        // position & size
        if (exprNo % 5 < 1) {
          transform.position = new Vector3(0.5f,0f,3.5f);
        } else if (exprNo % 5 < 3) {
          transform.position = new Vector3(0.5f,0f,3.5f) + new Vector3(-0.5f,3f,-3.5f)/2;
        } else {
          transform.position = new Vector3(0.5f,0f,3.5f) + new Vector3(-0.5f,3f,-3.5f)/4;
        }
        if (exprNo % 5 < 2 || 4 <= exprNo % 5) {
          transform.localScale = new Vector3(0.5f,0.1f,0.5f) * (1f-transform.position.y/3f);
        } else {
          transform.localScale = new Vector3(0.5f,0.1f,0.5f);
        }
        // disparity
        if (exprNo % 10 < 5) {
          OVRManager.instance.monoscopic = false;
        } else {
          OVRManager.instance.monoscopic = true;
        }
        // motion parallax
        if (exprNo % 20 < 10) {
          GameObject.Find("OVRCameraRig").GetComponent<MotionParallax>().stationary = true;
        } else {
          GameObject.Find("OVRCameraRig").GetComponent<MotionParallax>().stationary = false;
        }

        if (20 <= exprNo) {
          explanation.GetComponent<TextMesh>().text = "実験終了です. ヘッドセットを外してください. ";
          foreach (GameObject tile in tiles) {
            tile.SetActive(false);
          }
        }

        Debug.Log(exprNo);
        Debug.Log(id);

      }

      // color
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        Value += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;
        Saturation += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
        Hue += 0.001f * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
      }
      GetComponent<Renderer>().material.color = UnityEngine.Color.HSVToRGB(Hue,Saturation,Value);

      // reset color
      if (!OVRInput.Get(OVRInput.RawButton.LHandTrigger)) { // not debug mode
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
          GetComponent<Renderer>().material = initial_material;
        }
      }

    }
}
