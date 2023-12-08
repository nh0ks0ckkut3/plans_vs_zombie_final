using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zombieSpawner : MonoBehaviour
{
  // Start is called before the first frame update
  public Transform[] spawnpoint;
  public GameObject[] zombies;
  private game_manager gameManager;
  private bool isStart;

  private int numberOfSpawns = 20;


  //public ZombieType[] zombieTypes;
  public void Start()
  {
    gameManager = game_manager.instance;
    //gameManager.countToWin = numberOfSpawns;
    // gọi hàm SpawnZombie sau khi chạy màn chơi 6s thì hàm SpawnZombie sẽ chạy và  tiếp tục 5s lại gọi lại
    InvokeRepeating("SpawnZombie", 2, 2);
  }
  public void Update()
  {
    isStart = gameManager.isStart;
  }
  void SpawnZombie()
  {
    
    if (isStart)
    {
      numberOfSpawns--;
      int rz = Random.Range(0, zombies.Length);
      int r;
      if (rz == 1 || rz == 2)
      {
        r = Random.Range(0, 1);
        r += 2;
      }
      else
      {
        r = Random.Range(0, spawnpoint.Length);
        if (r == 2)
        {
          r = 1;
        }
        else if (r == 3)
        {
          r = 4;
        }

      }

      // tạo ra 1 zombie bản sao với 1 vị trí ngẫu nhiên
      GameObject myZombie = Instantiate(zombies[rz], spawnpoint[r].position, Quaternion.identity);
      if (gameManager.isRoofMap)
      {
        Vector3 currentScale = myZombie.transform.localScale;

        // Tăng kích thước theo mức độ mong muốn
        myZombie.transform.localScale = new Vector3(currentScale.x * 1.5f, currentScale.y * 1.5f, currentScale.z * 1.5f);
      }
      //myZombie.GetComponent<Zombie>().type = zombieTypes[Random.Range(0,zombieTypes.Length)];
      if (numberOfSpawns <= 0)
      {
        // Hủy bỏ hẹn giờ lặp lại sau khi đạt đến số lần lặp lại mong muốn
        CancelInvoke("SpawnZombie");
      }
    }

  }
}
