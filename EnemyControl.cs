using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] GameObject PlayerOBJ = null;
    Vector3 PlayerPos = new Vector3();

    public GameObject EnemyOBJ = null;
    Vector3 EnemyPos = new Vector3();

    UIManager m_UI = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && (EnemyPos.y < PlayerPos.y))
        {
            EnemyDie();
        }

        if (other.tag == "Player" && EnemyPos.y > PlayerPos.y)
        {
            PlayerOBJ.SetActive(false);
            m_UI.ShowGameOverBox();
        }
    }

    void Start()
    {
        PlayerOBJ = GameObject.Find("PlayerPos");
        m_UI = FindObjectOfType<UIManager>();
    }
	
	void Update ()
    {
        EnemyPos = EnemyOBJ.transform.position;
        PlayerPos = PlayerOBJ.transform.position;
    }

    void EnemyDie()
    {
        gameObject.SetActive(false);
    }
}
