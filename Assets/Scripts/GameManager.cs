using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public Graph g1;
    public Graph g2;
    public Follower follower;
    public Text buttonText;
    public bool astar = false;



    private void Awake() {
        if(instance == null) {
            instance = this;
        }else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void updateGraph() {

    //    Debug.Log(astar.ToString());
    //    if (!(astar)) { //Dijkstra
    //        buttonText.text = "Swap : A*";
    //        astar = true;
    //        follower.m_Graph = g1;
    //        g2.gameObject.SetActive(false);
    //        g1.gameObject.SetActive(true);
    //        SceneManager.LoadScene(0);
    //    }
    //    else {
    //        buttonText.text = "Swap : Dijkstra";
    //        astar = false;
    //        follower.m_Graph = g2;
    //        g1.gameObject.SetActive(false);
    //        g2.gameObject.SetActive(true);
    //        SceneManager.LoadScene(0);
    //    }

    //}



}
