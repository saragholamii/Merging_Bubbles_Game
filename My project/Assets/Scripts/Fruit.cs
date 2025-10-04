using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Fruit : MonoBehaviour
    {
        public FruitData data;
        private SpriteRenderer sr;
        private Rigidbody2D rb;

        private bool dontHandleCollision = false;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(FruitData fruitData, bool inJar = false)
        {
            if(inJar)
                transform.SetParent(GameManager.Instance.jar.transform);
            data = fruitData;
            if(data == null)
                print("********************************** FRUIT DATA IS NULL *******************************");
            sr.sprite = data.sprite;
            transform.localScale = data.size * Vector2.one;
            
            if (inJar)
                rb.simulated = true;
            else
                transform.localPosition = Vector2.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
            if (otherFruit != null && otherFruit.data.level == data.level)
            {
                Merge(otherFruit);
            }
        }

        private void Merge(Fruit other)
        {
            if(dontHandleCollision)
                return;
            
            other.gameObject.GetComponent<Fruit>().dontHandleCollision = true;
            
            MusicManager.instance.PlayMergeSound();
            
            if (GameManager.Instance.TryGetNextFruit(data.level, out FruitData nextData))
            {
                // Spawn merged fruit
                Vector3 newPos = (transform.position + other.transform.position) / 2f;
                GameObject newFruit = Instantiate(GameManager.Instance.fruitPrefab, newPos, Quaternion.identity);
                newFruit.GetComponent<Fruit>().Init(nextData, true);

                GameManager.Instance.AddScore(nextData.score);
            }
            
            
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}