using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;         //Speed the player is moving at.
    public float B_speed;       //B_speed and B_jumpSpeed are used to restore speed and jumpSpeed to there original values. 
    public float jumpSpeed;     //Intensity of player's jump.
    public float B_jumpSpeed;

    bool canJump;               //Bool used to stop the player from jumping in mid air...
    bool charging;              //Used for the charge float value.
    bool onCharge;              //Bool for checking whether or not the player is on a charge pad.

	private float jumpDelay;    //How long in seconds until the player can jump again...
    public float chargeDelay;   //How long after charge release until player can move again.
    private float charge;       //How much charge the player currently has.
    public float chargeMax;     //How much charge the player can have.
    public float chargeUp;      //Player's Charging Up rate.
    public float chargeDown;    //Player's Cooling Down rate.

    private int H_Move;         //used to determine which way Horizontally the Charge Pad will move the player.
    private int V_Move;         //used to determine which way Vertically the Charge Pad will move the player.

    private Rigidbody rb;       //Player's RigidBody Component, used for movement and charge via adding force.
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //assign RigidBody component to rb Data Type.
		canJump = true;                 //true on start, so the player can jump from the get go.
        charging = false;               //false on start since player isn't charging.      
        onCharge = false;               //false on start since player isn't on charge pad.
		jumpDelay = 1.2f;
        charge = 0;
    }
    void Update()
    {
        /* uses chargeDown to lower the value of charge when not charging and
         * sets lowest possible value of charge to 0 to avoid negative values [43-44]. 
        */
        if (!charging) charge = charge - chargeDown; //Charge01
        if (charge <= 0) charge = 0;                 //Charge02
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); //MoveH
        float moveVertical = Input.GetAxis("Vertical");     //MoveV

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //Move01

        rb.AddForce(movement * speed);                                      //Move02

        /* checks if player pressed jump and if the canJump bool is restricting them,
         * if not then AddForce is applied to the Y axis by the jumpSpeed value. canJump 
         * is set to false and the JumpRoutine is initiated, meaning that the player 
         * can't jump until the Jumproutine timer is finished [61-68] JumpRoutine [168-173].
        */
        if (Input.GetButton("Jump") && canJump) //Jump01
        {
            rb.AddForce(Vector3.up * jumpSpeed); 
			//we have jumped so dont let us do it again in the air
			canJump = false;
			//start a co routine to dictate when to turn jump back on
			StartCoroutine(JumpRoutine());
        }
        /* checks if player is pressing 'Z' and is on a charge pad, if true then the
         * player stops moving via speed values set to 0 and charge float rises
         * until it reaches chargeMax or until the player releases 'Z' [73-80].
        */ 
        if (Input.GetKey(KeyCode.Z) && onCharge) //Charge03
        {
            charging = true;
            speed = 0;
            jumpSpeed = 0;
            charge = charge + chargeUp;
            if (charge >= chargeMax) charge = chargeMax;
        }
        /*checks if player released 'Z' and is on charge pad, if true then AddForce is
         * used to launch the player in a direction depending on the pad they're on. Charging
         * is set to false to make the charge go down again and the ChargeRoutine is initiated.
         * meaning the player can't move until the ChargeRoutine timer is done [86-91] ChargeRoutine [175-186].
        */
        if (Input.GetKeyUp(KeyCode.Z) && onCharge) //Charge04
        {
            rb.AddForce(new Vector3(charge * (10 * H_Move), 0, charge * (10 * V_Move)));
            StartCoroutine(ChargeRoutine());
            charging = false;
        }
    }
    void OnTriggerEnter(Collider Other) //Pickups
    {
        //All pick ups are created in the stats script and controlled by the player manager script, other scripts such as doors will call on the player manager when using pick ups.

        //pick up yellow item and add 1 to the stats counter through the PlayerManager
		if (Other.gameObject.CompareTag("Pickup Yellow"))
          {
			PlayerManager.Get().stats.Yellow += 1;
			Other.gameObject.SetActive(false);
          }

		//pick up green item and add 1 to counter
		if (Other.gameObject.CompareTag ("Pickup Green")) 
		{
			PlayerManager.Get().stats.Green += 1;
			Other.gameObject.SetActive(false);
		}

		//pick up the END item and win the game
		if (Other.gameObject.CompareTag ("Pickup End")) 
		{
			//setting the timer to freeze, to be displayed at the end
			PlayerManager.Get ().stats.TimeFreeze = true;
			//load the Win scene due to finish condition being met
			SceneManager.LoadScene("WinScene");

		}

        
    }
    //checks if player touching an object via collision.
    void OnCollisionEnter(Collision Other)
    {
        /*Charge pads shoot in different directions based on there tags, once
         * the Pad has been identified different values are applied to the H_Move
         * and V_Move ints to move the player in their assigned direction.
         * H_Move: -1 = left, 0 = N/A, 1 = Right.
         * V_Move: -1 = Back, 0 = N/A, 1 = Forward.
         * onCharge is also set to true to enable the charge to initiate on 'Z' press.
        */
        if (Other.gameObject.CompareTag("Charge Pad Up")) //ChargePad01
        {
            onCharge = true;
            H_Move = 0;
            V_Move = 1;
        }
        if (Other.gameObject.CompareTag("Charge Pad Down")) //ChargePad02
        {
            onCharge = true;
            H_Move = 0;
            V_Move = -1;
        }
        if (Other.gameObject.CompareTag("Charge Pad Left")) //ChargePad03
        {
            onCharge = true;
            H_Move = -1;
            V_Move = 0;
        }
        if (Other.gameObject.CompareTag("Charge Pad Right")) //ChargePad04
        {
            onCharge = true;
            H_Move = 1;
            V_Move = 0;
        }

    }
    void OnCollisionExit(Collision Other)
    {
        //Upon leaving the Pad onCharge is set back to false.
        if (Other.gameObject.tag.Contains("Charge Pad")) //ChargePad05
        {
            onCharge = false;
        }
    }
    //this co-routine dictates when the jump mechanic is turned back on [168-173].
    private IEnumerator JumpRoutine()
	{
		yield return new WaitForSeconds (jumpDelay); //how long until the player can jump again.

		canJump = true; //jump is enabled again
	}
    //this co-routine dictates when the player can move again after using the charge [175-186].
    private IEnumerator ChargeRoutine()
    {
        yield return new WaitForSeconds(chargeDelay); //how long until the player can move again.
        //all movement is given back to the player.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        //backup floats are used for restoring previous values.
        speed = B_speed;
        jumpSpeed = B_jumpSpeed;
    }
}