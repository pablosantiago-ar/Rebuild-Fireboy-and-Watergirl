using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cenarioInfinito : MonoBehaviour
{
    public float length;
    public float startPos;

    private Transform cam;

    public float ParallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;    
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

    }

}
