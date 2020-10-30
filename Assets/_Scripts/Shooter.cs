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

    [Header("Dynamic")]
    public static Shooter S;
    public Vector3 mouseWorldPosition;
    public GameObject pr;
    public Rigidbody2D rigid;
    public bool knifeExist;
    public bool aimMode;

    public int cnt = 0;

    private void Awake()
    {
        S = this;
        rigid = GetComponent<Rigidbody2D>();

        rend.sprite = textures[0];//Initial sprite
    }
    void Start()
    {
        
    }

    void InstantShoot(GameObject go)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mouseWorldPosition = mainCam.ScreenToWorldPoint(mousePos);
        Vector3 playerPos = transform.position + Vector3.up * 0.5f;
        go.transform.position = playerPos + (mouseWorldPosition - transform.position).normalized*0.1f;
        Vector3 aimingDirection = mouseWorldPosition - playerPos;
        float angle = Mathf.Atan2(aimingDirection.y, aimingDirection.x);
        go.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
        go.GetComponent<Rigidbody2D>().AddForce((mouseWorldPosition - playerPos).normalized * shootPower * 2, ForceMode2D.Impulse);

    }
   
    public void polygonShifting()
    {
        rend.sprite = textures[cnt];

        coll.polygonCount = 6-cnt;
    }
    void VertexShooting()
    { 

        if (Input.GetMouseButtonDown(0))
        {
            if (!knifeExist)
            {
                pr = Instantiate<GameObject>(vertex);
                aimMode = true;
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
            }
            else
            {
                transform.position = pr.transform.position;
                rigid.velocity = Vector3.zero;

                //Sprite changing
                cnt++;
                polygonShifting();
                
                Destroy(pr);
            }   
            knifeExist = !knifeExist;
        }
    }
    void Update()
    {
        if(cnt < 3)
        VertexShooting();
    }
}
