using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float distance;
    public float speed;
    public bool toRight = true;
    List<GameObject> attachedObjs;
    float init_pos;
    float moveX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Ground"))
        {
            attachedObjs.Add(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Ground"))
        {
            attachedObjs.Remove(collision.gameObject);
        }
    }
    void Start()
    {
        init_pos = transform.position.x;
        attachedObjs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
         moveX = Time.deltaTime * (toRight ? speed : -speed);

        transform.position += Vector3.right * moveX;
        
        toRight = Mathf.Abs(transform.position.x - init_pos) > distance ? !toRight : toRight;
        if(transform.position.x - init_pos > distance )
        {
            transform.position = new Vector3(init_pos + distance - 0.01f, transform.position.y, transform.position.z);
            toRight = false;
        }
        if(transform.position.x - init_pos < -distance )
        {
            transform.position = new Vector3(init_pos - distance + 0.01f, transform.position.y, transform.position.z);
            toRight = true;
        }
        
    }
    private void LateUpdate()
    {
        foreach (GameObject go in attachedObjs)
        {
            if (go != null)
                go.transform.position += Vector3.right * moveX;
        }
    }

}
