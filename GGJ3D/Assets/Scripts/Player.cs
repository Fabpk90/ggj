using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{

    private Vector3 movementVector;
    public float movementSpeed = 5.0f;
    private List<Sprite> movementSheetUp = new List<Sprite>();
    private List<Sprite> movementSheetDown = new List<Sprite>();
    private List<Sprite> movementSheetLeft = new List<Sprite>();
    private List<Sprite> movementSheetRight = new List<Sprite>();

    // 0 nothing , 1 = left , 2 = right,  3 = up 4 = down
    private int movementDirection;
    private Vector2 movementDestination;

    private int idSprite = 0;
    private int nbSprite = 5;

    private float timeStart;
    private float timeEnd;

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

        gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("qd");
    }

    // Update is called once per frame
    void Update()
    {

        timeEnd += Time.deltaTime;

        if (Input.GetAxisRaw("Horizontal") > 0)//goes to the right
            goRight();
        else if (Input.GetAxisRaw("Horizontal") < 0)
            goLeft();
        else if (Input.GetAxisRaw("Vertical") > 0)//goes up
            goToward();
        else if (Input.GetAxisRaw("Vertical") < 0)
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
        if (movementDirection == 2) GetComponent<SpriteRenderer>().sprite = movementSheetRight[idSprite];
        if (movementDirection == 3) GetComponent<SpriteRenderer>().sprite = movementSheetUp[idSprite];
        if (movementDirection == 4) GetComponent<SpriteRenderer>().sprite = movementSheetDown[idSprite];


        Vector3 newPos = transform.position;
        newPos += movementVector;

        transform.position = newPos;
        movementVector = new Vector2();
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
