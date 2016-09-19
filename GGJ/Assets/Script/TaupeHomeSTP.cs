using UnityEngine;
using System.Collections;

public class TaupeHomeSTP : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Sprite[] spriteLoad = Resources.LoadAll<Sprite>("SpriteSheets/taupeChef");
        GetComponent<SpriteRenderer>().sprite = spriteLoad[0];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
