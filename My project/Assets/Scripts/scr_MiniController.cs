using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class scr_MiniController : MonoBehaviour
{
    [SerializeField]
    float maxspeed = 1f;
    [SerializeField]
    float currentVelX = 0f;
    [SerializeField]
    float currentVelY = 0f;
    [SerializeField]
    float acceleration = .1f;
    [SerializeField]
    float jumpStrength = 2f;
    [SerializeField]
    float gravStrength = .04f;
    private Rigidbody rb = null;
    private bool isOnGround = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get movement input
        if (SteamVR_Actions.default_WalkRight.state)
        {
            if (currentVelX < maxspeed)
            {
                currentVelX += acceleration;
            }
        }

        if (SteamVR_Actions.default_WalkLeft.state)
            if (currentVelX > maxspeed * -1)
            {
                currentVelX -= acceleration;
            }

        if (!SteamVR_Actions.default_WalkRight.state & !SteamVR_Actions.default_WalkLeft.state)
        {
            if (currentVelX > acceleration / 2)
            {
                currentVelX -= acceleration;
            }
            else if (currentVelX <= -acceleration / 2)
            {
                currentVelX += acceleration;
            }
            else
            {
                currentVelX = 0;
            }
        }

        //Check if player is on ground and act accordingly
        if (Physics.Raycast(transform.position, Vector2.down,.1f))
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.green, .1f);
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (isOnGround == false)
        {
            currentVelY -= gravStrength;
        }
        else if (SteamVR_Actions.default_Jump.state)
        {
            currentVelY = jumpStrength;
        }
        else
        {
            currentVelY = 0;
        }

        //move according to input
        rb.velocity = new Vector3(currentVelX, currentVelY, 0);
    }
}
