using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public static int life = 3;
    public static int lifeMax = 3;
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Vie" + life + "/" + lifeMax;
	}
}
