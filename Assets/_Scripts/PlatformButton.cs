using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public GameObject interactionObj;
    
    SpriteRenderer render;
    Color defaultColor;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            render.color = Color.green;
            interactionObj.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            render.color = defaultColor;
            interactionObj.SetActive(true);
        }
    }
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        defaultColor = render.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
