using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    //Attributes
    public float maxSpeed;
    public float minSpeed;
    private Rigidbody2D body;
    private bool facingRight = true; 

    Animator anim;

    // Use this for initialization
    void Start () {
        //Gets the Character's Animator for its variables
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	//void Update () {
	
	//}

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        // Update animation 
        anim.SetFloat("Speed", Mathf.Abs(move));

        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

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
}
