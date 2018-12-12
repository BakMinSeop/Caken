using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //2D모드용
    public Camera ChangeView = null;
    GameObject PlayerObj;

    //3D모드용
    public Transform Target = null;
    public float Floating = 10.0f;
    public float Height = 5.0f;

    Transform CameraView;

    Camera m_UseCamera = null;

    public float LerpVel = 0.2f;

    PlayerControl KeyOff = null;

    bool ChangeCamera = false;

    void Start()
    {
        m_UseCamera = Camera.main;
        ChangeView = GetComponent<Camera>();
        PlayerObj = GameObject.Find("PlayerPos");

        CameraView = GetComponent<Transform>();
        KeyOff = FindObjectOfType<PlayerControl>();
    }

    void Update()
    {
        if (KeyOff.InterdictOverlap)
        {
            if (Input.GetKeyDown(KeyCode.B) || ChangeCamera)
            {
                ChangeView.orthographic = !ChangeView.orthographic;
                ChangeCamera = false;
            }
        }

        //FollowCamera();
    }

    private void FixedUpdate()
    {
        FollowCamera();
    }

    private void LateUpdate()
    {
        //FollowCamera();
    }



    void FollowCamera()
    {
       
        if (ChangeView.orthographic)
        {
            Vector3 targetpos = PlayerObj.transform.position - (Vector3.forward * 5) + (Vector3.up * 2.5f);
            m_UseCamera.transform.position = Vector3.Lerp(m_UseCamera.transform.position, targetpos, LerpVel);

            CameraView.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Quaternion rot = Quaternion.Euler(0, 90, 0);
            Vector3 targetpos = Target.position - (rot * Vector3.forward * Floating) + (Vector3.up * Height);
            CameraView.position = Vector3.Lerp(CameraView.position, targetpos, LerpVel);

            CameraView.LookAt(Target);
        }
       
    }

    public void ChangeCameraClick()
    {
        ChangeCamera = true;
    }
}
