using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   

    public void LoadMainGameScene()
    {
       
        SceneManager.LoadScene("MainGame");
        
        //GameManager.Instance.TimeOnGoing();
    }
          
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
