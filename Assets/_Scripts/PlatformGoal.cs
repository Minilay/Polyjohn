using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGoal : MonoBehaviour
{
   
    SpriteRenderer render;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            render.color = Color.green;
        }
    }

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
