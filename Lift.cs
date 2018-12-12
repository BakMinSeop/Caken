using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public GameObject LiftOBJ = null;
    public bool Chack_Y = false;
    public bool Chack_X = false;

    Vector3 LiftVector = new Vector3(0, 0, 4);

    public float MoveRange = 5f;
    public float MoveSpeed = 1f;
    float DirectionCTR = 1;

    public GameObject PlayerOBJ = null;
    PlayerControl PlayerCPNT = null;
    float SavePower;
    float SaveSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && PlayerCPNT.ChackLift)
        {
            PlayerOBJ.transform.SetParent(LiftOBJ.transform);

            if(DirectionCTR == 1)
            {
                PlayerCPNT._jumpPower += 5f;
            }
            else
            {
                PlayerCPNT._jumpPower = SavePower;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player" && PlayerCPNT.ChackLift)
        {
            PlayerOBJ.transform.parent = null;
            PlayerCPNT._jumpPower = SavePower;
        }
    }

    void Start()
    {
        DirectionCTR = 1;
        PlayerCPNT = FindObjectOfType<PlayerControl>();
        SavePower = PlayerCPNT._jumpPower;
        SaveSpeed = MoveSpeed;
    }

    void Update()
    {
        RepeatLift();
    }

    void RepeatLift()
    {
        if (Chack_Y)
        {
            if (LiftVector.y <= -0.8f)
            {
                DirectionCTR = 1;
            }
            else if (LiftVector.y >= MoveRange)
            {
                DirectionCTR = -1;
            }

            LiftVector.y += MoveSpeed * Time.deltaTime * DirectionCTR;
            transform.localPosition = LiftVector;
        }

        if (Chack_X)
        {
            if (LiftVector.z <= 0)
            {
                DirectionCTR = 1;
            }
            else if (LiftVector.z >= MoveRange)
            {
                DirectionCTR = -1;
            }

            LiftVector.z += MoveSpeed * Time.deltaTime * DirectionCTR;
            transform.localPosition = LiftVector;
        }
    }
}
