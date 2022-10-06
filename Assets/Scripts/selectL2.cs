using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectL2 : MonoBehaviour
{
    public string LevelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "controller")
        {
            SceneManager.LoadScene(LevelName);
        }
    }     
}
