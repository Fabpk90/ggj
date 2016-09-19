using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Script.MiniGame;

public class Timer : MonoBehaviour {

    public static float duration;
    public float durationn;

	// Use this for initialization
	void Start () {
        duration = durationn;
	}
	
	// Update is called once per frame
	void Update () {

        duration -= Time.deltaTime;

        int seconds = (int) duration % 60;

        GetComponent<Text>().text = "Timer: " + seconds;

        if(seconds <= 0)
        {
            Destroy(gameObject);
        }
    }
}
