using UnityEngine;
using System.Collections;

public class GenerateStreet: MonoBehaviour
{
    public GameObject prefab; 
    private BoxCollider2D trigger; 

    public float width;
    public float distanceBetween; 

	// Use this for initialization
	void Start ()
	{
        trigger = GetComponent<BoxCollider2D>();
        trigger.enabled = true;
        //width = GetComponentInChildren<BoxCollider2D>().size.x;
    }
	
	// Update is called once per frame
	void Update ()
	{
        //Debug.Log(width);
	}

	public bool didTrigger()
	{
		return !trigger.enabled;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //transform.position = new Vector3(transform.position.x + width * 2, transform.position.y, transform.position.z);
        Vector3 newPos = transform.position;
        newPos.x += width + distanceBetween;  
        GameObject newStreet = Instantiate(prefab, newPos , transform.rotation) as GameObject;
		GenerateStreet genstreet=newStreet.GetComponentsInChildren<GenerateStreet>()[0];
		genstreet.prefab = prefab;
		genstreet.width = width;
		genstreet.distanceBetween = distanceBetween;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        trigger.enabled = false; 
    }
}
