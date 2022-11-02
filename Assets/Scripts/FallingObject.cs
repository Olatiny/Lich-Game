using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField][Tooltip("the speed at which this object will move up the screen")]private float moveSpeed;
    private Renderer render;
    //private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(0, moveSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.fixedDeltaTime), transform.position.z);
        if(!render.isVisible){
            Destroy(gameObject);
        }
    }

    public void setMoveSpeed(float newMoveSpeed){
        moveSpeed = newMoveSpeed;
    }

    public float getMoveSpeed(){
        return moveSpeed;
    }
}
