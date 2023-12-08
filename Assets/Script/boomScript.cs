using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomScript : MonoBehaviour
{
  // Start is called before the first frame update
  public float timeDie, timeSound;
  public bool isPepper;
  public GameObject fire;
  private game_manager gameManager;
  float y;
    void Start()
    {
    gameManager = game_manager.instance;
    y = gameObject.transform.position.y;
    Destroy(gameObject, timeDie);
    Invoke("sound", timeSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  void sound()
  {
    gameManager.sound_cherryboom.Play();
    if(isPepper)
    {
      GameObject myZombie = Instantiate(fire, new Vector3(0f, y, 0f), Quaternion.identity);
      Destroy(myZombie, timeDie);
    }
  }
}
