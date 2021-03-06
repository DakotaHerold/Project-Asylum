﻿using UnityEngine;
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
    private int DP; 

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

    void IdleDelayUpdate()
    {

        if (anim.GetFloat("IdleDelay") > 0)
            anim.SetFloat("IdleDelay", anim.GetFloat("IdleDelay") - 0.1f);
        else
		{ 
            anim.SetFloat("IdleDelay", Random.Range(5f, 20.0f));
            anim.Play("IdleAnimating");
        }
    }

    void FixedUpdate()
    {
        IdleDelayUpdate();
        if (ignoreInput)
			return;

        float moveX = Input.GetAxis("Horizontal");
        //Debug.Log(moveX);
        if (Mathf.Abs(moveX) < 0.5f)
            moveX = 0;
        float moveY = Input.GetAxis("Vertical");
        if(Mathf.Abs(moveY) < 0.5f)
            moveY = 0;
        if (!canMove)
        {
            anim.SetFloat("Speed", Mathf.Abs(0.0f));
            Move(0, 0);
            return;
        }
        else
        {
            // Update animation 
            anim.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));

            Move(moveX, moveY);
        }
    }

	public void Move(float moveX, float moveY)
	{
        if (!canMove)
        {
            anim.SetFloat("Speed", Mathf.Abs(0.0f));
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            anim.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * maxSpeed, moveY * maxSpeed);

            if (moveX > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveX < 0 && facingRight)
            {
                Flip();
            }
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
        if(inventory.Contains(obj))
        {
            inventory.Remove(obj);
        }
        
    }

    public bool HasItem(string item)
    {
        return inventory.Contains(item); 
    }

    public void IncrementDP(int num)
    {
        DP += num; 
    }

    public void DecrementDP(int num)
    {
        DP -= num;
    }
}
