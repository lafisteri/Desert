using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[SerializeField]
	private float speed = 4.0F;

	[SerializeField]
	private float jumpForce = 15.0F;

	[SerializeField]
	private Transform groundCheck;

	[SerializeField]
	private LayerMask whatIsGround;

	private float groundRadius;
	private bool isGrounded;

	[SerializeField]
	private int lives = 5;
	private UILivesBar livesBar;
	public int Lives
	{
		get
		{ 
			return lives; 
		}
		set
		{
			livesBar.Refresh ();
			if (value < 5)
			{
				lives = value;

			}
		}
	}

	private int score = 0;
	public int Score {
		get{
			return score;
		}
		set{
			score = value;
		}
	}

	bool lookRight;



	private CharState State {
		get {
			return (CharState)1;
			//return (CharState)animator.GetInteger ("State");
		}
		set { 
			//animator.SetInteger ("State", (int)value);
		}
	}

	private Rigidbody2D rigidBody;
	//private Animator animator;
	//private SpriteRenderer sprite;//инвертировать анимацию при движении влево - вправо свойство Flip
	//private Bullet bullet;


	void Start () {
		isGrounded = false;
		groundRadius = 0.35F;
		lookRight = true;
	}

	private void Awake()
	{
		livesBar = FindObjectOfType<UILivesBar>();//link to LivesBar for change hearts count
		rigidBody = GetComponent<Rigidbody2D> ();
		//animator = GetComponent<Animator> ();
		//sprite = GetComponentInChildren<SpriteRenderer> ();//спрайт дочерний элемент

		//bullet = Resources.Load<Bullet> ("Bullet");//класс и название префаба
	}
	private void Update()
	{
		if (isGrounded) 
			State = CharState.Idle;

		if (Input.GetButton ("Horizontal")) 
			Run (Input.GetAxis("Horizontal"));

		if (isGrounded && Input.GetButtonDown ("Jump")) 
			Jump ();

		//if(Input.GetButton("Fire1"))
		//	Shoot();
	}
	private void FixedUpdate()
	{
		CheckGround ();
	}

	public void Run(float side)
	{
		Vector3 direction = transform.right * side;
		transform.position = Vector3.MoveTowards (transform.position, transform.position + direction, speed * Time.deltaTime);

		if (side < 0 && lookRight)
			Flip ();
		if (side > 0 && !lookRight)
			Flip ();

		if (isGrounded) {
			State = CharState.Run;
		}
	}

	public void Flip()
	{
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
	}

	public void Jump()
	{
		if (isGrounded) {
			rigidBody.AddForce (transform.up * jumpForce, ForceMode2D.Impulse);
		} 
	}

	private void CheckGround()
	{
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (!isGrounded) {//если не стоит на земле значит анимация прыжка
			State = CharState.Jump;
		}
	}

	public void ReCreate()
	{
		gameObject.transform.position = Vector3.zero;
	}





	public void Attack()
	{
		Debug.Log ("shoot");
		/*
		Vector3 position = transform.position;
		position.y += 0.5F;
		position.x += (sprite.flipX ? -0.5F : 0.5F);
		Bullet newBullet = Instantiate (bullet, position, bullet.transform.rotation) as Bullet;
		newBullet.Parent = gameObject;
		newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);//направление куда смотрит персонаж
		*/

	}
	/*
	public override void ReceiveDamage (float side)
	{
		//Lives--;
		rigidBody.velocity = Vector3.zero;
		rigidBody.AddForce (transform.up * 7.0F, ForceMode2D.Impulse);
		rigidBody.AddForce (transform.right * (side * 2), ForceMode2D.Impulse); //нужно отбрасывать в ту сторону с какой подошел, как вариант сделать 2 коллайдера для обстакл
		Debug.Log ("lives- "+lives);
	}

	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Bullet bullet = collider.gameObject.GetComponent<Bullet> ();
		if (bullet && bullet.Parent != gameObject)
		{
			ReceiveDamage (bullet.transform.position.x - transform.position.x > 0 ? -1.0F : 1.0F);//monster - character
		}
	}
*/

}
public enum CharState
{
	Idle,
	Run,
	Jump
}