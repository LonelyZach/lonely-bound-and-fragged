using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviourBase : MonoBehaviour {

  public GameObject AvatarToControl;

  protected AvatarBehaviour _associatedAvatarBehaviour;

  protected void FindAssociatedAvatarBehaviour()
  {
    _associatedAvatarBehaviour = AvatarToControl.GetComponent<AvatarBehaviour>();
  }

  // [Command] - enable for netwokring
  protected void Cmd_SetPlayerDrivenMovement(float angleOfForce)
  {
    _associatedAvatarBehaviour.SetPlayerDrivenMovement(angleOfForce);
  }
}
