using UnityEngine;
using System.Collections;

public class PlayerProsp : MonoBehaviour {

    public Camera mainCamera;
    public ProjectileProspecteur projectileProps;
    public GameObject spawnPoint;
    public float coolDown;

    private float timeBeforeShot;

    /**
    * x lessthan 0 bottom x more = top
    * z lessthan 0 left z more right
    **/
    public Vector3 minValues;
    public Vector3 maxValues;

    public float moveSpeed;

	// Use this for initialization
	void Start () {
        Vector3 pos = Input.mousePosition;

        pos = mainCamera.ScreenToWorldPoint(pos);

        Debug.Log(pos.z - transform.position.z);

        //pos.z = transform.position.z;

        //transform.localPosition = mainCamera.ScreenToWorldPoint(pos);

        gameObject.AddComponent<SpriteRenderer>().sprite = ManagerProspecteur.spritesheet[0];
        //GetComponent<SpriteRenderer>().

        timeBeforeShot = coolDown;
    }
	
	// Update is called once per frame
	void Update () {

        timeBeforeShot -= Time.deltaTime;

        if (Input.GetAxis("Mouse X") < 0)
        {

            Vector3 pos = transform.position;
            pos.x -= ((moveSpeed * 2) * Time.deltaTime);

            if (checkPosition(pos, 0))
                transform.position = pos;


            //Code for action on mouse moving left
            //print("Mouse moved left");
        }
        else if (Input.GetAxis("Mouse X") > 0)
        {

            Vector3 pos = transform.position;
            pos.x += ((moveSpeed * 2) * Time.deltaTime);

            if (checkPosition(pos, 1))
                transform.position = pos;
           

            //Code for action on mouse moving right
             //print("Mouse moved right");
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            

            Vector3 pos = transform.position;
            pos.z -= ((moveSpeed * 2) * Time.deltaTime);

            if (checkPosition(pos, 2))
                transform.position = pos;

            //Code for action on mouse moving down
            //print("Mouse moved down");
        }
        else if (Input.GetAxis("Mouse Y") > 0)
        {

            Vector3 pos = transform.position;
            pos.z += ((moveSpeed * 2) * Time.deltaTime);

            if (checkPosition(pos ,3))
                transform.position = pos;

            //Code for action on mouse moving up
            //print("Mouse moved up");
        }

        //Debug.Log(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && timeBeforeShot <= 0)
        {
            timeBeforeShot = coolDown;

            ProjectileProspecteur projectile = (ProjectileProspecteur)Instantiate(projectileProps, spawnPoint.transform.position, Quaternion.Euler(90, 90, 0));
            projectile.destination = transform.position;
            Debug.Log(projectile.destination);
        }
    }

    /**
    *
    * direction : 0 left 1 right, 2down 3 top
    *
    */
    private bool checkPosition(Vector3 pos, int direction)
    {
        switch(direction)
        {
            case 0:
                if(transform.position.x - pos.x > minValues.x)
                {
                    return true;
                }
                return false;

            case 1:
                if(transform.position.x + pos.x < maxValues.x)
                {
                    return true;
                }

                return false;

            case 2:
                if(transform.position.y - pos.y > minValues.y)
                {
                    return true;
                }

                return false;

            case 3:
                if (transform.position.y + pos.y < maxValues.y)
                {
                    return true;
                }

                return false;
        }

        return false;
    }
}
