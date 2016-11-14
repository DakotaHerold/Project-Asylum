using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class CharacterController : MonoBehaviour
{
    //Attributes
    public float maxSpeed;
    public float minSpeed;
	public bool canMove = true;
	public bool ignoreInput = true;

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
		if(ignoreInput)
			return;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

		if (!canMove)
        {
            anim.SetFloat("Speed", Mathf.Abs(0.0f));
            return;
        }
        // Update animation 
        anim.SetFloat("Speed", Mathf.Abs(moveX + moveY));
        
        Move(moveX, moveY);
    }

	public void Move(float moveX, float moveY)
	{
        if (!canMove)
        {
            anim.SetFloat("Speed", Mathf.Abs(0.0f));
            return;
        }
        anim.SetFloat("Speed", Mathf.Abs(moveX + moveY));
        GetComponent<Rigidbody2D>().velocity=new Vector2(moveX * maxSpeed, moveY * maxSpeed);

		if(moveX > 0 && !facingRight)
		{
			Flip(); 
		}
		else if(moveX < 0 && facingRight)
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
