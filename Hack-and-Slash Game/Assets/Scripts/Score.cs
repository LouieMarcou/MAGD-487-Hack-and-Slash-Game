using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject endgamePanel;
    [SerializeField] private TMP_Text winLossText;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text numUpgradesText;
    [SerializeField] private TMP_Text damageTakenText;
    [SerializeField] private TMP_Text scoreText;


    private float damageTaken;
    private float numberOfUpgrades;
    private float totalTime;
    private float scoreTotal;
    private float enemiesKilled;


    public void SetScore()
    {
	Debug.Log("Game is over");
        Time.timeScale = 0;
        player.enabled = false;
		numberOfUpgrades = player.upgrades.Count;
        enemiesKilled = GetComponent<EnemyManager>().totalEnmiesKilled;
        endgamePanel.SetActive(true);
        if (player.GetIsAlive())
        {
            winLossText.text = "You win!";
            scoreTotal += 5000f;
        }
        else
            winLossText.text = "You lose!";
        //timeText.text = "Total time is " + totalTime;
        enemiesKilledText.text = "You have killed " + enemiesKilled + " enemies";
        numUpgradesText.text = "The number of upgrades you have is " + numberOfUpgrades;
        damageTakenText.text = "The amount of damage you have taken is " + damageTaken;
        scoreTotal = scoreTotal + (enemiesKilled * 100) + (numberOfUpgrades * 500) - (damageTaken * 5);
        scoreText.text = "Your score is " + scoreTotal;
    }

    public void AddToDamageTaken(float damage)
    {
        damageTaken += damage;
    }

    public void SetNumUpgrades(float num)
    {
        numberOfUpgrades = num;
    }

    public void SetTime()
    {
        totalTime = timer.totalTimeInSeconds;
    }
    
}
