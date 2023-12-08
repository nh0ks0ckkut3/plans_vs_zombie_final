using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LawnMowers : MonoBehaviour
{
    
    private float speed = 2f;
    private game_manager gameManager;
    private Vector3 vector3;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = game_manager.instance;
        if (gameManager.isRoofMap)
        {
            vector3 = new Vector3(1, 0.11f, 0);
        }
        else
        {
            vector3 = Vector3.right;
        }
    }
    private void Update()
    {
        
            transform.Translate(vector3 * speed * Time.deltaTime);
            Destroy(gameObject, 10f);
        
        
    }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("redpoint"))
    {
      vector3 = Vector3.right;
    }
  }
        
    }

