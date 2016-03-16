using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public RadialMenuHelper radialMenuHelper;

    float deltaTime = 0f;

    bool isHolding = true;

    Vector2 startTouchPosition;
    Vector2 endTouchPosition;

    public float radialMenuDelay = 0.35f;
    float deltaRadialMenu = 0f;
    public float radialMenuTime = 0.25f;

    public float radialMenuPercentage
    {
        get { return deltaRadialMenu > radialMenuTime ? 1f : deltaRadialMenu / radialMenuTime; }
    }

    void Update()
    {
        if (radialMenuHelper == null)
            radialMenuHelper = FindObjectOfType<RadialMenuHelper>();

        // Touch inputs
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            if(touch.phase == TouchPhase.Began && !isHolding)
            {
                Debug.Log("touch begin");
                startTouchPosition = touch.position;
                isHolding = true;
            }

            if (touch.phase == TouchPhase.Stationary && isHolding)
            {
                deltaTime += Time.deltaTime;
                if (deltaTime > radialMenuDelay)
                {
                    deltaRadialMenu += Time.deltaTime;
                    radialMenuHelper.ShowRadialLoading(true);
                }
                radialMenuHelper.SetDeltaTime(radialMenuPercentage);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("touch ended");
                isHolding = false;
                deltaRadialMenu = 0f;
                deltaTime = 0f;
                radialMenuHelper.SetDeltaTime(deltaTime);
                radialMenuHelper.ShowRadialLoading(false);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse down");
            startTouchPosition = Input.mousePosition;
            isHolding = true;
        }

        if(Input.GetMouseButton(0) && isHolding)
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > radialMenuDelay)
            {
                deltaRadialMenu += Time.deltaTime;
                radialMenuHelper.ShowRadialLoading(true);
            }
            radialMenuHelper.SetDeltaTime(radialMenuPercentage);
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse up");
            isHolding = false;
            deltaRadialMenu = 0f;
            deltaTime = 0f;
            radialMenuHelper.SetDeltaTime(deltaTime);
            radialMenuHelper.ShowRadialLoading(false);
        }


    }
    
}
