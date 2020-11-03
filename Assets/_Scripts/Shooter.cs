using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Inspector")]
    public Camera mainCam;
    public GameObject vertex;
    public float shootPower;
    public List<Sprite> textures;
    public SpriteRenderer rend;
    public PolygonCollider coll;
    public ParticleSystem particlesIn;
    [Header("Dynamic")]
    public static Shooter S;
    public Vector3 mouseWorldPosition;
    public GameObject pr;
    public Rigidbody2D rigid;
    public bool knifeExist;
    public bool aimMode;

    public int ver = 3;

    private void Awake()
    {
        S = this;
        rigid = GetComponent<Rigidbody2D>();

        polygonShifting();//Initial sprite  
    }
   
    public void polygonShifting()
    {
        rend.sprite = textures[ver];

        coll.polygonCount = ver + 3;
    }
    void VertexShooting()
    { 

        if (Input.GetMouseButtonDown(0))
        {
            if (!knifeExist)
            {
                pr = Instantiate<GameObject>(vertex);
                aimMode = true;
                particlesIn.Play();
            }
        }
        if(Input.GetMouseButton(0))
        {
            if(!knifeExist)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;
                mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePos);
                pr.transform.position = transform.position;
                Vector3 aimingDirection = mouseWorldPosition - transform.position;
                float angle = Mathf.Atan2(aimingDirection.y, aimingDirection.x);
                pr.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(!knifeExist)
            {
                pr.GetComponent<Rigidbody2D>().AddForce((mouseWorldPosition-transform.position).normalized*shootPower,ForceMode2D.Impulse);
                aimMode = false;
                particlesIn.Stop();
            }
            else
            {
                transform.position = pr.transform.position;
                rigid.velocity = Vector3.zero;

                //Sprite changing
                ver--;
                polygonShifting();
                
                Destroy(pr);
            }   
            knifeExist = !knifeExist;
        }
    }
    void Update()
    {
        if(ver > 0)
        VertexShooting();
    }
}
