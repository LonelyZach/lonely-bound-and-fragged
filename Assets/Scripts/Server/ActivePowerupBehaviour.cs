using UnityEngine;
using UnityEngine.Networking;

using System.Linq;

/// <summary>
/// Network behaviour for powerups that are activley affecting the game state.
/// </summary>
public abstract class ActivePowerupBehaviour : NetworkBehaviour
{
  public Color ActivatingAvatarColor;

  protected AvatarBehaviour ActivatingAvatar;

  private bool _initialized = false;

  private NetworkIdentity _networkIdentity;

  private float TimeToLive = 10.0f;

  // Use this for initialization
  void Start()
  {
    _networkIdentity = GetComponent<NetworkIdentity>();
    Initialize();
  }

  // Update is called once per frame
  void Update()
  {
    if (_networkIdentity.isServer)
    {
      ServerUpdate();
    }

    if(_networkIdentity.isClient)
    {
      ClientUpdate();
    }
  }

  public void Initialize()
  {
    _initialized = true;
    ActivatingAvatar = FindObjectsOfType<AvatarBehaviour>().Single(x => x.startColor == ActivatingAvatarColor);
    StartPowerupServer();
    Rpc_StartPowerupClient();
  }

  private void ClientUpdate()
  {
    gameObject.transform.position = ActivatingAvatar.transform.position;
  }

  private void ServerUpdate()
  {
    TimeToLive -= Time.deltaTime;

    if(TimeToLive < 0.0f)
    {
      EndPowerupServer();
      Rpc_EndPowerupClient();
      NetworkServer.Destroy(gameObject);
    }
  }

  protected abstract void StartPowerupServer();

  protected abstract void StartPowerupClient();

  protected abstract void EndPowerupServer();

  protected abstract void EndPowerupClient();

  [ClientRpc]
  public void Rpc_EndPowerupClient()
  {
    EndPowerupClient();
  }

  [ClientRpc]
  public void Rpc_StartPowerupClient()
  {
    StartPowerupClient();
  }
}
