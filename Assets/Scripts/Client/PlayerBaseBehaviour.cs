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

  // [Command] - enable for networking
  protected void Cmd_SetPlayerDrivenMovement(float angleOfForce, float intensity)
  {
    intensity = Mathf.Clamp(intensity, 0.0f, 1.0f);
    _associatedAvatarBehaviour.SetPlayerDrivenMovement(angleOfForce, intensity);
  }
}
