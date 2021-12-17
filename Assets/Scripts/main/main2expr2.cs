using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class main2expr2 : MonoBehaviour//, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // button
    public void OnClick(){
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        SceneManager.LoadScene("expr2");
    }

    /*
    public void OnPointerClick(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    public void OnPointerEnter(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    public void OnPointerExit(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    */
}
