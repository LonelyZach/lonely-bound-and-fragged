﻿using UnityEngine;
using UnityEngine.Networking;

using System.Linq;

/// <summary>
/// Network behaviour for powerups that are activley affecting the game state.
/// </summary>
public abstract class ActivePowerupBehaviour : NetworkBehaviour
{
  public float TimeToLive = 10.0f;

  [SyncVar]
  public int ActivatingAvatarId;

  protected AvatarBehaviour ActivatingAvatar;

  private NetworkIdentity _networkIdentity;

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
    ActivatingAvatar = FindObjectsOfType<AvatarBehaviour>().Single(x => x.Id == ActivatingAvatarId);

    if (_networkIdentity.isServer)
    {
      StartPowerupServer();
    }
    if(_networkIdentity.isClient)
    {
      StartPowerupClient();
    }
  }

  private void ClientUpdate()
  {
    gameObject.transform.position = ActivatingAvatar.transform.position;
  }

  private void ServerUpdate()
  {
    TimeToLive -= Time.deltaTime;

    if(TimeToLive < 0.0f || !ActivatingAvatar.IsAlive)
    {
      End();
    }
  }

  private void End()
  {
    EndPowerupServer();
    NetworkServer.Destroy(gameObject);
  }

  protected abstract void StartPowerupServer();

  protected abstract void StartPowerupClient();

  protected abstract void EndPowerupServer();

  protected abstract void EndPowerupClient();

  public override void OnNetworkDestroy()
  {
    EndPowerupClient();
    base.OnNetworkDestroy();
  }
}
