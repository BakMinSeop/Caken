using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrigger : MonoBehaviour
{
    public GameObject TriggerOBJ = null;

    Vector3 RotationAngle;
    [SerializeField] float RotationSpeed = 10f;

    CameraControl m_CameraCtr = null;

    [SerializeField] ClearManager[] m_CountKey = null;

    public bool Offstar = false;

    [SerializeField] Animator PlayStar = null;

    AudioSource RiseSound = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayStar.enabled = true;
            RiseSound.Play();
            RotationSpeed = 50f;

            m_CountKey[0].CountKey += 1f;
            m_CountKey[1].CountKey += 1f;
        }
    }

    void Start ()
    {
        RotationAngle = new Vector3(0, 0, 10);
        m_CameraCtr = FindObjectOfType<CameraControl>();
        m_CountKey = FindObjectsOfType<ClearManager>();

        PlayStar = GetComponent<Animator>();
        
        PlayStar.enabled = false;
        Offstar = false;

        RiseSound = GetComponent<AudioSource>();
	}

    void Update()
    {
        RotationStar();
        EnableStar(Offstar);
    }

    void RotationStar()
    {
        transform.Rotate(RotationAngle * RotationSpeed * Time.deltaTime);
    }

    void EnableStar(bool p_star)
    {
        if (p_star)
        {
            gameObject.SetActive(false);
        }
    }
}
