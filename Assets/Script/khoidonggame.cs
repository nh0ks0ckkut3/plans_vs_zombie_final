using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class khoidonggame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    PlayerPrefs.DeleteAll();
    Destroy(gameObject);
    }
}
