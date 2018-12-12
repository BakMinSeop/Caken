using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text MessageText = null;
    public Image MessageBox = null;
    public GameObject CreateIcon = null;

    [SerializeField] bool ISTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ISTrigger)
            {
                MessageText.gameObject.SetActive(true);
                MessageBox.gameObject.SetActive(true);
                if (CreateIcon)
                {
                    CreateIcon.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MessageText.gameObject.SetActive(false);
        MessageBox.gameObject.SetActive(false);

        ISTrigger = false;
    }

    void Start()
    {
        MessageText.gameObject.SetActive(false);
        MessageBox.gameObject.SetActive(false);
        if (CreateIcon)
        {
            CreateIcon.SetActive(false);
        }
    }

    void Update()
    {

    }
}
