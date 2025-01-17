using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour {
    private BattleManager battleManager;
    public Enemy EnemyProfile;
    Animator enemyAI;
    private bool selected;
    GameObject selectionCircle;

    public BattleManager BattleManager {
        get {
            return battleManager;
        }
        set {
            battleManager = value;
        }
    }

    public void UpdateAI() {
        if (enemyAI != null && EnemyProfile != null) {
            enemyAI.SetInteger("EnemyHealth", EnemyProfile.health);
            enemyAI.SetInteger("PlayerHealth", GameState.CurrentPlayer.health);
            enemyAI.SetInteger("EnemiesInBattle", battleManager.EnemyCount);
        }
    }

    public void Awake() {
        enemyAI = GetComponent<Animator>();
        if (enemyAI == null) {
            Debug.LogError("No AI System Found");
        }
    }

    void Update() {
        UpdateAI();
    }

    IEnumerator SpinObject(GameObject target) {
        while (true) {
            target.transform.Rotate(0, 0, 180 * Time.deltaTime);
            yield return null;
        }
    }

    void OnMouseDown() {
        if (battleManager.CanSelectEnemy) {
            selectionCircle = Instantiate(battleManager.selectionCircle);
            selectionCircle.transform.parent = transform;
            selectionCircle.transform.localPosition = new Vector3(0f, -1f, 0f);
            selectionCircle.transform.localScale = new Vector3(4f, 4f, 1f);
            StartCoroutine(SpinObject(selectionCircle));
            battleManager.SelectEnemy(this, EnemyProfile.name);
            battleManager.GetComponent<Attack>().attackSelected = false;
            battleManager.battleStateManager.SetBool("PlayerReady", true);
        }
    }

}
