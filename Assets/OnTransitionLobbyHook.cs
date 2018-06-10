using System.Collections;
using System.Collections.Generic;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class OnTransitionLobbyHook : LobbyHook {

  public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
  {
    LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
    NetworkPlayerBehaviour player = gamePlayer.GetComponent<NetworkPlayerBehaviour>();

    player.avatarName = lobby.name;
    player.avatarColor = lobby.playerColor;
  }
}
