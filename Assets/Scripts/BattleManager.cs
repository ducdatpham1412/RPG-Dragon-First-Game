using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    public enum BattleState {
        Begin_Battle,
        Intro,
        Player_Move,
        Player_Attack,
        Change_Control,
        Enemy_Attack,
        Battle_Result,
        Battle_End
    }
    private Dictionary<int, BattleState> battleStateHash = new Dictionary<int,
     BattleState>();
    private BattleState currentBattleState;
    public GameObject[] EnemySpawnPoints;
    public GameObject[] EnemyPrefabs;
    public AnimationCurve SpawnAnimationCurve;
    private int enemyCount;
    public CanvasGroup theButtons;
    public Animator battleStateManager;
    public GameObject introPanel;
    Animator introPanelAnim;
    private string selectedTargetName;
    private EnemyController selectedTarget;
    public GameObject selectionCircle;
    private bool canSelectEnemy;
    bool attacking = false;
    public GameObject smackParticle;
    public GameObject wackParticle;
    public GameObject kickParticle;
    public GameObject chopParticle;
    private GameObject attackParticle;


    void Awake() {
        battleStateManager = GetComponent<Animator>();
        if (battleStateManager == null) {
            Debug.LogError("No battleStateMachine Animator found.");
        }
        introPanelAnim = introPanel.GetComponent<Animator>();
    }


    void GetAnimationStates() {
        foreach (BattleState state in (BattleState[])
          System.Enum.GetValues(typeof(BattleState))) {
            battleStateHash.Add(Animator.StringToHash(state.ToString()), state);
        }
    }


    void Start() {
        enemyCount = Random.Range(1, EnemySpawnPoints.Length);
        StartCoroutine(SpawnEnemies());
        GetAnimationStates();
    }


    void Update() {
        currentBattleState = battleStateHash[battleStateManager.GetCurrentAnimatorStateInfo(0).shortNameHash];

        switch (currentBattleState) {
            case BattleState.Intro:
                introPanelAnim.SetTrigger("Intro");
                break;
            case BattleState.Player_Move:
                if (GetComponent<Attack>().attackSelected == true) {
                    canSelectEnemy = true;
                }
                break;
            case BattleState.Player_Attack:
                canSelectEnemy = false;
                if (!attacking) {
                    StartCoroutine(AttackTarget());
                }
                break;
            case BattleState.Change_Control:
                break;
            case BattleState.Enemy_Attack:
                break;
            case BattleState.Battle_Result:
                break;
            case BattleState.Battle_End:
                break;
            default:
                break;
        }

        if (currentBattleState == BattleState.Player_Move) {
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

            var controller = newEnemy.GetComponent<EnemyController>();
            controller.BattleManager = this;

            var EnemyProfile = ScriptableObject.CreateInstance<Enemy>();
            EnemyProfile.Class = EnemyClass.Dragon;
            EnemyProfile.level = 1;
            EnemyProfile.health = 10;
            EnemyProfile.name = EnemyProfile.Class + " " + i.ToString();
            controller.EnemyProfile = EnemyProfile;
        }

        battleStateManager.SetBool("BattleReady", true);
    }


    public bool CanSelectEnemy {
        get {
            return canSelectEnemy;
        }
    }


    public int EnemyCount {
        get {
            return enemyCount;
        }
    }

    public void SelectEnemy(EnemyController enemy, string name) {
        selectedTarget = enemy;
        selectedTargetName = name;
    }

    IEnumerator AttackTarget() {
        attacking = true;
        var damageAmount = GetComponent<Attack>().hitAmount;
        switch (damageAmount) {
            case 5:
                attackParticle = Instantiate(smackParticle);
                break;
            case 10:
                attackParticle = Instantiate(wackParticle);
                break;
            case 15:
                attackParticle = Instantiate(kickParticle);
                break;
            case 20:
                attackParticle = Instantiate(chopParticle);
                break;
        }
        if (attackParticle != null) {
            attackParticle.transform.position = selectedTarget.transform.position;
        }
        selectedTarget.EnemyProfile.health -= damageAmount;
        yield return new WaitForSeconds(1f);
        attacking = false;
        GetComponent<Attack>().hitAmount = 0;
        battleStateManager.SetBool("PlayerReady", false);
        Destroy(attackParticle);
    }
}
