using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMiddleScript : MonoBehaviour
{
    public logicScript logic;
    // Start is called before the first frame update
    void Start()
    {
      // this is tricky to remember. but it looks for a game object tagged "Logic", you had to make the Tag in Unity 
       logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.layer == 3)
      {
      logic.addScore(1);
      }

    }
}
