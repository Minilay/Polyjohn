using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Inspector")]
    public Camera mainCam;
    public GameObject projectile;
    public GameObject kunai;
    public float shootPower;
    [Header("Dynamic")]
    public static Shooter S;
    public Vector3 mouseWorldPosition;
    public GameObject pr;
    public Rigidbody2D rigid;
    public bool knifeExist;
    public bool aimMode;



    private void Awake()
    {
        S = this;
        rigid = GetComponent<Rigidbody2D>();
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
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = transform.position + Vector3.up * 0.5f;

        if (Input.GetMouseButtonDown(0))
        {
            if (!knifeExist)
            {
                pr = Instantiate<GameObject>(projectile);
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
                pr.transform.position = playerPos + (mouseWorldPosition-playerPos).normalized*0.1f;
                Vector3 aimingDirection = mouseWorldPosition - playerPos;
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
                Destroy(pr);
            }
            
            knifeExist = !knifeExist;
        }

        if(Input.GetMouseButtonDown(1))
        {
            GameObject kn = Instantiate<GameObject>(kunai);
            InstantShoot(kn);
        }
    }
}
