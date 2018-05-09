using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementParticleBehaviour : MonoBehaviour {

  private AvatarBehaviour _avatarBehaviour;
  private ParticleSystem _particleSystem;

	// Use this for initialization
	void Start () {
    _particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
    _avatarBehaviour = gameObject.transform.parent.GetComponent<AvatarBehaviour>();
  }
	
	// Update is called once per frame
	void Update ()
  {
    var movementDirection = _avatarBehaviour.PlayerForceAngle;

    if(!float.IsNaN(movementDirection))
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
