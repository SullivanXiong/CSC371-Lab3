using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            GameObject result = GameObject.Find("Result");
            UnityEngine.UI.Text textComponent =  result.GetComponent<UnityEngine.UI.Text>();
            textComponent.text = "You Lost :(";
        }
        else
        {
            for (int i = health; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
