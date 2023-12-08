using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShowSelectLV : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject pannelSelectLV;
    void Start()
    {
    if (PlayerPrefs.GetInt("isshowSelectLV", 0) == 1)
    {
      pannelSelectLV.SetActive(true);
      Invoke("turnOff", 3f);
    }
  }
  public void turnOff()
  {
    PlayerPrefs.SetInt("isshowSelectLV", 0);
  }
}
