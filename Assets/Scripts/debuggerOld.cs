using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class debuggerOld : MonoBehaviour
{
    public GameObject appear_tiles;
    public GameObject disappear_tiles;
    public GameObject debug_log;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 direction = new Vector3(1f,-6f,7f)/900;

      // debug mode
      if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
          transform.position += direction;
        } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
          transform.position -= direction;
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp)) {
          transform.position += new Vector3(0f,0f,0.01f);
        } else if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown)) {
          transform.position -= new Vector3(0f,0f,0.01f);
        }
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft)) {
          transform.position -= new Vector3(0.01f,0f,0f);
        } else if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight)) {
          transform.position += new Vector3(0.01f,0f,0f);
        }


        // localScale
        transform.localScale = new Vector3(1f,0.1f,1f) * (1f-transform.position.y/3f);
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
          transform.localScale = new Vector3(1f,0.1f,1f);
        }


        // compare tile appear/disappear
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
          appear_tiles.transform.position = this.transform.position;// - transform.right*this.transform.localScale.x;
          appear_tiles.transform.localScale = this.transform.localScale;
          appear_tiles.SetActive(true);
        } else {
          appear_tiles.SetActive(false);
        }
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
          disappear_tiles.SetActive(false);
        } else {
          disappear_tiles.SetActive(true);
        }


        // disparity
        if (OVRInput.Get(OVRInput.RawButton.X)) {
          OVRManager.instance.monoscopic = false;
        } else if (OVRInput.Get(OVRInput.RawButton.Y)) {
          OVRManager.instance.monoscopic = true;
        }


        // target change
        if (OVRInput.Get(OVRInput.RawButton.A)) {
          GameObject.Find("target tile").GetComponent<Renderer>().enabled = false;
        } else {
          GameObject.Find("target tile").GetComponent<Renderer>().enabled = true;
        }
        if (OVRInput.Get(OVRInput.RawButton.B)) {
          //GameObject.Find("target tile tmp").GetComponent<Renderer>().enabled = false;
        } else {
          //GameObject.Find("target tile tmp").GetComponent<Renderer>().enabled = true;
        }


        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);
        debug_log.GetComponent<TextMesh>().text = $"HSV: {Hue:F3},{Saturation:F3},{Value:F3}\n" + transform.position + "\n";
      } else {
        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);
        debug_log.GetComponent<TextMesh>().text = $"HSV: {Hue:F3},{Saturation:F3},{Value:F3}\n" + transform.position + "\n";
      }
    }
}
