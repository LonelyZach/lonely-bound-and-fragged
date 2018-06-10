using UnityEngine;
using UnityEngine.Networking;

public class AvatarBehaviour : NetworkBehaviour {


  private Vector2 _startPositon;

  [SyncVar]
  public Color startColor;

  public float MoveSpeed = 100.0f;

  private float _playerDrivenMovement = float.NaN;
  private float _playerDrivenIntensity = 0.0f;

  public bool IsAlive { get { return _alive; } }

  [SyncVar]
  public float PlayerForceAngleReadOnly = float.NaN;

  [SyncVar]
  public float PlayerForceIntensityReadOnly = 0.0f;

  [SyncVar]
  private bool _alive = true;

  void Start()
  {
    _startPositon = gameObject.transform.position;
    gameObject.GetComponent<SpriteRenderer>().color = startColor;
  }

  private void Update()
  {
    AddPlayerDrivenMovementForce();
    if(!_alive)
    {
      gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }
  }

  // Update is called once per frame
  public void SetPlayerDrivenMovement(float angleOfForce, float intensity)
  {
    if (_alive)
    {
      PlayerForceAngleReadOnly = angleOfForce;
      _playerDrivenMovement = angleOfForce;

      PlayerForceIntensityReadOnly = intensity;
      _playerDrivenIntensity = intensity;
    }
  }

  private void AddPlayerDrivenMovementForce()
  {
    //Precondition is that our prime variable is a number and valid angle
    if(float.IsNaN(_playerDrivenMovement) || _playerDrivenMovement < 0.0f || _playerDrivenMovement > 360.0f)
    {
      return;
    }

    //If preconditions met, make some calculations!
    float radAngle = Mathf.Deg2Rad * _playerDrivenMovement;
    float forceMagnitude = MoveSpeed * Time.deltaTime * _playerDrivenIntensity;

    //Determine the angle with simple trig
    var forceVector = new Vector2(Mathf.Cos(radAngle) * forceMagnitude, Mathf.Sin(radAngle) * forceMagnitude);

    //Apply the force to the game object
    gameObject.GetComponent<Rigidbody2D>().AddForce(forceVector, ForceMode2D.Force);
  }

  public void Kill()
  {
    _alive = false;
    _playerDrivenMovement = float.NaN;
    PlayerForceAngleReadOnly = float.NaN;
    PlayerForceIntensityReadOnly = 0.0f;
  }

  public void ReturnToStartPosition()
  {
    gameObject.GetComponent<Rigidbody2D>().MovePosition(_startPositon);
    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
  }

  [ClientRpc]
  public void Rpc_Resurect()
  {
    _alive = true;
    gameObject.GetComponent<SpriteRenderer>().color = startColor;
  }
}
