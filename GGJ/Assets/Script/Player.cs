using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    /*
    *   0 maison, 1 taupe
    *
    */

    public static int position = 0;
    //uses this to know where the plqyer needs to be tped

    public GameObject[] spawnPoint; 

    private Vector3 movementVector;
    public float movementSpeed = 5.0f;
    private List<Sprite> movementSheetUp = new List<Sprite>();
    private List<Sprite> movementSheetDown = new List<Sprite>();
    private List<Sprite> movementSheetLeft = new List<Sprite>();
    private List<Sprite> movementSheetRight = new List<Sprite>();

    // 0 nothing , 1 = left , 2 = right,  3 = up 4 = down
    private int movementDirection;
    private int movementDirectionBlocked;
    private Vector2 movementDestination;

    private int idSprite = 0;
    private int nbSprite = 5;

    private float timeStart;
    private float timeEnd;

    public bool isHome = false;

    private bool isIdle = true;

    // Use this for initialization
    void Start()
    {

        //load all the sprite      
        Sprite[] spriteLoad = Resources.LoadAll<Sprite>("SpriteSheets/test/player");

        int i = 0, x = 0, j = 0, c = 0;

        //put them in order in the list
        for (i = 0; i < 5; i++)
        {
            movementSheetRight.Add(spriteLoad[i]);
        }

        for (x = 5; x < nbSprite * 2; x++)
        {
            movementSheetLeft.Add(spriteLoad[x]);
        }

        for (j = 10; j < nbSprite * 3; j++)
        {
            movementSheetDown.Add(spriteLoad[j]);
        }

        for (c = 15; c < nbSprite * 4; c++)
        {
            movementSheetUp.Add(spriteLoad[c]);
        }

        movementVector = new Vector2();
        movementDestination = new Vector2();

        timeStart = Time.deltaTime;

        GetComponent<SpriteRenderer>().sprite = movementSheetDown[2];
        isIdle = true;

        if(isHome)
            transform.position = spawnPoint[position].transform.position;

           
        // gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        isIdle = false;

        timeEnd += Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") > 0 && movementDirectionBlocked != 2)//goes to the right
            goRight();
        else if (Input.GetAxisRaw("Horizontal") < 0 && movementDirectionBlocked != 1)
            goLeft();
        else if (Input.GetAxisRaw("Vertical") > 0 && movementDirectionBlocked != 3)//goes up
            goToward();
        else if (Input.GetAxisRaw("Vertical") < 0 && movementDirectionBlocked != 4)
            goBackward();
        else//is idle
        {
            isIdle = true;
            idSprite = 2;
        }

        if (timeEnd - timeStart > 0.25f && !isIdle)
        {
            idSprite = (idSprite + 1) % nbSprite;
            timeStart = timeEnd;
        }

        //idle = 2
        if (movementDirection == 1) GetComponent<SpriteRenderer>().sprite = movementSheetLeft[idSprite];
        else if (movementDirection == 2) GetComponent<SpriteRenderer>().sprite = movementSheetRight[idSprite];
        else if (movementDirection == 3) GetComponent<SpriteRenderer>().sprite = movementSheetUp[idSprite];
        else if (movementDirection == 4) GetComponent<SpriteRenderer>().sprite = movementSheetDown[idSprite];

        Debug.Log(isIdle);
        if (!isIdle)
        {
            Vector3 newPos = transform.position;
            newPos += movementVector;

            transform.position = newPos;
        }

        movementVector = new Vector2();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
        {
            isIdle = true;
            idSprite = 2;

            movementDirectionBlocked = movementDirection;

            Debug.Log("collides");
        }
        else
        {
            Debug.Log("qdqsdqsd");

            isIdle = false;
            movementDirectionBlocked = 0;
        }

        if(other.tag == "bro")
        {
            GameManager.loadhome();
        }

        if(other.tag == "taupetepart")
        {
            position = 1;
            GameManager.taupetepart();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "block")
        {
            isIdle = false;
            movementDirectionBlocked = 0;
        }
    }

    private void goBackward()
    {
        isIdle = false;
        movementVector.z -= movementSpeed * Time.deltaTime;
        movementDirection = 4;
    }

    private void goToward()
    {
        isIdle = false;
        movementVector.z += movementSpeed * Time.deltaTime;
        movementDirection = 3;
    }

    private void goLeft()
    {
        isIdle = false;
        movementVector.x -= movementSpeed * Time.deltaTime;
        movementDirection = 1;
    }

    private void goRight()
    {
        isIdle = false;
        movementVector.x += movementSpeed * Time.deltaTime;
        movementDirection = 2;
    }
}
