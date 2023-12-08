using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShowShop : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject pnShop;
    void Start()
    {
        if (PlayerPrefs.GetInt("isShowShop", 0) == 1)
          {
            pnShop.SetActive(true);
          }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turnOff()
    {
      PlayerPrefs.SetInt("isShowShop", 0);
    }
}
