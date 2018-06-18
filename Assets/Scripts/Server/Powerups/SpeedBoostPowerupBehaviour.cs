using UnityEngine;

public class SpeedBoostPowerupBehaviour : ActivePowerupBehaviour {

  public GameObject MovementParticles;

  protected override void StartPowerupClient()
  {
    var movementParticleBehaviour = MovementParticles.GetComponent<MovementParticleBehaviour>();
    movementParticleBehaviour.AvatarBehaviour = ActivatingAvatar;
    movementParticleBehaviour.gameObject.SetActive(true);
  }

  protected override void EndPowerupClient()
  {

  }

  protected override void StartPowerupServer()
  {
    ActivatingAvatar.MoveSpeed = ActivatingAvatar.MoveSpeed * 2;
  }

  protected override void EndPowerupServer()
  {
    ActivatingAvatar.MoveSpeed = ActivatingAvatar.MoveSpeed / 2;
  }
}
