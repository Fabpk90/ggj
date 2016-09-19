using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("qsd");
            Application.Quit();
        }
    }

    public void loadHome()
    {
        Application.LoadLevel("Maison - RdC");
    }

    public static void loadhome()
    {
        Application.LoadLevel("Maison - RdC");
    }

    public void loadCoolness()
    {
        Application.LoadLevel("Prospecteur");
    }

    public static void taupetepart()
    {
        Application.LoadLevel("ChasseTaupe");
        Player.position = 1;
    }

    public void labynefaitpaslemoine()
    {
        Application.LoadLevel("Labyrinth Shining");
    }
	
}
