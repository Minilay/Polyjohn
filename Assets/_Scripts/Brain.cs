using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(collision.CompareTag("poi"))
        {
            currentPoi++;
            currentPoi %= pois.Count;
        }

        if(collision.CompareTag("projectile"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<CircleCollider2D>().enabled = false;
            collision.transform.position = transform.position;
            proj = collision.gameObject;
           
        }
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        GameObject poi = pois[currentPoi];
        if(transform.position.x < poi.transform.position.x)
        {
            rigid.AddTorque(-speed);
        }
        else
        {
            rigid.AddTorque(speed);

        }
        float angVelo = rigid.angularVelocity;
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
