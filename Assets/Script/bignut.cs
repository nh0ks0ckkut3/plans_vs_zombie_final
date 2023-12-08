using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bignut : MonoBehaviour
{
  public int health;
  private int MaxHealth;
  private Animator PlantAnimator;
  
  // Start is called before the first frame update
  void Start()
    {

    gameObject.layer = 10;
    MaxHealth = health;
   
  }

    // Update is called once per frame
    void Update()
    {
    if ((float)health <= (MaxHealth * 2 / 3))
    {
      PlantAnimator.SetInteger("status", 1);
    }
    if ((float)health <= (MaxHealth * 1/ 3))
    {
      PlantAnimator.SetInteger("status", 2);
    }
    if ((float)health <= 0)
    {
      Destroy(gameObject);
    }
  }
  public void Hit(int damage)
  {
    health -= damage;
    if (health <= 0)
      Destroy(gameObject);
  }
}
