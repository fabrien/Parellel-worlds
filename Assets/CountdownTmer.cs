using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTmer : MonoBehaviour
{
    private bool soundPlayed = false;
    public float timeStart = 30;
    public Text textBox;
    public AudioClip audioclip;
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
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            timeStart = 0;
        }
        if (timeStart <= 0.5 && !soundPlayed)
        {
            AudioSource.PlayClipAtPoint(audioclip, new Vector3(0, 0, 0));
            soundPlayed = true;
        }
    }
    
}

