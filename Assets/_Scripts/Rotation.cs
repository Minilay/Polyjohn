using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject poi;
    public float speed;
    public float radiusX;
    public float radiusY;
    public float phaseDeg;
    private float x, y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angX, angY;
        x += speed * Time.deltaTime;

        angX = Mathf.Cos((x+phaseDeg) * Mathf.Deg2Rad) * radiusX;
        angY = Mathf.Sin((x+phaseDeg) * Mathf.Deg2Rad) * radiusY;

        transform.localPosition = new Vector2(angX, angY);
    }
}
