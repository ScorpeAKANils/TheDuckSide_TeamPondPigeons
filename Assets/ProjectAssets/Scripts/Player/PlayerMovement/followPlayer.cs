using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform Player;
    [SerializeField] Vector3 Offset;
    float baseFOV; 
    Camera mCamera;
    float interpolate = 0f; 
    float maxFov = 90f;
    bool isHigh = false;
    private void Awake()
    {
        mCamera = Camera.main;
        baseFOV = mCamera.fieldOfView;
   
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.position.x + Offset.x, Player.position.y + Offset.y, Player.position.z + Offset.z);
 
        if (Player.position.y >= 13.28f)
        {
            if (!isHigh | interpolate > 0 && interpolate != 1f)
            {
                
                mCamera.fieldOfView = Mathf.Lerp(baseFOV, maxFov, interpolate);
                interpolate += 0.5f * Time.deltaTime;
                if (interpolate >= 1f)
                {
                    isHigh = true;
                    interpolate = 0.0f;
                }
            }
           
        }

        if (Player.position.y < 8f)
        {
            if (isHigh | interpolate > 0 && interpolate != 1f)
            {
        
                mCamera.fieldOfView = Mathf.Lerp(maxFov, baseFOV, interpolate);
                interpolate += 0.5f * Time.deltaTime;
                if (interpolate >= 1f)
                {
                    isHigh = false;
                    interpolate = 0.0f;
                }
            }
          
        }

   
    }
}
