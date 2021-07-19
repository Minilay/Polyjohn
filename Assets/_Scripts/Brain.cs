using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    //Modifier Access
    [Header("Inspector")]
    public List<GameObject> pois;
    public int speed;
    public float maxAngVelo;
    [Header("Projectile")]
    public ParticleSystem pts;
    [Header("Dynamic")]
    public int currentPoi;
    private Rigidbody2D rigid;
    public GameObject proj;
    public Vector3 aimingDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("poi")) //String Check is very bad
        {
            currentPoi++;
            currentPoi %= pois.Count;
        }

        if(collision.CompareTag("projectile")) //String Check is very bad
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.transform.position = transform.position;
            proj = collision.gameObject;
           
        }
    }
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();   
    }

   private void Update()
    {
        var poi = pois[currentPoi];
        
        if(transform.position.x < poi.transform.position.x)
        {
            rigid.AddTorque(-speed);
        }
        else
        {
            rigid.AddTorque(speed);

        }
        
        var angVelo = rigid.angularVelocity;
        
        if (angVelo > maxAngVelo)
        {
            rigid.angularVelocity = maxAngVelo;
        }
        if (angVelo < -maxAngVelo)
        {
            rigid.angularVelocity = -maxAngVelo;
        }

        if(proj != null)
        {
            proj.transform.position = transform.position;
        }
    }
}
