using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update    
    [Header("Inspector")]
    public Animator anim;
    public float speed;
    public Rigidbody2D rigid;

    [Header("Dynamic")]
    public float velo;
    public bool Hit;
    public bool Shield;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velo = Input.GetAxisRaw("Horizontal") * speed;
        rigid.velocity = Vector2.right * velo * Time.deltaTime;

        //rigid.velocity += Vector2.right * velo * Time.deltaTime;
        transform.localScale = new Vector2(velo>=0 ? 1: -1, 1);

        anim.SetFloat("Speed", Mathf.Abs(velo));
    }
    private void Update()
    {
        Hit = Input.GetMouseButton(0);
        anim.SetBool("Hit", Hit);

        Shield = Input.GetMouseButton(1);
        anim.SetBool("Shield", Shield);
    }
}
