using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class CharacterController : MonoBehaviour
{
    //Attributes
    public float maxSpeed;
    public float minSpeed;
    public bool canMove = true;

    public GameObject TextNotifier; 

    private Rigidbody2D body;
    private bool facingRight = true;

    private List<string> inventory;
    private NotifyText notification; 

    Animator anim;

    // Use this for initialization
    void Start ()
	{
		//Gets the Character's Animator for its variables
		anim=GetComponent<Animator>();

		inventory=new List<string>();
		if(TextNotifier!=null)
		{
			notification=TextNotifier.GetComponent<NotifyText>(); 
		}
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        // Update animation 
        anim.SetFloat("Speed", Mathf.Abs(move));
        if (!canMove)
        {
            return;
        }

        Move(move);
    }

	public void Move(float move)
	{
        if (!canMove)
        {
            return;
        }

        GetComponent<Rigidbody2D>().velocity=new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(move > 0 && !facingRight)
		{
			Flip(); 
		}
		else if(move < 0 && facingRight)
		{
			Flip(); 
		}
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale; 
    }

    public void AddToInventory(string obj)
    {       
        if (notification != null)
        {
            
            notification.StartCoroutine(notification.UpdateText(obj + " added to inventory")); 
        }
        inventory.Add(obj);
    }

    public void RemoveFromInventory(string obj)
    {
        if (notification != null)
        {
            notification.StartCoroutine(notification.UpdateText(obj + " removed from inventory"));
        }
        inventory.Add(obj);
    }
}
