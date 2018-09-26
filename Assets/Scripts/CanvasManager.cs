using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public Slider sld_nodes;
    public Text nb_nodes;

	// Use this for initialization
	void Start () {
        sld_nodes.value = PlayerPrefs.GetFloat("sliderValue");
        

    }
	
	// Update is called once per frame
	void Update () {
        nb_nodes.text = sld_nodes.value.ToString();
        PlayerPrefs.SetFloat("sliderValue",sld_nodes.value);

    }
}
