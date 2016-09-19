using UnityEngine;
using System.Collections;
using Assets.Script.Trap;

public class PlayerTaupe : MonoBehaviour
{

    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("SpriteSheets/test/taupes")[3];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;

        //float y = pos.y;
        //pos.y = pos.x;
        //pos.x = y;

        pos.z += 5;

        transform.localPosition = mainCamera.ScreenToWorldPoint(pos);
        /*
        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            print("Mouse moved right");
        }

        if (Input.GetAxis("Mouse Y") < 0)
        {
            //Code for action on mouse moving down
            print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            //Code for action on mouse moving up
            print("Mouse moved right");
        }*/

        if (Input.GetMouseButton(0))
        {          
            RaycastHit vHit = new RaycastHit();
            Ray vRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(vRay, out vHit, 1000))
            {
                Debug.Log(vHit.collider.name);

                if (vHit.collider.tag == "Taupe")
                {
                    if(!vHit.collider.gameObject.GetComponent<Taupe>().isBeingDestroy)
                        vHit.collider.gameObject.GetComponent<Taupe>().hitted();
                    Debug.Log("taupe");
                }
            }
        }
    }


}
