using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Network behaviour for powerup icons that have not been picked up yet.
/// </summary>
public class PowerupBehaviour : NetworkBehaviour
{
  /// <summary>
  /// The prefab containing an <see cref="ActivePowerupBehaviour"/> to instatiate when this powerup is picked up.
  /// </summary>
  public GameObject ActivePowerupPrefab;

  private NetworkIdentity _networkIdentity;

  private bool _triggered = false;

  // Use this for initialization
  void Start () {
    _networkIdentity = GetComponent<NetworkIdentity>();
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnTriggerEnter2D(Collider2D other)
  {
    if (!_networkIdentity.isServer || _triggered)
    {
      return;
    }

    var collidingAvatar = other.transform.gameObject.GetComponent<AvatarBehaviour>();
    if (collidingAvatar == null)
    {
      return;
    }

    _triggered = true;

    var activePowerup = (GameObject)Instantiate(ActivePowerupPrefab, new Vector3(0, 0), Quaternion.identity);
    activePowerup.GetComponent<ActivePowerupBehaviour>().ActivatingAvatarColor = collidingAvatar.startColor;
    NetworkServer.Spawn(activePowerup);

    NetworkServer.Destroy(gameObject);
  }
}
