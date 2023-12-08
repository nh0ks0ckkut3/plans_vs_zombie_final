using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class footballZombie : MonoBehaviour
{

  public float speed;
  private float speedCool;
  private float speedStart;
  public int health;
  public int damage;
  public float eatCooldown;
  private bool isRun = true;
  private game_manager gameManager;
  private Vector3 vector3;
  private bool canEat = true;
  public Plant targetPlant;
  private Renderer ren;
  private Animator zombieAnimator;// Tham chiếu tới Animator của zombie
  private int MaxHealth;
  public GameObject ash;


  void Start()
  {
    MaxHealth = health;
    gameManager = game_manager.instance;
    zombieAnimator = GetComponent<Animator>();
    speedCool = 0.6f * speed;
    speedStart = speed;
    ren = GetComponent<Renderer>();
    //StartCoroutine(ApplyDamageOverTime());
      vector3 = Vector3.left;
  }

  // Update is called once per frame
  void Update()
  {

    if (isRun)
    {
      transform.Translate(vector3 * speed * Time.deltaTime);
      if ((float)health <= (MaxHealth / 2))
      {
        zombieAnimator.SetInteger("status", 1);
      }

      if ((float)health <= 0)
      {
        isRun = !isRun;
        gameManager.sound_die.Play();
        zombieAnimator.SetInteger("status", 10);
        Destroy(gameObject);
        if (gameManager.countToWin == 1)
        {
          gameManager.win();
        }
        else
        {
          if (gameManager.countToWin == 1)
          {
            gameManager.win();
          }
          else
          {
            gameManager.countToWin--;
          }
        }
      }
    }
    else
    {
      if ((float)health <= (MaxHealth / 2))
      {
        zombieAnimator.SetInteger("status", 5);
      }

      if ((float)health <= 0)
      {
        gameManager.sound_die.Play();
        zombieAnimator.SetInteger("status", 10);
        Destroy(gameObject);
        if (gameManager.countToWin == 1)
        {
          gameManager.win();
        }
        else
        {
          gameManager.countToWin--;
        }
      }
    }


  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("boom"))
    {
      health = 0;
      GameObject MyAsh = Instantiate(ash, gameObject.transform.position, Quaternion.identity);
      Destroy(MyAsh, 1f);
    }
    if (collision.CompareTag("plant"))
    {
      isRun = false;
      zombieAnimator.SetBool("Eat", true);
    }
    if (collision.CompareTag("redpoint"))
    {
      vector3 = new UnityEngine.Vector3(-1, -0.11f, 0);
    }
    if (collision.CompareTag("bullet"))
    {
      gameManager.sound_bullet.Play();
      Destroy(collision.gameObject);
      health--;
    }
    if (collision.CompareTag("bullet_cold"))
    {
      gameManager.sound_bullet.Play();
      Destroy(collision.gameObject);
      health--;
      Invoke("khoiphuctrangthai", 5f);
      speed = speedCool;
      ren.material.color = Color.blue;
    }
    if (collision.CompareTag("LawnMower"))
    {
      LawnMowers lawnMowersComponent = collision.gameObject.GetComponent<LawnMowers>();

      if (lawnMowersComponent == null)
      {
        // Nếu chưa tồn tại, thêm mới
        collision.gameObject.AddComponent<LawnMowers>();
        gameManager.sound_lawnMower.Play();

        // Sau đó, hủy GameObject hiện tại



      }
      health = 0;
    }

  }
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
    {
      targetPlant = collision.GetComponent<Plant>();
      Eat();
    }
    if (collision.CompareTag("LawnMower"))
    {
      targetPlant = collision.GetComponent<Plant>();
      Eat();
    }
  }
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
      isRun = true;
    zombieAnimator.SetBool("Eat", false);
  }
  void Eat()
  {



    // nếu đúng 1 trong 2 điều kiện thì hàm sẽ return không cho chạy hàm dưới
    if (!canEat || !targetPlant)
      return;
    gameManager.sound_eat_plant.Play();
    canEat = false;
    Invoke("ResetEatCooldown", eatCooldown);

    targetPlant.Hit(damage);
  }
  void ResetEatCooldown()
  {
    canEat = true;
  }
  void khoiphuctrangthai()
  {
    speed = speedStart;
    ren.material.color = Color.white;
  }
  //private IEnumerator ApplyDamageOverTime()
  //{
  //    while (true)
  //    {
  //        yield return new WaitForSeconds(1f); // Chờ 1 giây

  //        health -= damagePerSecond; // Trừ máu

  //    }
  //}
}
