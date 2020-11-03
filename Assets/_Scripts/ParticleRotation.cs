using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed;
    public float angle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;
        angle %= 360;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
