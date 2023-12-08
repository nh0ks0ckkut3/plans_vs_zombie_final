using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class selectlevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lock2, lock3, lock4;
    private GameObject[] arrayLock = new GameObject[3];
    void Start()
    {
      arrayLock[0] = lock2;
      arrayLock[1] = lock3;
      arrayLock[2] = lock4;
      
      for(int i = 2; i <= PlayerPrefs.GetInt("level", 1); i++)
    {
      arrayLock[i-2].SetActive(false);
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
