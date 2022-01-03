using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class debugger : MonoBehaviour
{
    public GameObject camera;
    public GameObject target;
    public GameObject debug_log;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      // this -- camera
      Vector3 direction = this.transform.position - camera.transform.position;
      //Vector3 direction = new Vector3(1f,-6f,7f)/900;

      // debug mode
      if (OVRInput.Get(OVRInput.RawButton.LHandTrigger)) {

        // target move
        if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
          transform.position += 0.01f * direction/direction.magnitude;
        } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
          transform.position -= 0.01f * direction/direction.magnitude;
        }
        // front back move
        transform.position += new Vector3(0f,0f,0.01f) * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).y;
        /*
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickUp)) {
          transform.position += new Vector3(0f,0f,0.01f);
        } else if (OVRInput.Get(OVRInput.RawButton.LThumbstickDown)) {
          transform.position -= new Vector3(0f,0f,0.01f);
        }
        */
        // right left move
        transform.position += new Vector3(0.01f,0f,0f) * OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
        /*
        if (OVRInput.Get(OVRInput.RawButton.LThumbstickLeft)) {
          transform.position -= new Vector3(0.01f,0f,0f);
        } else if (OVRInput.Get(OVRInput.RawButton.LThumbstickRight)) {
          transform.position += new Vector3(0.01f,0f,0f);
        }
        */

        // localScale
        //transform.localScale = new Vector3(0.5f,0.05f,0.5f) * (1f-transform.position.y/3f);
        //if (OVRInput.Get(OVRInput.RawButton.)) {
          //transform.localScale = new Vector3(0.5f,0.05f,0.5f);
        //}

        // disparity
        if (OVRInput.Get(OVRInput.RawButton.X)) {
          OVRManager.instance.monoscopic = true;
        } else if (OVRInput.Get(OVRInput.RawButton.Y)) {
          OVRManager.instance.monoscopic = false;
        }

        // motion parallax
        if (OVRInput.Get(OVRInput.RawButton.A)) {
          camera.GetComponent<MotionParallax>().stationary = true;
        } else if (OVRInput.Get(OVRInput.RawButton.B)) {
          camera.GetComponent<MotionParallax>().stationary = false;
        }

        // back main scene
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
          SceneManager.LoadScene("main");
        }

        // color debug
        float Hue, Saturation, Value;
        UnityEngine.Color.RGBToHSV(target.GetComponent<Renderer>().material.color, out Hue, out Saturation, out Value);
        debug_log.GetComponent<TextMesh>().text = $"HSV: {Hue:F3},{Saturation:F3},{Value:F3}\n" + transform.position + "\n";
      } else {
        debug_log.GetComponent<TextMesh>().text = "";
      }
    }
}
