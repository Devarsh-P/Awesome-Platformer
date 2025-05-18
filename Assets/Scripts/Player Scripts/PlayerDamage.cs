using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public TMP_Text LifeText;
    private int LifeScoreCount;

    private bool canDamage;

    private Vector2 SpawnPosition;

    void Awake()
    {
        LifeScoreCount = 3;
        LifeText.text = "x" + LifeScoreCount;

        canDamage = true;

        SpawnPosition = transform.position;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
       if (canDamage)
        {
            LifeScoreCount--;

            if (LifeScoreCount >= 0)
            {
                LifeText.text = "x" + LifeScoreCount;
            }

            if (LifeScoreCount == 0)
            {
                //Restart The Game
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;
        }
        StartCoroutine(WaitForDamage());
    }


    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
    }

    void WaterDamage()
    {
        transform.position = SpawnPosition;
        DealDamage();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == MyTags.WATER_TAG)
        {
            WaterDamage();
        }
    }

}//Class
