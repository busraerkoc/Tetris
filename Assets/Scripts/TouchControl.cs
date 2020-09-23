using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{   public delegate void TouchEventHandler(Vector2 swipe);
    public static event TouchEventHandler SwipeEvent;
    public static event TouchEventHandler SwipeEndEvent;
    Vector2 touchMovement;
    int minSwipeDistance = 50;
    void OnSwipe()
    {
        if(SwipeEvent!=null)
        {
            SwipeEvent(touchMovement);
        }
    }

    void OnSwipeEnd()
    {
        if(SwipeEndEvent != null)
        {
            SwipeEndEvent(touchMovement);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began)
            {
                touchMovement = Vector2.zero;
            }
            else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                touchMovement += touch.deltaPosition;
                if(touchMovement.magnitude > minSwipeDistance)
                {
                    OnSwipe();
                }  
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                OnSwipeEnd();
            }
        }
        
    }
}
