using UnityEngine;

namespace DefaultNamespace
{
    public class FruitSpawner:MonoBehaviour
    {
        public float moveSpeed = 200f; // pixels per second
        public Transform basket;
        public Transform jar;
        private int direction = 1;

        private RectTransform rectTransform;
        private GameObject currentFruit;
        

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            // Move left & right in UI space
            rectTransform.anchoredPosition += Vector2.right * moveSpeed * direction * Time.deltaTime;

            float halfWidth = ((RectTransform)rectTransform.parent).rect.width / 2f - 80f;
            if (Mathf.Abs(rectTransform.anchoredPosition.x) > halfWidth)
                direction *= -1;

            if (currentFruit != null && Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(rectTransform.localPosition);
                worldPos.z = 0;

                currentFruit.transform.SetParent(jar);
                //currentFruit.transform.position = worldPos;
                currentFruit.GetComponent<Rigidbody2D>().simulated = true;
                currentFruit = null;

                StartCoroutine(WaitAndSpawnFruit());
            }
        }
        
        private System.Collections.IEnumerator WaitAndSpawnFruit()
        {
            yield return new WaitForSeconds(1f);
            SpawnFruit();
        }

        public void SpawnFruit()
        {
            
            FruitData randomFruit = GameManager.Instance.GetRandomFruit();
            currentFruit = Instantiate(GameManager.Instance.fruitPrefab, Vector3.zero, Quaternion.identity);
            currentFruit.transform.SetParent(basket);
            currentFruit.GetComponent<Fruit>().Init(randomFruit);
            currentFruit.GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}