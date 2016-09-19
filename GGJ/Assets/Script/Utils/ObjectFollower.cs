using UnityEngine;
using System.Collections;

public class ObjectFollower : MonoBehaviour {

    public GameObject ObjectToFollow = null;

    public int DistanceXFromObject = 0;
    public int DistanceYFromObject = 0;
    public int DistanceZFromObject = 0;

    //used for storing the distance from the object
    Vector3 newPos = new Vector3();

    // Use this for initialization
    void Start () {

        if (ObjectToFollow == null)
            Debug.LogError("Set a proper object to follow!");

        transform.position = ObjectToFollow.transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        if (DistanceXFromObject > 0)
            newPos.x = ObjectToFollow.transform.position.x + DistanceXFromObject;
        else if(DistanceXFromObject < 0)
            newPos.x = ObjectToFollow.transform.position.x - DistanceXFromObject;
        else
            newPos.x = ObjectToFollow.transform.position.x;

        if (DistanceYFromObject > 0)
            newPos.y = ObjectToFollow.transform.position.y + DistanceYFromObject;
        else if (DistanceYFromObject < 0)
            newPos.y = ObjectToFollow.transform.position.y - DistanceYFromObject;
        else
            newPos.y = ObjectToFollow.transform.position.y;

        if (DistanceZFromObject > 0)
            newPos.z = ObjectToFollow.transform.position.z + DistanceZFromObject;
        else if (DistanceZFromObject < 0)
            newPos.z = ObjectToFollow.transform.position.z - DistanceZFromObject;
        else
            newPos.z = ObjectToFollow.transform.position.z;

        Debug.Log(newPos.ToString());

        transform.position = newPos;

    }
}
