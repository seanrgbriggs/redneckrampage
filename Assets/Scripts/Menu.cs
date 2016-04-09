using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public void Quit() {
        Application.Quit();
    }

    public void Play() {
        Application.LoadLevel("MainGame");
    }
}
