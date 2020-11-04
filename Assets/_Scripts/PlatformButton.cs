using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public GameObject interactionObj;
    
    SpriteRenderer render;
    Color defaultColor;
    public float delay;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            render.color = Color.green;
            interactionObj.SetActive(false);
        }
    }
    

    void Disappear()
    {
        render.color = defaultColor;
        interactionObj.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Invoke("Disappear", delay);
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
