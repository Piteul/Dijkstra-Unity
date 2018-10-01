using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public Text buttonText;

    private void Start() {
        if(GameManager.instance.astar) {
            buttonText.text = "Swap : A*";
        }
        else {
            buttonText.text = "Swap : Dijkstra";
        }
    }

    public void SwapName() {

        switch (buttonText.text.ToString()) {
            case "Swap : Dijkstra":
                buttonText.text = "Swap : A*";
                GameManager.instance.astar = true;
                GameManager.instance.updateGraph();
                SceneManager.LoadScene(0);
                break;
            case "Swap : A*":
                buttonText.text = "Swap : Dijkstra";
                GameManager.instance.updateGraph();
                SceneManager.LoadScene(0);
                break;

            default:
                Debug.Log("Nothing");
                break;
        }

    }
}
