using System.Collections;
using UnityEngine;
using UnityEngine.UI;


enum BattlePhase {
    PlayerAttack,
    EnemyAttack
}

public class BattleManager : MonoBehaviour {
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;
    private int enemyCount;
    private BattlePhase phase;
    public CanvasGroup theButtons;


    void Start() {
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());
        phase = BattlePhase.PlayerAttack;
    }


    void Update() {
        if (phase == BattlePhase.PlayerAttack) {
            theButtons.alpha = 1;
            theButtons.interactable = true;
            theButtons.blocksRaycasts = true;
        }
        else {
            theButtons.alpha = 0;
            theButtons.interactable = false;
            theButtons.blocksRaycasts = false;
        }
    }


    public void RunAway() {
        GameState.justExitedBattle = true;
        NavigationManager.NavigateTo("Overworld");
    }


    IEnumerator MoveCharacterToPoint(GameObject destination, GameObject character) {
        float timer = 0f;
        var StartPosition = character.transform.position;
        if (SpawnAnimationCurve.length > 0) {
            while (timer < SpawnAnimationCurve.keys[
                SpawnAnimationCurve.length - 1].time) {
                character.transform.position =
                    Vector3.Lerp(StartPosition,
                        destination.transform.position,
                            SpawnAnimationCurve.Evaluate(timer));
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else {
            character.transform.position =
                destination.transform.position;
        }
    }


    IEnumerator SpawnEnemies() {
        for (int i = 0; i < enemyCount; i++) {
            var newEnemy = Instantiate(EnemyPrefabs[0]);
            newEnemy.transform.position = new Vector3(10, -1, 0);
            yield return StartCoroutine(MoveCharacterToPoint(EnemySpawnPoints[i], newEnemy));
            newEnemy.transform.parent =
                EnemySpawnPoints[i].transform;
        }
    }
}
