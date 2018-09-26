﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The Follower.
/// </summary>
[ExecuteInEditMode]
public class Follower : MonoBehaviour {
    [SerializeField]
    protected int randomNodes;
    [SerializeField]
    protected bool randomStartAndEnd;
    [SerializeField]
    protected Graph m_Graph;
    [SerializeField]
    protected Node m_Start;
    [SerializeField]
    protected Node m_End;
    [SerializeField]
    protected float m_Speed = 0.01f;
    protected Path m_Path = new Path();
    protected Node m_Current;

    [SerializeField]
    protected GameObject trap;

    SpriteRenderer Srend;
    Animator anim;

    void Start() {

        Srend = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        allNodesActive();

        if (randomStartAndEnd) {
            randomLandmark();
        }

        setLandmark();

        if (randomNodes > 0) {
            randomNode(randomNodes);
        }

        m_Path = m_Graph.GetShortestPath(m_Start, m_End);

        //No path, activate trap text
        if (m_Path.length == 0) {
            trap.SetActive(true);
        }

        Follow(m_Path);
    }

    void Update() {
        if (m_Current != null) {

            makeAnimation();

            //adjust speed compared framerate
            float step = m_Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, m_Current.transform.position, step);
        }

        if (Input.GetKeyDown("space")) {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    /// <summary>
    /// manage the follower's animation
    /// </summary>
    void makeAnimation() {

        //up
        if (m_Current.transform.position.y > this.transform.position.y) {
            anim.SetBool("WalkUp", true);
            anim.SetBool("Idle", false);
        }
        else {
            anim.SetBool("WalkUp", false);

        }

        //right
        if (m_Current.transform.position.x > this.transform.position.x) {
            anim.SetBool("WalkRight", true);
            anim.SetBool("Idle", false);
        }
        else {
            anim.SetBool("WalkRight", false);

        }

        //left
        if (m_Current.transform.position.x < this.transform.position.x) {
            anim.SetBool("WalkLeft", true);

        }
        else {
            anim.SetBool("WalkLeft", false);

        }

        //down
        if (m_Current.transform.position.y < this.transform.position.y) {
            anim.SetBool("WalkDown", true);

        }
        else {
            anim.SetBool("WalkDown", false);
        }

        


    }

    /// <summary>
    /// put all nodes active by default
    /// </summary>
    void allNodesActive() {
        for (int i = 0; i < 25; i++) {
            m_Graph.nodes[i].gameObject.SetActive(true);
        }

    }

    /// <summary>
    /// inactive nb nodes
    /// </summary>
    void randomNode(int nb) {
        while (nb > 0) {
            int rand = Random.Range(0, 25);
            //Debug.Log("rand : " + rand.ToString());

            Node temp = m_Graph.nodes[rand];

            if (!(temp.Equals(m_Start) || temp.Equals(m_End))) {
                //Debug.Log("Good");

                m_Graph.nodes[rand].gameObject.SetActive(false);
                --nb;

            }

        }

    }

    /// <summary>
    /// make random start and end
    /// </summary>
    void randomLandmark() {
        int rand = Random.Range(0, 25);
        m_Start = m_Graph.nodes[rand];

        int rand2 = Random.Range(0, 25);

        while (rand == rand2) {
            rand2 = Random.Range(0, 25);
        }

        m_End = m_Graph.nodes[rand2];

    }

    /// <summary>
    /// Change the position of the differents landmarks
    /// </summary>
    void setLandmark() {
        GameObject landmarkStart = GameObject.Find("Landmark_Start");
        GameObject landmarkEnd = GameObject.Find("Landmark_End");

        if (m_Start) {
            landmarkStart.transform.position = m_Start.transform.position;
            landmarkStart.SetActive(true);
        }

        if (m_End) {
            landmarkEnd.transform.position = m_End.transform.position;
            landmarkEnd.SetActive(true);
        }
    }

    /// <summary>
    /// Follow the specified path.
    /// </summary>
    /// <param name="path">Path.</param>
    public void Follow(Path path) {
        StopCoroutine("FollowPath");
        m_Path = path;
        transform.position = m_Path.nodes[0].transform.position;
        StartCoroutine("FollowPath");
    }

    /// <summary>
    /// Following the path.
    /// </summary>
    /// <returns>The path.</returns>
    IEnumerator FollowPath() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update += Update;
#endif
        var e = m_Path.nodes.GetEnumerator();
        while (e.MoveNext()) {
            m_Current = e.Current;

            // Wait until we reach the current target node and then go to next node
            yield return new WaitUntil(() => {
                return transform.position == m_Current.transform.position;
            });
        }
        m_Current = null;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.update -= Update;
#endif
    }

}
