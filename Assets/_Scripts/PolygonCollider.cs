using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PolygonCollider : MonoBehaviour
{
    // Start is called before the first frame update

    public EdgeCollider2D edge;
    public int polygonCount;
    public float length;
    public bool click = false;

    void polygonMaker()
    {
        if (polygonCount < 3 || length <= 0)
            return;
        List<Vector2> vert = new List<Vector2>();
        float angle = 360 / polygonCount;
        float sum = 0;
        for(int i = 0; i < polygonCount; i++)
        {

            float   x = Mathf.Cos((sum - 90 + angle / 2) * Mathf.Deg2Rad) * length,
                    y = Mathf.Sin((sum - 90 + angle / 2) * Mathf.Deg2Rad) * length;
            vert.Add(new Vector2(x, y));

            sum += angle;
        }

        vert.Add(vert[0]);
        edge.points = vert.ToArray();

    }
    void Start()
    {
        polygonMaker();
    }

    // Update is called once per frame
    void Update()
    {

        polygonMaker();
        
    }
}
