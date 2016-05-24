using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	//Velocidad
	public float speed = 0f;
	private float speedLadder = 0f;
	private float movex = 0f;
	private float movey = 0f;
	private float jumpForce = 100f;
	//states
	private bool jump = false;
	private bool onLadder = false;
	private bool hitting = false;
	//References
	private Rigidbody2D rb2d;
	private GameObject hit1;
	//Timers
	private float timerHit1 = 3;
	private float timerHit = 0;
	//GUI
	public Text aux;
	//Thirds

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		hit1 = GameObject.Find("Player_hit1");
		hit1.GetComponent<Renderer>().enabled = false;
		aux.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		aux.text = "Time: "+Time.time;
		if(Input.GetKey(KeyCode.LeftArrow)){
			movex = -1;
			GetComponent<Animator>().SetBool("Run",true);
			GetComponent<SpriteRenderer>().flipX = true;
		}else if(Input.GetKey(KeyCode.RightArrow)){
			movex = 1;
			GetComponent<Animator>().SetBool("Run",true);
			GetComponent<SpriteRenderer>().flipX = false;
		}else{
			GetComponent<Animator>().SetBool("Run",false);
			movex = 0;
		}
		if(Input.GetKeyDown(KeyCode.Z) && !onLadder){
			jump = true;
		}

		if(onLadder){
			rb2d.gravityScale = 0f;
			if(Input.GetKey(KeyCode.UpArrow)){
				speedLadder = 3f;
			}else if(Input.GetKey(KeyCode.DownArrow)){
				speedLadder = -3f;
			}else{
				speedLadder = 0f;
			}
		}else{
			rb2d.gravityScale = 1f;
		}

		if(Input.GetKeyDown(KeyCode.X)){
			timerHit = timerHit1 + Time.time;

		}
	}

	void FixedUpdate(){
		float velaux = rb2d.velocity.y;
		if(onLadder) velaux = speedLadder;
		rb2d.velocity = new Vector2(movex * speed,velaux);
		if(jump){
			rb2d.AddForce(new Vector2(0f,jumpForce));
			jump = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Ladder"){
			onLadder = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Ladder"){
			onLadder = false;
		}
	}
}
