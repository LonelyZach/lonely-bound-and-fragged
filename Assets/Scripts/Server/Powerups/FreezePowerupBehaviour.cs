using UnityEngine;
using System.Linq;

public class FreezePowerupBehaviour : ActivePowerupBehaviour {

  protected override void StartPowerupClient()
  {

  }

  protected override void EndPowerupClient()
  {

  }

  protected override void StartPowerupServer()
  {
    ActivatingAvatar.GetComponent<Rigidbody2D>().drag += 10000.0f;
    ActivatingAvatar.GetComponent<Rigidbody2D>().angularDrag += 10000.0f;
  }

  protected override void EndPowerupServer()
  {
    ActivatingAvatar.GetComponent<Rigidbody2D>().drag -= 10000.0f;
    ActivatingAvatar.GetComponent<Rigidbody2D>().angularDrag -= 10000.0f;
  }
}
