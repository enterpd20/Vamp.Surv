using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("# Game Control")]
    public bool isTimeOnGoing;
    public float gameTime;
    public float maxgameTime = 2 * 10f;

    [Header("# Player Info")]
    public float currentHP;
    public float MAXHP = 100;
    public int level;
    public int kill;
    public int exp = 0;
    public int[] nextLevEXP = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600};

    [Header("# Game Object")]
    public Player_Control player;
    public PoolManager poolManager;
    public LevelUP uiLevelUP;
    public GameObject uiResult;

    public List<ItemData> itemDatabase;

    private void Awake()
    {
        Instance = this;       
    }

    private void Start()
    {
        currentHP = MAXHP;
        TimeOnGoing();

        //임시
        uiLevelUP.Select(2);
        AudioManager.instance.PlayBgm(true);
    }

    void Update()
    {
        if (!isTimeOnGoing) return;

        gameTime += Time.deltaTime;
        
        //이거 있으면 위에 maxgameTime 때문에 게임 레벨에 영향을 줘서 몬스터 최대 스폰 목록이 제한됨
        //if(gameTime > maxgameTime)
        //{
        //    gameTime = maxgameTime;
        //}
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextLevEXP[Mathf.Min(level, nextLevEXP.Length-1)]) //필요 경험치에 도달하면 레벨업
        {
            level++;
            exp = 0;
            uiLevelUP.Show();
        }
    }

    public ItemData GetItemDataById(int id) //새로추가
    {
        return itemDatabase.Find(item => item.itemID == id);
       
        /*
        foreach (var item in itemDatabase)
        {
            Debug.Log($"Item ID: {item.itemID}, Name: {item.itemName}");
        }
        var result = itemDatabase.Find(item => item.itemID == id);
        if (result == null)
        {
            Debug.LogError($"No item found with ID: {id}");
        }
        return result;
        */       
    }

    public void TimeStop()
    {
        isTimeOnGoing = false;
        Time.timeScale = 0;
    }

    public void TimeOnGoing()
    {
        isTimeOnGoing = true;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        StartCoroutine(GameOver_co());
    }

    IEnumerator GameOver_co()
    {
        isTimeOnGoing = false;

        yield return new WaitForSeconds(0.5f);
        uiResult.SetActive(true);

        TimeStop();
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySFX(AudioManager.Sfx.sfx_gameOver);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("Intro");
    }
}
