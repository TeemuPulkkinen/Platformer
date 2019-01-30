using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //speed of the enemy
    public float speed = 3f;
    // range of movement Y
    public float rangeY = 2;
    //initial position
    Vector3 initialPos;
    // direction
    int direction = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        //save initial position
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //ternary operator means that if direction == 1, then factor is 2f, otherwise it is 1
        float factor = direction == -1 ? 2f : 1;
        //how much are we moving?
        //Time.deltatime, speed
        float movementY = factor * speed * Time.deltaTime * direction;

        // new position Y
        float newY = transform.position.y + movementY;
       
        // checking whether we've left our range
        if (Mathf.Abs(newY - initialPos.y) > rangeY)
        {
            direction *= -1;
            /* increase speed if going down
            if (transform.position.y > rangeY)
            {
                speed = speed * 1.2f;
            }*/
        }
        else
        {
            transform.position += new Vector3(0, movementY, 0);
        }
    }
}
