using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private GameObject tailPrefab;

    private Vector2 moveDirection = Vector2.right;
    private List<Transform> tail = new List<Transform>();
    private bool ate = false;

    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject game;

    private bool isMovingForward = true;

    private void Start() => InvokeRepeating(nameof(Move), 0.3f, 0.3f);

    private void Update()
    {

        if (tail.Count <= 0)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection = Vector2.left;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection = Vector2.down;
            }
            return;
        }


        if (Input.GetKey(KeyCode.RightArrow) && moveDirection != Vector2.left && isMovingForward)
        {
            moveDirection = Vector2.right;
            isMovingForward = false;  // thay đổi trạng thái di chuyển
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && moveDirection != Vector2.right && isMovingForward)
        {
            moveDirection = Vector2.left;
            isMovingForward = false;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && moveDirection != Vector2.down && isMovingForward)
        {
            moveDirection = Vector2.up;
            isMovingForward = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && moveDirection != Vector2.up && isMovingForward)
        {
            moveDirection = Vector2.down;
            isMovingForward = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            ate = true;
            Destroy(collision.gameObject);
        }
        else
        {
            GameObject[] snake = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < snake.Length; i++)
            {
                Destroy(snake[i]);
            }
            Time.timeScale = 0;
        }
    }

    private void Move()
    {
        Vector2 currentPosition = transform.position;

        if (currentPosition.x > SpawnMatrix.w)
        {
            currentPosition.x = Mathf.Floor(-SpawnMatrix.w) - 0.5f;
        } 
        else if (currentPosition.x < -SpawnMatrix.w)
        {
            currentPosition.x = Mathf.Floor(SpawnMatrix.w) + 0.5f;
        }

        if (currentPosition.y > SpawnMatrix.h)
        {
            currentPosition.y = Mathf.Floor(-SpawnMatrix.h) - 0.5f;
        }
        else if (currentPosition.y < -SpawnMatrix.h)
        {
            currentPosition.y = Mathf.Floor(SpawnMatrix.h) + 0.5f;
        }

        transform.position = currentPosition;
        transform.Translate(moveDirection * SpawnMatrix.k);


        if (ate)
        {
            GameObject instantiatedTail = Instantiate(tailPrefab, currentPosition, Quaternion.identity);
            tail.Insert(0, instantiatedTail.transform);
            ate = false;
        }
        else if (tail.Count > 0)
        {
            //Di chuyển đuôi cuối cùng đến nơi đầu
            tail.Last().position = currentPosition;

            //Thêm đuôi vào trước danh sách, loại bỏ từ phía sau
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

        isMovingForward = true;
    }


    private bool IsStraightLine(Vector2 headPosition, Vector2 tailPosition)
    {
        Vector2 direction = tailPosition - headPosition;
        float dotProduct = Vector2.Dot(direction, transform.up);

        return Mathf.Approximately(dotProduct, 0f);
    }

    private void OnDestroy()
    {
        Destroy(game);
        if(gameOver != null)
        {
            gameOver.SetActive(true);
        }
        GameObject[] snake = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < snake.Length; i++)
        {
            Destroy(snake[i]);
        }
    }

}
