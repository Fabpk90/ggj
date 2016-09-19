using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Script.Trap;
using System.Collections;
using Assets.Script.MiniGame;

public class ChasseTaupe : MonoBehaviour {

    public static Sprite[] spriteLoad;

    public List<GameObject> spawnPoints;
    public static List<int> taupes = new List<int>();

    private bool isWin = false;

    public Taupe taupe;

    static public uint AliveTaupe;

    private uint spawnedTaupe = 0;
    public uint spawningRate = 1;

    static public int spawnPoint;

    public uint taupeToSpawn = 20;

    private float spawningTimer = 0.0f;

    public float spawningSec = 1.0f;

    public float gameDuration = 30.0f;
    public float gameLevel2 = 15.0f;

    private float timeStart;

    public float timeBeforeSpawn = 1.0f;
    public float timeBeforeDestroyingTaupe = 0.5f;

    private System.Random r = new System.Random();

    // Use this for initialization
    void Start () {
        Cursor.visible = false;

        timeStart = Time.time;

        Sprite[] spriteLoad = Resources.LoadAll<Sprite>("SpriteSheets/test/player");

        for (int i = 0; i < 2; i++)
        {
            spawnPoint = findFreeSpace();

            Debug.Log("spawn " + spawnPoint);
                    
            Taupe taupep = (Taupe)Instantiate(taupe, spawnPoints[spawnPoint].transform.position, Quaternion.Euler(90, 90, 0));
            taupes.Add(spawnPoint);
            

            AliveTaupe++;
            spawnedTaupe++;
        }
	}

    private int findFreeSpace()
    {
        int point = -1;

        if (checkIfListFull())
        {
            Debug.Log("Time before" + Time.time);
            StartCoroutine(WaitingForPointsFree());
            Debug.Log("Time after" + Time.time);
        }
        else
            point = findPoint();  

        Debug.Log("searching free space finished");
        return point;
    }

    private int findPoint()
    {
        // List<GameObject> listPoint = spawnPoints;

        List<int> listPoints = new List<int>();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            bool isFound = false;

            for (int x = 0; x < taupes.Count && !isFound; x++)
            {
                if (i == taupes[x])
                {
                    isFound = true;
                }
            }        

            if(!isFound)
            {
                listPoints.Add(i);
            }           
        }

        Debug.Log(listPoints.Count);

        if (listPoints.Count != 0)
        {
            return listPoints[r.Next(listPoints.Count - 1)];
        }

        return -1;
    }

    IEnumerator WaitingForPointsFree()
    {    
            yield return new WaitForSeconds(spawningSec);     
    }

    public static void removeFromTaupes(int taupeIndex)
    {
        //Debug.Log("Destroy");

       for(int i = 0; i < taupes.Count; i++)
        {
            if (taupes[i] == taupeIndex)
            {
                Debug.Log("Destroy");
                taupes.RemoveAt(i);
            }
                
        }
    }

    // Update is called once per frame
    void Update () {

        spawningTimer += Time.deltaTime;

        if(spawningTimer >= spawningSec)
        {
            spawn();
            spawningTimer = 0.0f;
        }

        //checks if it's time for the next level
        if(Time.time - timeStart > gameLevel2)
        {
            spawningRate = 2;
        }

        //game ends if there's no time left
        if(Time.time == timeStart + gameDuration)
        {
            Win();
        }

        if(Timer.duration <= 0)
        {
            Loose();
        }

        if(Life.life <= 0)
        {
            Loose();
        }
	}

    private void spawn()
    {
        if(spawnedTaupe == taupeToSpawn)
        {
            if(AliveTaupe == 0)
            {
                Win();
            }
        }
        else//let's spawn! if we can ;)
        {
            spawnPoint = findFreeSpace();
            if(spawnPoint != -1)
            {
                Taupe taupep = (Taupe)Instantiate(taupe, spawnPoints[spawnPoint].transform.position, Quaternion.Euler(90, 90, 0));
                taupes.Add(spawnPoint);
                
                AliveTaupe++;
                spawnedTaupe++;
            }
            
        }
    }

    private bool checkIfListFull()
    {
        if (taupes.Count == spawnPoints.Count)
            return true;
        else
            return false;
    }

    private bool isSpawnPointFree(int point)
    {
        for(int i = 0; i < taupes.Count; i++)
        {
            if(taupes[i] == point)
            {
                return false;
            }
        }

        return true;
    }

    public void Win()
    {
        isWin = true;

        GameManager.loadhome();
    }

    public static void Loose()
    {
        //load the gameover
        Debug.Log("you bitch!");

        Application.LoadLevel("gameOverTaupe");
    }
}
