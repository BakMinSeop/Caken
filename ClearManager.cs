using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    public float CountKey = 0f;
    public GameObject OpenGate = null;
    public GameObject CloseGate = null;

    int SceneNum;

    UIManager m_UI = null;

    AudioSource ClearSound = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (CountKey == 6f)
            {
                Debug.Log("게임클리어");
                m_UI.ShowGameClearBox();

                ClearSound.Play();

                m_UI.DogSound = false;
            }
            else
            {
                Debug.Log("모든 별을 획득해야 합니다. 현재 획득한 별 : " + CountKey * 0.5f + "개");
            }
        }
    }

    void Start ()
    {
        SceneNum = SceneManager.GetActiveScene().buildIndex;
        m_UI = FindObjectOfType<UIManager>();

        ClearSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        ChangeGate();
    }

    void ChangeGate()
    {
        if (CountKey == 6f)
        {
            CloseGate.SetActive(false);
            OpenGate.SetActive(true);

            if (SceneNum == 2)
            {
                PlayerPrefs.SetInt("1_1Clear", 5);
            }

            if (SceneNum == 3)
            {
                PlayerPrefs.SetInt("1_2Clear", 6);
            }

            if (SceneNum == 4)
            {
                PlayerPrefs.SetInt("1_3Clear", 7);
            }

            PlayerPrefs.Save();
        }
        else
        {
            CloseGate.SetActive(true);
            OpenGate.SetActive(false);
        }
    }
}
