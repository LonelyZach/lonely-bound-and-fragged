using UnityEngine;

public class MovementParticleBehaviour : MonoBehaviour {

  public AvatarBehaviour AvatarBehaviour;

  private ParticleSystem _particleSystem;

	// Use this for initialization
	void Start () {
    _particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
  }
	
	// Update is called once per frame
	void Update ()
  {
    var movementDirection = AvatarBehaviour.PlayerForceAngleReadOnly + 90.0f;
    if(movementDirection > 360.0f)
    {
      movementDirection -= 360.0f;
    }

    if(!float.IsNaN(movementDirection) && AvatarBehaviour.PlayerForceIntensityReadOnly > 0.0f)
    {
      _particleSystem.gameObject.SetActive(true);
      gameObject.transform.eulerAngles = new Vector3(0, 0, movementDirection);
    }
    else
    {
      if(!_particleSystem.isStopped)
      {
        _particleSystem.gameObject.SetActive(false);
      }
    }
  }
}
