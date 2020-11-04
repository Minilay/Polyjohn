using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rigid;
    public CircleCollider2D coll;
    public ParticleSystem particles;
    public ParticleSystem explosion;
    public bool cancel;
    bool inActive = false;
    
    void Start()
    {
        rigid = GetComponent < Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        cancel = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        print("Collided");
        rigid.velocity = Vector3.zero;
        coll.enabled = false;
        inActive = true;
        particles.Play();
       
    }

    private void OnDestroy()
    {
        if (cancel)
        {
            ParticleSystem exp = Instantiate<ParticleSystem>(explosion);
            exp.transform.position = transform.position;
            exp.Play();
            Destroy(exp, 0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!inActive)
        {
            if(Shooter.S.aimMode)
            {
                coll.enabled = false;
            }
            else
            {
                coll.enabled = true;
            }
        }
       
    }
}
