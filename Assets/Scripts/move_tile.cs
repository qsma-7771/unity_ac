using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class move_tile : MonoBehaviour
{
    public GameObject compare_tile;

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
        var compare_pos = 0f;
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
          compare_pos = GameObject.Find("compare").transform.position.x;
        }
        sw.WriteLine(
          DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "," +
          SceneManager.GetActiveScene().name + "," +
          (transform.position.x) + "," +
          (transform.position.y) + "," +
          (transform.position.z) + "," +
          transform.localScale.x + "," +
          transform.localScale.y + "," +
          transform.localScale.z + "," +
          OVRInput.Get(OVRInput.RawButton.RIndexTrigger) + "," +
          OVRInput.Get(OVRInput.RawButton.RHandTrigger) + "," +
          compare_pos
        );
        sw.Flush();
        sw.Close();

        transform.position = new Vector3(0.5f,0f,3.5f);
      }

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
      transform.localScale = new Vector3(1f,0.1f,1f) * (1f-transform.position.y/6f);
      if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
        transform.localScale = new Vector3(1f,0.1f,1f);
      }

      // compare tile
      if (OVRInput.Get(OVRInput.RawButton.RHandTrigger)) {
        compare_tile.transform.position = this.transform.position;// - transform.right*this.transform.localScale.x;
        compare_tile.transform.localScale = this.transform.localScale;
        compare_tile.SetActive(true);
      } else {
        compare_tile.SetActive(false);
      }
    }
}
