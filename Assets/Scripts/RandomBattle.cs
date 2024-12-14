using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RandomBattle : MonoBehaviour {
    public int battleProbability;
    int encounterChance = 100;
    public int secondsBetweenBattles;
    public string battleSceneName;

    IEnumerator RecalculateChance() {
        while (encounterChance > battleProbability) {
            yield return new WaitForSeconds(secondsBetweenBattles);
            encounterChance = Random.Range(1, 100);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!GameState.justExitedBattle) {
            encounterChance = Random.Range(1, 100);
            if (encounterChance > battleProbability) {
                StartCoroutine(RecalculateChance());
            }
        }
        else {
            StartCoroutine(RecalculateChance());
            GameState.justExitedBattle = false;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (encounterChance <= battleProbability) {
            Debug.Log("Battle");
            SceneManager.LoadScene(battleSceneName);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        encounterChance = 100;
        StopCoroutine(RecalculateChance());
    }
}
