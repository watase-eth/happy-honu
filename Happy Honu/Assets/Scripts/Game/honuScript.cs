using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class honuScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public logicScript logic;
    public bool honuIsAlive = true;
    // Start is called before the first frame update
    void Start()
    {
      logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && honuIsAlive) 
        {
          myRigidbody.velocity = Vector2.up * flapStrength;    
        }
        // if (transform.position.y > 17 || transform.position.y < -17 )  // off the screen
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     logic.gameOver();
     honuIsAlive = false;
    }
}
