using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class main2expr1 : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    public void OnPointerClick(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
    public void OnPointerEnter(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    public void OnPointerExit(PointerEventData pointerEventData) {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
