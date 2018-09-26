using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Text buttonText;

    private void Start() {
        PlayerPrefs.SetInt("graphNumber", 1);
    }

    public void SwapName() {

        switch (buttonText.text.ToString()) {
            case "Swap : Dijkstra":
                buttonText.text = "Swap : A*";
                PlayerPrefs.SetInt("graphNumber", 2);
                SceneManager.LoadScene(0);
                break;
            case "Swap : A*":
                buttonText.text = "Swap : Dijkstra";
                PlayerPrefs.SetInt("graphNumber", 1);
                SceneManager.LoadScene(0);
                break;

            default:
                Debug.Log("Nothing");
                break;
        }

        Debug.Log(PlayerPrefs.GetInt("graphNumber").ToString());
    }
}
