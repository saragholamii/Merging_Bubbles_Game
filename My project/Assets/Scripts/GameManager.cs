using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager: MonoBehaviour
    {
        public static GameManager Instance;

        public FruitDataList fruitDataList;  
        public GameObject fruitPrefab;
        public FruitSpawner fruitSpawner;
        public Transform jar;
        public TextMeshProUGUI scoreText;
        public string mainMenuName = "Main Menu"; 
        public Animator scoreAnimator;
        

        private int score = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            fruitSpawner.SpawnFruit();
        }

        public void AddScore(int amount)
        {
            score += amount;
            scoreText.text = score.ToString();
            scoreAnimator.Play("HitScoreAnimation", -1, 0f);
        }

        public bool TryGetNextFruit(int currentLevel, out FruitData nextData)
        {
            nextData = fruitDataList.fruitsDataList.Find(f => f.level == currentLevel + 1);
            return nextData != null;
        }

        public FruitData GetRandomFruit()
        {
            // Difficulty scaling by score
            int maxLevel = Mathf.Min(score / 100 + 1, fruitDataList.fruitsDataList.Count - 1);
            int randomLevel = Random.Range(0, maxLevel);
            print("Spawning fruit of level " + randomLevel + " (maxLevel: " + maxLevel + ", score: " + score + ")");
            return fruitDataList.fruitsDataList.Find(f => f.level == randomLevel);
        }

        public void BackToMenu()
        {
            PlayerPrefs.SetInt("BestScore", score > PlayerPrefs.GetInt("BestScore", 0) ? score : PlayerPrefs.GetInt("BestScore", 0));
            SceneManager.LoadScene(mainMenuName);
        }
    }
}