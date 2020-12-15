using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PolyJohn: MonoBehaviour
{
    [Header("Inspector")]
    public Camera mainCam;
    public GameObject vertex;
    public float shootPower;
    public SpriteRenderer rend;
    public PolygonCollider coll;
    public ParticleSystem particlesIn;
    public ParticleSystem deathParticles;
    public Animator anim;
    public List<Sprite> textures;

    [Header("Dynamic")]
    public Vector3 mouseWorldPosition;
    public GameObject pr;
    public Rigidbody2D rigid;
    public bool knifeExist;
    public static PolyJohn S;

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
                //pr.GetComponent<Rigidbody2D>().AddForce((mouseWorldPosition-transform.position).normalized*shootPower,ForceMode2D.Impulse);
                pr.GetComponent<Rigidbody2D>().velocity = (mouseWorldPosition - transform.position).normalized * shootPower; 

                particlesIn.Stop();
            }
            else
            {
                anim.SetBool("In", true);
            }   
            knifeExist = !knifeExist;
        }

        
    }

    
    void Update()
    {
        if(ver > 0)
            VertexShooting();

        if(anim.GetBool("In") == true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PolyJohn_in"))
            {
                transform.position = pr.transform.position;
                rigid.velocity = Vector3.zero;

                //Sprite changing
                ver--;
                polygonShifting();

                Destroy(pr);
                anim.SetBool("In", false);
            }
        }
        if (Input.GetMouseButtonDown(1) && knifeExist)
        {
            knifeExist = !knifeExist;
            pr.GetComponent<Projectile>().cancel = true; //Starts Destroy animation
            Destroy(pr);
        }
        if (Input.GetKey(KeyCode.Tab) && pr != null)
        {
            mainCam.GetComponent<CamFollower>().poi = pr;
        }
        else
        {
            mainCam.GetComponent<CamFollower>().poi = this.gameObject;
        }
    }

    public void Ded()
    {
        coll.enabled = false;
        ParticleSystem pts = Instantiate<ParticleSystem>(deathParticles);
        pts.transform.position = transform.position;
        pts.Play();
        Destroy(pts, 2);
        Destroy(gameObject);
        //loadScene();
        print("You ded");
        GameManager.S.ReloadScene(true);
    }
    void loadS()
    {
        SceneManager.LoadScene("Scene 0");
    }
}
