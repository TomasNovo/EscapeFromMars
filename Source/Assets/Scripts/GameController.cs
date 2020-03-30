using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool gameOver = false;
    public float score_value = 0;
    private float gameCyclePoints = 12000;

    private float firstPowerUpSpawnPoints = 1500;
    public float gameSpeed = -1.5f;
    public float objectsSpeed = -1.5f; //speed of objects, dont change
    private int frames = 0;
    private float pointsPerSecond = 40;
    private float lastIterationUpdateTime = 0;


    private CoinGenerator coinGenerator;
    private float lastCoinSpawnTime = -4f;
    private float coinDeltaTime = 4f;
    private float coinSpawnMaxY = 18f;
    private float coinSPawnMinY = -4f;
    private bool coinInitialSpawn = true;

    //boss
    private int predatorSpawnDeltaPoints = 1000; //spawn the predator at 1000 points
    public bool predatorAtive = false; //checks if predator active

    public bool predatorDead = false;
    public GameObject predatorPrefab;
    public GameObject badPowerUp;
    public GameObject goodPowerUp;

    public float predatorMaxSpawnY = 1.3f;
    public float predatorMinSpawY = -1.3f;

    public bool miniExploderActive = true; //spawn mini exploders
    private MiniExploderGenerator miniExploderGenerator;
    private float lastMiniExploderSpawnTime = 0f;
    private int miniExploderDeltaTime = 6;
    public float miniExploderSpawnChance = 75f; //75% of having a miniexploder spawn in miniExploderDeltaTime
    public int miniExploderMaxSpawnNumber = 3;
    public float miniExploderSpawnMaxY = 1.3f;
    public float miniExploderSPawnMinY = -1.3f;
    private int miniExploderMaxNumberOnScreen = 13;

    // StaticObjects

    private BigLaserGenerator bigLaserGenerator;
    private int staticObjectDeltaTime = 2; //obstacle each 2 seconds

    private StaticObjectsGenerator staticObjectsGenerator;
    private int lasersDeltaTime = 7;
    private float lastLaserSpawnTime = 0;
    private float staticObjectSpawnChance = 80f; //80% of spawning
    private float lastStaticObjectSpawnTime = 0;
    private float staticObjectSpawnMaxY = 12f; //12 units need divide by 10
    private float staticObjectSpawnMinY = -12f;
    //

    // Machine Guns
    private MachineGunGenerator machineGunGenerator;
    private int machineGunDeltaTime = 4; //machine each 3 seconds
    private float lastMachineGunSpawnTime = 0;
    private float machineGunYPos = -1.33f;
    //

    // MiniExploderWave
    private float lastMiniExploderWaveSpawnTime = 0;
    private int MiniExploderWaveDeltaTime = 4; //wave each 5 seconds
    //

    private AsteroidGenerator asteroidGenerator;
    private float lastAsteroidSpawnTime = 0;
    private int asteroidDeltaTime = 1; //asteroid rate(1/second)
    private float asteroidSpawnMaxY = 20f;
    private float asteroidSpawnMinY = -10f;

    private bool powerUpActive = false;
    private int powerUpDeltaTime = 15; //power up em 15 segundos
    private float lastPowerUpSpawnTime = 0;
    private float pwoerUpSpawnMaxY = 13f;
    private float pwoerUpSpawnMinY = -13f;

    public GameObject powerUp;
    public GameObject game_over_menu;
    public GameObject game_win_menu;


    public GameObject gameInterface;

    public static int score_multiplier = 1;
    // Start is called before the first frame update

    //called before start
    void Awake()
    {
        gameOver = false;

        if (instance != null)
        {
            GameController.Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void DestroyScene()
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].name != "Main Camera")
                Destroy(objects[i]);
        }
    }

    void Start()
    {
        gameOver = false;
        score_value = 0;
        CoinsController.coins_value = 0;
        score_multiplier = 1;
        CoinCatchScript.multiplier = 1;
        lastIterationUpdateTime = Time.time;
        lastMiniExploderSpawnTime = Time.time;
        lastPowerUpSpawnTime = Time.time;
        lastStaticObjectSpawnTime = Time.time;
        lastMachineGunSpawnTime = Time.time;
        lastAsteroidSpawnTime = Time.time;
        lastMiniExploderWaveSpawnTime = Time.time;
        lastLaserSpawnTime = -1 * lasersDeltaTime;
        asteroidGenerator = FindObjectOfType<AsteroidGenerator>();
        coinGenerator = FindObjectOfType<CoinGenerator>(); //find the only1 coingenerator object
        miniExploderGenerator = FindObjectOfType<MiniExploderGenerator>(); //find the only1 Exploder object
        staticObjectsGenerator = FindObjectOfType<StaticObjectsGenerator>(); //find the only1 StaticObjectgenerator object
        machineGunGenerator = FindObjectOfType<MachineGunGenerator>();
        bigLaserGenerator = FindObjectOfType<BigLaserGenerator>();
        lastCoinSpawnTime = Time.time;
        gameInterface.SetActive(true);
        powerUp.SetActive(false);
        UpdateSound();
        game_over_menu.SetActive(false);
        game_win_menu.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Jungle");
    }

    void UpdateSound(){
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<AudioSource>().volume *= SettingsScript.GetSoundEntry();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameOver)
        {
            score_multiplier = 0;
            CoinCatchScript.multiplier = 0;
        }

        frames += 1;

        if (PauseController.paused == false)
        {
            float elapsed = Time.time - lastIterationUpdateTime;
            score_value += elapsed * pointsPerSecond * score_multiplier;
            lastIterationUpdateTime = Time.time;
        }
        /* if (frames % 60 == 0)
         {
             score_value += 1 * score_multiplier;
         }*/

        if (!gameOver)
        {
            CoinSpawn();
            GameCycle(); //executes the game cycle logic
        }
        else
        {
            gameSpeed = 0;
            objectsSpeed = 0;
        }
    }

    void CoinSpawn()
    {
        //coins
        if (score_value <= 200)
            return;
        if ((Time.time - lastCoinSpawnTime) >= coinDeltaTime)
        {
            lastCoinSpawnTime = Time.time;
            float spawnYpos = Random.Range(coinSPawnMinY, coinSpawnMaxY);

            spawnYpos /= 10;
            coinGenerator.Spawn(new Vector3(transform.position.x + 5, spawnYpos, transform.position.z));
        }
    }

    void PowerUpSpawn()
    {
        //PowerUpSpawn
        if (score_value >= firstPowerUpSpawnPoints && !powerUpActive && (Time.time - lastPowerUpSpawnTime) >= powerUpDeltaTime)
        {
            lastPowerUpSpawnTime = Time.time;
            if (Random.Range(0, 1 + 1) == 1) //good
            {
                Instantiate(goodPowerUp, new Vector3(transform.position.x + 5, 0, transform.position.z), Quaternion.identity);
            }
            else //bad
            {
                Instantiate(badPowerUp, new Vector3(transform.position.x + 5, 0, transform.position.z), Quaternion.identity);
            }
        }
    }

    void ResetPredator()
    {
        predatorAtive = false;
        predatorDead = false;
    }

    void BigLasersSpawn()
    {
        if ((Time.time - lastLaserSpawnTime) >= lasersDeltaTime && score_value >= 3200)
        {
            lastLaserSpawnTime = Time.time;
            bigLaserGenerator.Spawn();
        }
    }

    void GameCycle()
    {
        float currentPosition = score_value % 12000;

        //coin initial Spawn
        if (coinInitialSpawn)
        {
            coinInitialSpawn = false;
            coinGenerator.InitialSpawn();
        }
        //Debug.Log(currentPosition);

        if (currentPosition >= 0 && currentPosition < 1000)
        { //lasers 
            //score_value = 3500; //to debug where the thing u want
            //Static Objects Generator
            StaticObjectsSpawn();
            PowerUpSpawn();
        }
        else if (currentPosition >= 1000 && currentPosition < 2000)
        { //lasers e canhoes
            StaticObjectsSpawn();
            MachineGunSpawn();
            PowerUpSpawn();
        }
        else if (currentPosition >= 2000 && currentPosition < 3000)
        { //power up, lasers, mini exploders
            PowerUpSpawn();
            StaticObjectsSpawn();
            MiniExploderSpawn();
        }
        else if (currentPosition >= 3000 && currentPosition < 4000)
        {
            PowerUpSpawn();
            BigLasersSpawn();
        }
        else if (currentPosition >= 4000 && currentPosition < 8000)
        {
            PowerUpSpawn();
            StaticObjectsSpawn();
            //Predator Spawn
            if (!predatorAtive)
            { //se nao existir o predator
                predatorSpawn();
            }
            if (predatorDead && currentPosition >= 7000)
            {
                MiniExploderSpawn();
            }

        }
        else if (currentPosition >= 8000 && currentPosition < 10000) //lasers moveis
        { //lasers e canhoes
            PowerUpSpawn();
            StaticObjectsSpawn();
            MiniExploderSpawn();
        }
        else if (currentPosition >= 10000 && currentPosition < 11000)
        {
            PowerUpSpawn();
            AsteroidSpaw();
        }
        else if (currentPosition >= 11000 && currentPosition < 12000)
        {
            PowerUpSpawn();
            MiniExploderWaveSpawnPeriodic();
        }
        /*
        //PowerUpSpawn()
        PowerUpSpawn();
        //Static Objects Generator
        StaticObjectsSpawn();
        //MachineGunSpawn
        MachineGunSpawn();

        //Mini Exploder Spawn
        if (miniExploderActive)
        {
            MiniExploderSpawn();
        }

        //Predator Spawn
        if (!predatorAtive && score_value > predatorSpawnDeltaPoints)
        { //se nao existir o predator
            predatorSpawn();
        }*/
    }

    void MiniExploderWaveSpawnPeriodic(){
        if ((Time.time - lastMiniExploderWaveSpawnTime) >= MiniExploderWaveDeltaTime)
        {
            lastMiniExploderWaveSpawnTime = Time.time;
            MiniExploderWaveSpawn();
        }
    }

    void AsteroidSpaw()
    {
        if ((Time.time - lastAsteroidSpawnTime) >= asteroidDeltaTime)
        {
            float spawnYpos = Random.Range(asteroidSpawnMinY, asteroidSpawnMaxY);
            spawnYpos /= 10f;
            lastAsteroidSpawnTime = Time.time;
            Debug.Log(spawnYpos);
            asteroidGenerator.Spawn(new Vector3(transform.position.x + 5, spawnYpos, transform.position.z));
        }
    }
    void MachineGunSpawn()
    {
        if ((Time.time - lastMachineGunSpawnTime) >= machineGunDeltaTime)
        {
            lastMachineGunSpawnTime = Time.time;
            machineGunGenerator.Spawn(new Vector3(transform.position.x + 5, machineGunYPos, transform.position.z));
        }
    }

    public void StaticObjectsSpawn()
    {
        if ((Time.time - lastStaticObjectSpawnTime) >= staticObjectDeltaTime)
        {
            lastStaticObjectSpawnTime = Time.time;
            if (Random.Range(1, 100 + 1) <= staticObjectSpawnChance)
            {
                float spawnYpos = Random.Range(staticObjectSpawnMinY, staticObjectSpawnMaxY);
                spawnYpos /= 10f;
                staticObjectsGenerator.Spawn(new Vector3(transform.position.x + 5, spawnYpos, transform.position.z));
            }
        }
    }
    public List<int> GenerateRandomList(int minNum, int maxNum, int listSize)
    {
        List<int> uniqueNumbers = new List<int>();
        List<int> finishedList = new List<int>();

        for (int i = 0; i < maxNum; i++)
        {
            uniqueNumbers.Add(i);
        }

        for (int i = 0; i < listSize; i++)
        {
            int ranNum = uniqueNumbers[Random.Range(minNum, uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
        return finishedList;
    }

    public void DebugList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }

    private void MiniExploderSpawn()
    {
        if ((Time.time - lastMiniExploderSpawnTime) >= miniExploderDeltaTime)
        {
            if (Random.Range(0, 100) <= miniExploderSpawnChance) //having a mini exploder spawn
            {
                int miniExplodersToSpawn = Random.Range(1, miniExploderMaxSpawnNumber + 1);

                //selects the indexs to spawn
                List<int> miniIndexes = GenerateRandomList(1, miniExploderMaxNumberOnScreen, miniExplodersToSpawn);
                //DebugList(miniIndexes);

                lastMiniExploderSpawnTime = Time.time;

                List<Vector3> positions = new List<Vector3>();
                float spawnYpos = miniExploderSPawnMinY;
                for (int i = 0; i < miniExploderMaxNumberOnScreen; i++) //percorre todos os possiveis
                {
                    if (miniIndexes.IndexOf(i) != -1)
                    { //se o index existe nos indexs a gerar
                        positions.Add(new Vector3(transform.position.x + 5, spawnYpos, transform.position.z));
                        spawnYpos += 0.25f;
                    }
                    else
                    {
                        spawnYpos += 0.25f;
                    }
                }
                miniExploderGenerator.Spawn(positions, miniExplodersToSpawn);
            }
        }

    }

    public void MiniExploderWaveSpawn()
    {
        List<Vector3> positions = new List<Vector3>();
        float spawnYpos = miniExploderSPawnMinY;
        int notspawn1 = Random.Range(0, 9 + 1);
        int notspawn2 = notspawn1 + 1; //dont spawn 2 
        int notspawn3 = notspawn2 + 1;
        int notspawn4 = notspawn3 + 1;
        int notspawn5 = notspawn4 + 1;
        for (int i = 0; i < miniExploderMaxNumberOnScreen; i++)
        {
            if ((i == notspawn1 || i == notspawn2 || i == notspawn3 || i == notspawn4 || i == notspawn5))
            {
               spawnYpos += 0.25f;
               continue;
            }
            positions.Add(new Vector3(transform.position.x + 5, spawnYpos, transform.position.z));
            spawnYpos += 0.25f;
        }
        miniExploderGenerator.Spawn(positions, 8);
    }

    private void predatorSpawn()
    { //NO NEED POOL
        miniExploderActive = false;
        predatorAtive = true;
        Instantiate(predatorPrefab, new Vector3(transform.position.x + 5, 0, transform.position.z), Quaternion.identity);
    }
}
