using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class move_tile : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 direction = new Vector3(1f,-6f,7f)/900;

      if (OVRInput.GetDown(OVRInput.RawButton.B)) {
        direction = new Vector3(0f,1f,0f)/100;
      }

      // reset & get data
      if (OVRInput.GetDown(OVRInput.RawButton.A)) {
        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/position.csv", append:true, System.Text.Encoding.UTF8);
        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," + (transform.position.x) + "," +(transform.position.y) + "," + (transform.position.z)+","+ transform.rotation.x +","+ transform.rotation.y +","+ transform.rotation.z +","+ transform.rotation.w);
        sw.Flush();
        sw.Close();

        transform.position = new Vector3(0.5f,0f,3.5f);
      }

      if (OVRInput.Get(OVRInput.RawButton.RThumbstickUp)) {
        transform.position += direction;
      } else if (OVRInput.Get(OVRInput.RawButton.RThumbstickDown)) {
        transform.position -= direction;
      }
      transform.localScale = new Vector3(1f,0.1f,1f) * (1f-transform.position.y/6f);
    }
}
