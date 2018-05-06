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
	void Update () {
    var movementDirections = _avatarBehaviour.GetPlayerDirvenMovementWithOpposingDirectionsIgnored();

    if(movementDirections.Any())
    {
      _particleSystem.gameObject.SetActive(true);
    }
    else
    {
      if(!_particleSystem.isStopped)
      {
        _particleSystem.gameObject.SetActive(false);
      }
    }

    if(movementDirections.Contains(Direction.Down) && movementDirections.Contains(Direction.Right))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, 45);
    }
    else if (movementDirections.Contains(Direction.Down) && movementDirections.Contains(Direction.Left))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, -45);
    }
    else if (movementDirections.Contains(Direction.Up) && movementDirections.Contains(Direction.Right))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, 135);
    }
    else if (movementDirections.Contains(Direction.Up) && movementDirections.Contains(Direction.Left))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, -135);
    }
    else if (movementDirections.Contains(Direction.Down))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }
    else if (movementDirections.Contains(Direction.Up))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, 180);
    }
    else if (movementDirections.Contains(Direction.Right))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, 90);
    }
    else if (movementDirections.Contains(Direction.Left))
    {
      gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
    }
  }
}
