using UnityEngine;
using System.Collections;


// Code modified from Scott's answer here http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html 

public class SmoothFollow2D : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera followCamera;

    void Start()
    {

    }
	

    // Update is called once per frame
    void Update()
    {
    	if (target && followCamera)
        {
            Vector3 targetPos = target.position;
            //targetPos.y = targetPos.y - 0.6f; 

            Vector3 point = followCamera.WorldToViewportPoint(targetPos);
            Vector3 delta = targetPos - followCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }

	public void setTarget(Transform t)
	{
		this.target=t;
		print(this.target);
	}
}
