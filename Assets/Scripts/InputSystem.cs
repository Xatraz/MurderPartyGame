using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

/*#if UNITY_WEBGL
    private void Start()
    {
        WebGLInput.captureAllKeyboardInput(enabled);
    }
#endif*/
}

public interface IInputSystem
{
    float GetAxisHorizontal(string horizontalCtrl);
    float GetAxisVertical(string verticalCtrl);
    bool GetFire1(string fire1);
}

public class PCInput : IInputSystem
{
    public float GetAxisHorizontal(string horizontalCtrl)
    {
        return Input.GetAxisRaw(horizontalCtrl);
    }

    public float GetAxisVertical(string verticalCtrl)
    {
        return Input.GetAxisRaw(verticalCtrl);
    }

    public bool GetFire1(string fire1)
    {
        return Input.GetButtonDown(fire1);
    }
}


public class MobileInput : IInputSystem
{
    private Vector2 touchOrigin = -Vector2.one;
    private float inputHorizontal;
    private float inputVertical;

    void Update()
    {
        //touch input
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    inputHorizontal = x > 0 ? 1 : -1;
                }
                else
                {
                    inputVertical = y > 0 ? 1 : -1;
                }
            }
        }
    }

    public float GetAxisHorizontal(string horizontalCtrl)
    {
        return inputHorizontal;
    }

    public float GetAxisVertical(string verticalCtrl)
    {
        return inputVertical;
    }

    public bool GetFire1(string fire1)
    {
        return Input.GetButtonDown(fire1);
    }
}


public class WebInput : IInputSystem
{
    public float GetAxisHorizontal(string horizontalCtrl)
    {
        return Input.GetAxisRaw(horizontalCtrl);
    }

    public float GetAxisVertical(string verticalCtrl)
    {
        return Input.GetAxisRaw(verticalCtrl);
    }

    public bool GetFire1(string fire1)
    {
        return Input.GetButtonDown(fire1);
    }

}