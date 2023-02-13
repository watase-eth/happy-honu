using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    public bool autoScroll = false;

    private float length, startpos;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float distance = (cam.transform.position.x * parallaxEffect);

        float desiredXPos = startpos + distance;

        if (autoScroll)
        {
            // this will push bg to the left
            desiredXPos = transform.position.x - parallaxEffect;
        }

        transform.position = new Vector2(desiredXPos, transform.position.y);


        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp > startpos - length)
        {
            startpos -= length;
        }
    }
}
