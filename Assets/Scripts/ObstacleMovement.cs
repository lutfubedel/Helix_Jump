using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleMovement : MonoBehaviour
{
    public GameObject player;
    public float move_speed;
    
    public int firstTouch;

    Touch touch;


    void Update()
    {
        if (firstTouch == 1)
        {
            player.GetComponent<Rigidbody>().useGravity = true;
        }


        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    firstTouch = 1;
                }

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    transform.Rotate(transform.rotation.x,touch.deltaPosition.x * move_speed * Time.deltaTime, transform.rotation.z);
                }

            }

            else if (touch.phase == TouchPhase.Ended)
            {
                
            }

        }
    }

}
