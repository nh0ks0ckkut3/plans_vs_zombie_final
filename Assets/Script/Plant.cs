using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    // Start is called before the first frame update
    
  public int health;
  private int MaxHealth;
  private Animator PlantAnimator;
  private void Start()
    {
    PlantAnimator = GetComponent<Animator>();
    gameObject.layer = 10;
    MaxHealth = health;
    }
  void Update()
  {
    if ((float)health <= ((float)MaxHealth  * 2f/ 3f))
    {
      PlantAnimator.SetInteger("status", 1);
    }
    if ((float)health <= ((float)MaxHealth * 1f / 3f))
    {
      PlantAnimator.SetInteger("status", 2);
    }
    if ((float)health <= 0f)
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
