using UnityEngine;

public class ChassisController : MonoBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
	}
	
    public void FuncChassis()
    {
        anim.SetTrigger("Chassis");
    }
}
