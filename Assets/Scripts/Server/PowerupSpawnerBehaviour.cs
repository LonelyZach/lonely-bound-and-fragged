using UnityEngine;
using UnityEngine.Networking;

public class PowerupSpawnerBehaviour : NetworkBehaviour
{
  public float SpawningFieldHeight = 10.0f;
  public float SpawningFieldWidth = 18.0f;

  public float PowerupSpawnInterval = 5.0f;

  public GameObject PowerupPrefab;

  public Sprite[] Sprites;
  public GameObject[] ActivePowerupPrefabs;

  public float _timeToNextPowerupSpawn;

  private System.Random _random;

	// Use this for initialization
	void Start () {
    _timeToNextPowerupSpawn = PowerupSpawnInterval;
    _random = new System.Random();
  }
	
	// Update is called once per frame
	void Update () {
    _timeToNextPowerupSpawn -= Time.deltaTime;

    if(_timeToNextPowerupSpawn <= 0.0f)
    {
      SpawnPowerup();
      _timeToNextPowerupSpawn = PowerupSpawnInterval;
    }
  }

  private void SpawnPowerup()
  {
    var x = (Random.value * SpawningFieldWidth) - (SpawningFieldWidth / 2.0f);
    var y = (Random.value * SpawningFieldHeight) - (SpawningFieldHeight / 2.0f);
    Vector2 location = new Vector2(x, y);

    
    var i = _random.Next(0, ActivePowerupPrefabs.Length);
    var powerup = Instantiate(PowerupPrefab).GetComponent<PowerupBehaviour>();
    powerup.transform.position = location;
    powerup.ActivePowerupPrefab = ActivePowerupPrefabs[i];

    powerup.GetComponent<SpriteRenderer>().sprite = Sprites[i];

    NetworkServer.Spawn(powerup.gameObject);
  }
}
