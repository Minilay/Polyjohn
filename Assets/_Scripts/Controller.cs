using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update    
    [Header("Inspector")]
//    public Animator anim;
    public float speed;
    public float jumpPower;
    public Rigidbody2D rigid;
    public float maxAngVelo;
    [Header("Dynamic")]
    public float velo;
    public float angVelo;
    public bool Hit;
    public bool Shield;
    public int onGround;
    public bool jumped;
    public Vector2 dir;
    public List<GameObject> touchingObjs;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchingObjs.Add(collision.gameObject);
        //if(collision.CompareTag("Ground"))
        //{
        //    onGround++;
        //}
        onGround++;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touchingObjs.Remove(collision.gameObject);
        //if(collision.CompareTag("Ground"))
        //{
        //    onGround--;
        //}
        onGround--;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velo = -Input.GetAxisRaw("Horizontal") * speed;
        dir = new Vector2(velo * Time.deltaTime, transform.position.y);


        //rigid.velocity = new Vector2(velo * Time.deltaTime, rigid.velocity.y);
        rigid.AddTorque(velo);
        angVelo = rigid.angularVelocity;
      
        if (angVelo > maxAngVelo)
        {
            rigid.angularVelocity = maxAngVelo;
        }
        if (angVelo < -maxAngVelo)
        {
            rigid.angularVelocity = -maxAngVelo;
        }
        //rigid.angularVelocity = velo;
        transform.localScale = new Vector2(velo >= 0 ? 1 : -1, 1);
        //anim.SetFloat("Speed", Mathf.Abs(velo));


    }
    private void Update()
    {
        Hit = Input.GetMouseButton(0);
        //anim.SetBool("Hit", Hit);

        Shield = Input.GetMouseButton(1);
        //anim.SetBool("Shield", Shield);

        if(Input.GetButtonDown("Jump") && onGround > 0)
        {
            
            rigid.AddForce(Vector2.up * jumpPower);
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }


    }
}
