using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    float DirectionCTR = 1;
    public float MoveRange = 5f;
    Vector3 EnemyOBJ = new Vector3(0, -0.8f, 0);

    [SerializeField] CameraControl m_ChangeCamera;

    ChangePositionZ m_TempPosZ = null;


    void Start ()
    {
        DirectionCTR = 1;
        if(DirectionCTR == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        m_ChangeCamera = FindObjectOfType<CameraControl>();
        m_TempPosZ = FindObjectOfType<ChangePositionZ>();
    }

    void Update ()
    {
        EnemyMoving();
    }

    void EnemyMoving()
    {
        if (EnemyOBJ.x <= -MoveRange)
        {
            DirectionCTR = 1;

            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (EnemyOBJ.x >= MoveRange)
        {
            DirectionCTR = -1;

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        EnemyOBJ.x += 5.0f * Time.deltaTime * DirectionCTR;
        EnemyOBJ.z = transform.localPosition.z;
        transform.localPosition = EnemyOBJ;
    }
}
