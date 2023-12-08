using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DolphineRiderZombie : MonoBehaviour
{
  public float speed;
  private float speedCool;
  private float speedStart;
  private Renderer ren;
  public int health;
  public int damage;
  public float eatCooldown;
  private bool isRun = true;
  private bool FirstPlant = true; // theo logic zombie này sẽ bỏ qua plant đầu thay đổi speed, trở thành zombie thường
  private game_manager gameManager;
  private UnityEngine.Vector3 vector3;
  private bool canEat = true;
  public Plant targetPlant;
  private Animator zombieAnimator;// Tham chiếu tới Animator của zombie
  public GameObject ash;

  void Start()
  {
    gameManager = game_manager.instance;
    zombieAnimator = GetComponent<Animator>();
    ren = GetComponent<Renderer>();
    speedCool = 0.6f * speed;
    speedStart = speed;
    vector3 = UnityEngine.Vector3.left;

  }

  // Update is called once per frame
  void Update()
  {

    if (isRun)
    {
      transform.Translate(vector3 * speed * Time.deltaTime);
      
      //zombieAnimator.SetInteger("status", 2);
      if (FirstPlant)
      {
        //zombieAnimator.SetInteger("status", 1);
        //zombieAnimator.SetInteger("status", 2);
      }
      else
      {
        //zombieAnimator.SetInteger("status", 3);
        //zombieAnimator.SetInteger("status", 4);
      }




    }
    if (health <= 0)
    {

      isRun = !isRun;
      gameManager.sound_die.Play();
      zombieAnimator.SetInteger("status", 10);
      
      if(gameManager.countToWin == 1)
      {
        gameManager.win();
      }
      else
      {
        gameManager.countToWin--;
      }
      Destroy(gameObject);
    }

  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
    {

      if (FirstPlant)
      {
        Collider2D collider1 = GetComponent<Collider2D>(); // Collider của đối tượng hiện tại
        Collider2D collider2 = collision; // Collider của đối tượng bạn muốn bỏ qua va chạm

        Physics2D.IgnoreCollision(collider1, collider2);
        jump();
      }
      else
      {
        isRun = false;
      }
    }
    if (collision.CompareTag("redpoint"))
    {
      vector3 = new UnityEngine.Vector3(-1, -0.11f, 0);
    }if (collision.CompareTag("jumppoint"))
    {
      zombieAnimator.SetInteger("status", 1);
      Invoke("doneJump", 2.1f);
    }
    

    

    if (collision.CompareTag("bullet"))
    {
      gameManager.sound_bullet.Play();
      Destroy(collision.gameObject);
      health--;
    }
    if (collision.CompareTag("boom"))
    {
      health = 0;
      GameObject MyAsh = Instantiate(ash, gameObject.transform.position, Quaternion.identity);
      Destroy(MyAsh, 1f);
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

      }

      // Sau đó, hủy GameObject hiện tại
      health = 0;
    }
  }
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("plant") && FirstPlant == false)
    {
      zombieAnimator.SetBool("Eat", true);
      targetPlant = collision.GetComponent<Plant>();
      Eat();
    }
  }
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
    {
      isRun = true;
      zombieAnimator.SetBool("Eat", false);
    }

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
  void jump()
  {

    // code nhảy

    //animation
    zombieAnimator.SetInteger("status", 3);
    Invoke("DoSomethingElse", 0.5f);


  }
  void DoSomethingElse()
  {
    // Hành động tiếp theo ở đây
    zombieAnimator.SetInteger("status", 4);
    speed = speed - 0.5f;
    FirstPlant = false;
  }
  void khoiphuctrangthai()
  {
    speed = speedStart;
    ren.material.color = Color.white;
  }
  void doneJump()
  {
    Debug.Log("jump");
    zombieAnimator.SetInteger("status", 2);
  }
}
