using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] bool isjump = false;
    [SerializeField] bool ismove = false;

    [SerializeField] CameraControl m_ChangeCamera;

    Rigidbody m_LinkRigidBody = null;

    Vector3 m_OffsetMove = Vector3.zero;

    [SerializeField] Transform _transform;
    [SerializeField] bool _isJump;           //점프실행
    [SerializeField] bool _isJumping;        //점프입력가능 여부
    [SerializeField] float _gravity;         //중력가속도
    [SerializeField] float _jumpTime;        //점프 이후 경과시간
    [SerializeField] float _posY;            //오브젝트의 초기 높이
    public float _jumpPower;       //점프력
    float _savePower;

    [SerializeField] bool isWall = false;

    public PhysicMaterial Smoothmove = null;

    public GameObject TouchParticle = null;

    public AudioSource Meow = null;

    public bool BoxMove = false;

    public bool InterdictOverlap = false;

    public bool ChackLift = false;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Road" && collision.transform.tag == "Hidden"
            && collision.transform.position.y - 0.001f > transform.position.y
            && !isWall)
        {
            _isJumping = false;
        }
        else
        {
            _isJumping = true;
        }

        if (collision.transform.tag == "Lift")
        {
            _posY = _transform.position.y;
            //_jumpPower = 5.5f;
        }
        else
        {
            _jumpPower = _savePower;
        }

        if (collision.transform.tag == "Box")
        {
            BoxMove = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _posY = _transform.position.y;
        _jumpTime = 0.0f;
        _transform.position = new Vector3(_transform.position.x, _posY, _transform.position.z);
        _isJump = false;

        if (collision.transform.tag == "Enemy")
        {
            _isJump = true;
            Instantiate(TouchParticle, transform.position, transform.rotation);
        }

        if (collision.transform.tag == "Road" && collision.transform.tag == "Hidden"
            && collision.transform.position.y - 0.001f > transform.position.y
            && !isWall)
        {
            _isJumping = false;
        }
        else
        {
            _isJumping = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _isJumping = false;

        if (collision.transform.tag == "Box")
        {
            BoxMove = false;
        }
    }

    void Start()
    {
        m_ChangeCamera = FindObjectOfType<CameraControl>();
        m_LinkRigidBody = GetComponent<Rigidbody>();

        _transform = transform;
        _isJump = false;
        _isJumping = true;
        _posY = transform.position.y;
        _gravity = 10f;
        _jumpPower = 3.5f;
        _jumpTime = 0.0f;
        _savePower = _jumpPower;

        Meow.Stop();
    }

    void Update()
    {
        isjump = false;
        ismove = false;

        m_OffsetMove.Set(0, 0, 0);
        moveObject();

        if(ChackLift)
        {
            Vector3 temppos = transform.position;
            temppos += m_OffsetMove;
            this.transform.position = temppos;
        }
        else
        {
            Vector3 temppos = transform.position;
            temppos += m_OffsetMove;
            m_LinkRigidBody.MovePosition(temppos);
            //m_LinkRigidBody.transform.position = temppos;
        }
        
        CheckWall();

        if (_isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) || m_ISJumpEvent)
            {
                _isJump = true;
                Meow.Play();
                m_ISJumpEvent = false;
            }
        }

        if (_isJump)
        {
            Jump(2f);
        }

        GetComponent<Animator>().SetBool("Jump", isjump);
    }

    private void LateUpdate()
    {

    }



    float m_HorizontalVal = 0f;
    float m_VerticalVal = 0f;
    bool m_ISJumpEvent = false;

    public void SetPlayerMoveLeftClick( bool p_flag )
    {
        if(p_flag)
        {
            m_HorizontalVal = -1f;
        }
        else
        {
            m_HorizontalVal = 0f;
        }
        
    }

    public void SetPlayerMoveRightClick( bool p_flag )
    {
        if (p_flag)
        {
            m_HorizontalVal = 1f;
        }
        else
        {
            m_HorizontalVal = 0f;
        }
    }

    public void SetPlayerMoveForwardClick(bool p_flag)
    {
        if (p_flag)
        {
            m_VerticalVal = 1f;
        }
        else
        {
            m_VerticalVal = 0f;
        }
    }

    public void SetPlayerMoveBackClick(bool p_flag)
    {
        if (p_flag)
        {
            m_VerticalVal = -1f;
        }
        else
        {
            m_VerticalVal = 0f;
        }
    }

    public void SetPlayerJumpClick()
    {
         m_ISJumpEvent = true;
    }


    void moveObject()
    {
#if false //UNITY_EDITOR
        float keyHorizontal = Input.GetAxis("Horizontal");
        float keyVertical = Input.GetAxis("Vertical");
        bool ISAKey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool ISDKey = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool ISWKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool ISSKey = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
#else
        float keyHorizontal = m_HorizontalVal;
        float keyVertical = m_VerticalVal;
        bool ISAKey = false;
        bool ISDKey = false;
        bool ISWKey = false;
        bool ISSKey = false;
#endif
        if (m_ChangeCamera.ChangeView.orthographic)
        {
            if (keyHorizontal <= -1)
            {
                ISAKey = true;
            }
            else if (keyHorizontal >= 1)
            {
                ISDKey = true;
            }
        }
        else
        {
            if (keyHorizontal <= -1)
            {
                ISAKey = true;
            }
            else if (keyHorizontal >= 1)
            {
                ISDKey = true;
            }

            if (keyVertical <= -1)
            {
                ISSKey = true;
            }
            else if (keyVertical >= 1)
            {
                ISWKey = true;
            }
        }

        if (m_ChangeCamera.ChangeView.orthographic)
        {
            m_OffsetMove.x = speed * Time.deltaTime * keyHorizontal;

            Vector3 OrthographicPos = transform.position;
            OrthographicPos.z = -2f;
            transform.position = OrthographicPos;

            if (ISAKey)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            if (ISDKey)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if ((keyHorizontal != 0 || keyVertical != 0) && !isjump)
            {
                ismove = true;
            }

            GetComponent<Animator>().SetBool("Move", ismove);

            m_LinkRigidBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        }
        else
        {
            m_OffsetMove.x = speed * Time.deltaTime * keyVertical;
            m_OffsetMove.z = -speed * Time.deltaTime * keyHorizontal;

            if (ISSKey)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            if (ISWKey)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (ISAKey)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (ISDKey)
            {
                transform.rotation = Quaternion.Euler(0, -180, 0);
            }

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                || (ISAKey && ISSKey))
            {
                transform.rotation = Quaternion.Euler(0, -45, 0);
            }

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
                || (ISAKey && ISWKey))
            {
                transform.rotation = Quaternion.Euler(0, 45, 0);
            }

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
                || (ISDKey && ISSKey))
            {
                transform.rotation = Quaternion.Euler(0, -135, 0);
            }

            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                || (ISDKey && ISWKey))
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }

            if ((keyHorizontal != 0 || keyVertical != 0) && !isjump)
            {
                ismove = true;
            }

            GetComponent<Animator>().SetBool("Move", ismove);

            m_LinkRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Jump(float powerup)
    {
        float height = ((_jumpTime * _jumpTime * (-_gravity) / 2)
            + (_jumpTime * _jumpPower)) * powerup;

        Vector3 pos = transform.position;
        pos = new Vector3(pos.x + m_OffsetMove.x
            , _posY + height
            , pos.z + m_OffsetMove.z);

        if (ChackLift)
        {
            transform.position = pos;
        }
        else
        {
            m_LinkRigidBody.MovePosition(pos);
        }

        _jumpTime += Time.deltaTime;

        isjump = true;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 1.3f, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.right * 3f, Color.red);
        Debug.DrawLine(transform.position, transform.position + -transform.right * 3f, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.up * -0.3f, Color.red);
    }

    void CheckWall()
    {
        RaycastHit[] hit = new RaycastHit[4];

        if (Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), transform.forward, out hit[0], 1.3f)
            && Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, out hit[1], 0.3f))
        {
            isWall = true;
        }
        else
        {
            isWall = false;
        }

        if(Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, out hit[1], 0.3f))
        {
            if(hit[1].transform.tag == "Lift")
            {
                ChackLift = true;
            }
            else
            {
                ChackLift = false;
            }
        }

        if (Physics.Raycast(transform.position + (new Vector3(0f, 0.1f, 0f)), transform.right, out hit[2], 3f)
            || Physics.Raycast(transform.position + (new Vector3(0f, 0.1f, 0f)), -transform.right, out hit[3], 3f))
        {
            InterdictOverlap = false;

            for (int i = 2; i < 4; i++)
            {
                if (hit[i].transform != null
                    && hit[i].transform.tag == "Hidden")
                {
                    InterdictOverlap = true;
                }
            }
        }
        else
        {
            InterdictOverlap = true;
        }
    }
}