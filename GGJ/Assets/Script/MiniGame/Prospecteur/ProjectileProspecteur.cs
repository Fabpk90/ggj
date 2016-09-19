using UnityEngine;
using System.Collections;

public class ProjectileProspecteur : MonoBehaviour {

    public Sprite sprite;
    public Vector3 destination;

    private Sprite projectileSprite;

    private float startTime;
    public float velocity;

	// Use this for initialization
	void Start () {
        //play sound
        startTime = Time.time;

        gameObject.AddComponent<SpriteRenderer>().sprite = sprite;

        projectileSprite = ManagerProspecteur.spritesheet[1];
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        float step = (Time.time - startTime) / velocity;

        bool xTouched = false;
        if(pos.y < destination.y)// too much left
        {
            pos.y += Mathf.Lerp(destination.y, pos.y, step);
        }
        else if (pos.y > destination.y)// too much right
        {
            pos.y -= Mathf.Lerp(pos.y, destination.y, step);
        }
        else
        {
            xTouched = true;
        }

        if (pos.y < destination.y)// too much up
        {
            pos.y -= Mathf.Lerp(destination.y, pos.y, step);
        }
        else if (pos.y > destination.y)// too much down
        {
            pos.y += Mathf.Lerp(pos.y, destination.y, step);
        }
        else if(xTouched)
        {
            //destination reached, or at least it should
            DestroyImmediate(GetComponent<SpriteRenderer>());
            Destroy(this);
            
        }
             
        transform.position = pos;

        //transform.position = new Color(1f, 1f, 1f, Mathf.Lerp(GetComponent<SpriteRenderer>().color.a, 0.0f, step));

        Debug.Log(destination);
    }
}
