using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTmer : MonoBehaviour
{
    
    public float timeStart = 30;
    public Text textBox;
    public AudioSource audiosource;
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

     void Update()
    {
        timeStart -= Time.deltaTime;
        textBox.text = Mathf.Round(timeStart).ToString();
        if(timeStart <= 10)
        {
            textBox.color = Color.red;
        }
        if (timeStart <= 0)
        {
            audiosource.Play;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            timeStart = 0;
        }
    }
    
}

