using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemySpawnManager : MonoBehaviour
{
    public GameObject EnemyList = null;

    public bool ISIn = true;

    [SerializeField] AudioSource DogSound = null;

    UIManager m_UI = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float direction = other.transform.position.x - this.transform.position.x;
            if (ISIn)
            {
                if (direction < 0)
                {
                    AllReSpawnEnemy();
                    DogSound.Play();
                }

                else
                {
                    DogSound.Stop();
                }
            }
            else
            {
                if (direction > 0)
                {
                    AllReSpawnEnemy();
                    DogSound.Play();
                }

                else
                {
                    DogSound.Stop();
                }
            }
        }
    }

    void AllReSpawnEnemy()
    {
        if (!EnemyList.activeSelf)
        {
            EnemyList.SetActive(true);
        }
    }

    void Start()
    {
        DogSound = EnemyList.GetComponent<AudioSource>();

        DogSound.Stop();

        m_UI = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if(!m_UI.DogSound)
        {
            DogSound.volume = 0;
        }
        else
        {
            DogSound.volume = 1;
        }
    }
}
