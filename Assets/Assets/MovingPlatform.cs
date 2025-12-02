using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PointA; 
    public Transform PointB ;

    public float speed;
    private bool At0B ;

    private Vector3 targetPosition ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

void Update()
{
    //The platform should always start its journey from Point A to Point B.
    //Once At0B eventually becomes false, the platform will begin moving back towards Point A
    if (At0B == true)
    {
        targetPosition = PointA.position;
    }
    else
    {
        targetPosition = PointB.position;
    }

    //Move towards target position on Y-axis
    Vector3 newPosition = transform.position;
    newPosition.y = Mathf.MoveTowards(transform.position.y, targetPosition.y, speed * Time.deltaTime);

    transform.position = newPosition;

    //Switch direction when closing in on target
    if (Mathf.Abs(transform.position.y - targetPosition.y) < 0.1f)
    {
        At0B = !At0B;     //if true, become false. If false, become true
    }
}
//To make the character sit still on the platform, we will make him a child as soon as he stands on it
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Player")
    {
        //Make the platform a parent
        collision.transform.SetParent(null);
    }
}

}
