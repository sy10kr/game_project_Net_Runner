//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UnityChan
{
// 必要なコンポーネントの列記
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class UnityChanControlScriptWithRgidBody : MonoBehaviour
	{

		public float animSpeed = 1.5f;				// アニメーション再生速度設定
		public float lookSmoother = 3.0f;			// a smoothing setting for camera motion
		public bool useCurves = true;				// Mecanimでカーブ調整を使うか設定する
		// このスイッチが入っていないとカーブは使われない
		public float useCurvesHeight = 0.5f;		// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

		// 以下キャラクターコントローラ用パラメタ
		// 前進速度
		public float forwardSpeed = 7.0f;
		// 後退速度
		public float backwardSpeed = 2.0f;
		// 旋回速度
		public float rotateSpeed = 2.0f;
		// ジャンプ威力
		public float jumpPower = 3.0f; 
		// キャラクターコントローラ（カプセルコライダ）の参照
		private CapsuleCollider col;
		private Rigidbody rb;
		// キャラクターコントローラ（カプセルコライダ）の移動量
		private Vector3 velocity;
		// CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
		private float orgColHight;
		private Vector3 orgVectColCenter;
		private Animator anim;							// キャラにアタッチされるアニメーターへの参照
		private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照


        private GameObject cameraObject;	// メインカメラへの参照

        public GameObject ProgressObject;
        private ProgressBar Pb;
        public GameObject SkillObject;
        private SkillCheck SkillScript;

        //Init_collider 초기화용 오브젝트 
        public GameObject Init_ColliderObject = null;

        // アニメーター各ステートへの参照
        static int idleState = Animator.StringToHash ("Base Layer.Idle");
		static int locoState = Animator.StringToHash ("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash ("Base Layer.Jump");
		static int restState = Animator.StringToHash ("Base Layer.Rest");

		    public float speed = 3f;
    public bool space_flag = false;
    public float power = 0f;
    public float skill_num = 0;

    //포탈쓴게 처음인지 확인하는용
    int portal_check = 0;

	//시간세기용
	float zeroTime = 0f;

    //각종 총알 오브젝트, 총쏘는위치, 총 이펙트
    [SerializeField] GameObject Bullet_hacking = null;
    [SerializeField] GameObject Bullet_portal = null;
    [SerializeField] GameObject Bullet_stop = null;
	[SerializeField] GameObject Bullet_riding = null;

    [SerializeField] Transform Fire_pos = null;
    [SerializeField] GameObject Fire_effect = null;

    //좀비 오브젝트, 소환 위치, 소환 이펙트
    [SerializeField] GameObject Zombie = null;
    [SerializeField] Transform Zombie_pos = null;
    [SerializeField] GameObject Zombie_effect = null;

    //포탈 소환 이펙트
    [SerializeField] GameObject Portal_effect = null;

    //카메라 및 컨트롤 제어
    [SerializeField] CameraControl camera = null;

		// 初期化
		void Start ()
		{
			// Animatorコンポーネントを取得する
			anim = GetComponent<Animator> ();
			// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
			col = GetComponent<CapsuleCollider> ();
			rb = GetComponent<Rigidbody> ();
            Pb = ProgressObject.GetComponent<ProgressBar>();
            SkillScript = SkillObject.GetComponent<SkillCheck>();
            //メインカメラを取得する
            cameraObject = GameObject.FindWithTag ("MainCamera");
            

			// CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}
	
	
		// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
		void FixedUpdate ()
		{
			zeroTime += Time.deltaTime;
			if(camera.control == 0)
			{

				float h = Input.GetAxis ("Horizontal");				// 入力デバイスの水平軸をhで定義
				float v = Input.GetAxis ("Vertical");				// 入力デバイスの垂直軸をvで定義
				anim.SetFloat ("Speed", v);							// Animator側で設定している"Speed"パラメタにvを渡す
				anim.SetFloat ("Direction", h); 						// Animator側で設定している"Direction"パラメタにhを渡す
				anim.speed = animSpeed;								// Animatorのモーション再生速度に animSpeedを設定する
				currentBaseState = anim.GetCurrentAnimatorStateInfo (0);	// 参照用のステート変数にBase Layer (0)の現在のステートを設定する
				rb.useGravity = true;//ジャンプ中に重力を切るので、それ以外は重力の影響を受けるようにする
			

				// 以下、キャラクターの移動処理
				velocity = new Vector3 (0, 0, v);		// 上下のキー入力からZ軸方向の移動量を取得
				// キャラクターのローカル空間での方向に変換
				velocity = transform.TransformDirection (velocity);
				//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する


				if (v > 0.1) {
					velocity *= forwardSpeed;		// 移動速度を掛ける
				} else if (v < -0.1) {
					velocity *= backwardSpeed;	// 移動速度を掛ける
				}
			
				if (Input.GetButtonDown ("Jump")) {	// スペースキーを入力したら

					//アニメーションのステートがLocomotionの最中のみジャンプできる
					if (currentBaseState.nameHash == locoState) {
						//ステート遷移中でなかったらジャンプできる
						if (!anim.IsInTransition (0)) {
							rb.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
							anim.SetBool ("Jump", true);		// Animatorにジャンプに切り替えるフラグを送る
						}
					}
				}

				if (Input.GetMouseButtonDown(0))
				{
					//해킹총알
					if(skill_num == 1)
					{
                        Pb.ProgressControl(10);
                        GameObject Instance_bullet = Instantiate(Bullet_hacking, Fire_pos.position, Fire_pos.rotation);
						GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

						Destroy(Instance_effect, 0.05f);
					}
					//포탈총알
					if(skill_num == 3)
					{
                        Pb.ProgressControl(15);
                        GameObject Instance_bullet = Instantiate(Bullet_portal, Fire_pos.position, Fire_pos.rotation);
						GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

						Destroy(Instance_effect, 0.05f);
					}
					if(skill_num == 4)
					{
                        Pb.ProgressControl(15);
                        GameObject Instance_bullet = Instantiate(Bullet_stop, Fire_pos.position, Fire_pos.rotation);
						GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

						Destroy(Instance_effect, 0.05f);
					}

				}

				//1번 스킬 - 해킹
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					skill_num = 1;
				}
				//2번 스킬 - 좀비소환
				if (Input.GetKeyDown(KeyCode.Alpha2))
				{
                    Pb.ProgressControl(5);
                    skill_num = 2;
					Vector3 edit_pos;
					edit_pos = Zombie_pos.position;
					edit_pos.z += 1;
					edit_pos.x -= 0.5f;
					GameObject Instance_Zombie = Instantiate(Zombie, edit_pos, gameObject.transform.rotation);
					GameObject Instance_effect = Instantiate(Zombie_effect, Zombie_pos.position, Zombie_pos.rotation);

					Destroy(Instance_Zombie, 5f);
					Destroy(Instance_effect, 5f);
				}
				//3번 스킬 - 포탈소환
				if (Input.GetKeyDown(KeyCode.Alpha3) && SkillScript.skill_state_list[2] == 1 )
				{
					skill_num = 3;
					Vector3 edit_pos; 
					edit_pos = Zombie_pos.position;
					edit_pos.y += 1.5f;
					//edit_pos.x += 1;
					GameObject Instance_effect = Instantiate(Portal_effect, edit_pos, Zombie_pos.rotation);
					Destroy(Instance_effect, 1f);
					GameObject.Find("Portal").transform.position = edit_pos;
					GameObject.Find("Portal").transform.rotation = Fire_pos.rotation;
					if(portal_check == 0)
					{
						GameObject.Find("Portal").GetComponent<BoxCollider>().enabled = false;
						portal_check = 1;
					}
				}
				//4번 스킬 - 정지
				if (Input.GetKeyDown(KeyCode.Alpha4) && SkillScript.skill_state_list[2] == 1)
				{
					skill_num = 4;
				}
				//5번 스킬 - 무적
				if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					skill_num = 5;
					Pb.ProgressControl(-100);
				}

				//F키 누르면 앞에 있는 드론 탑승
				if (Input.GetKey(KeyCode.F))
				{
					GameObject Instance_bullet = Instantiate(Bullet_riding, Fire_pos.position, Fire_pos.rotation);
				}
			

				// 上下のキー入力でキャラクターを移動させる
				transform.localPosition += velocity * Time.fixedDeltaTime;

				// 左右のキー入力でキャラクタをY軸で旋回させる
				transform.Rotate (0, h * rotateSpeed, 0);	
		

				// 以下、Animatorの各ステート中での処理
				// Locomotion中
				// 現在のベースレイヤーがlocoStateの時
				if (currentBaseState.nameHash == locoState) {
					//カーブでコライダ調整をしている時は、念のためにリセットする
					if (useCurves) {
						resetCollider ();
					}
				}
				// JUMP中の処理
				// 現在のベースレイヤーがjumpStateの時
				else if (currentBaseState.nameHash == jumpState) {
					//cameraObject.SendMessage ("setCameraPositionJumpView");	// ジャンプ中のカメラに変更
					// ステートがトランジション中でない場合
					if (!anim.IsInTransition (0)) {
					
						// 以下、カーブ調整をする場合の処理
						if (useCurves) {
							// 以下JUMP00アニメーションについているカーブJumpHeightとGravityControl
							// JumpHeight:JUMP00でのジャンプの高さ（0〜1）
							// GravityControl:1⇒ジャンプ中（重力無効）、0⇒重力有効
							float jumpHeight = anim.GetFloat ("JumpHeight");
							float gravityControl = anim.GetFloat ("GravityControl"); 
							if (gravityControl > 0)
								rb.useGravity = false;	//ジャンプ中の重力の影響を切る
											
							// レイキャストをキャラクターのセンターから落とす
							Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
							RaycastHit hitInfo = new RaycastHit ();
							// 高さが useCurvesHeight 以上ある時のみ、コライダーの高さと中心をJUMP00アニメーションについているカーブで調整する
							if (Physics.Raycast (ray, out hitInfo)) {
								if (hitInfo.distance > useCurvesHeight) {
									col.height = orgColHight - jumpHeight;			// 調整されたコライダーの高さ
									float adjCenterY = orgVectColCenter.y + jumpHeight;
									col.center = new Vector3 (0, adjCenterY, 0);	// 調整されたコライダーのセンター
								} else {
									// 閾値よりも低い時には初期値に戻す（念のため）					
									resetCollider ();
								}
							}
						}
						// Jump bool値をリセットする（ループしないようにする）				
						anim.SetBool ("Jump", false);
					}
				}
				// IDLE中の処理
				// 現在のベースレイヤーがidleStateの時
				else if (currentBaseState.nameHash == idleState) {
					//カーブでコライダ調整をしている時は、念のためにリセットする
					if (useCurves) {
						resetCollider ();
					}
					// スペースキーを入力したらRest状態になる
					if (Input.GetButtonDown ("Jump")) {
						anim.SetBool ("Rest", true);
					}
				}
				// REST中の処理
				// 現在のベースレイヤーがrestStateの時
				else if (currentBaseState.nameHash == restState) {
					//cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
					// ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
					if (!anim.IsInTransition (0)) {
						anim.SetBool ("Rest", false);
					}
				}
			}
			
		}

		void OnGUI ()
		{
			//GUI.Box (new Rect (Screen.width - 260, 10, 250, 150), "Interaction");
			//GUI.Label (new Rect (Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forwald/Go Back");
			//GUI.Label (new Rect (Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
			//GUI.Label (new Rect (Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
			//GUI.Label (new Rect (Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
			//GUI.Label (new Rect (Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
			//GUI.Label (new Rect (Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");
		}


		// キャラクターのコライダーサイズのリセット関数
		void resetCollider ()
		{
			// コンポーネントのHeight、Centerの初期値を戻す
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Trap" )
            {
				Init_ColliderObject = collision.gameObject;
                Debug.Log("Damage");
                anim.Play("Damage");
				Pb.ProgressControl(5);

				//해당 함정이나 총알 충돌체크 1초간 정지후 1초뒤에 다시 원상태로
				collision.gameObject.GetComponent<BoxCollider>().enabled = false;
				Invoke("Init_Collider", 1.0f);

				GameObject.Find("RedScreen").transform.position = GameObject.Find("Main Camera").transform.position;
				GameObject.Find("RedScreen").transform.rotation = GameObject.Find("Main Camera").transform.rotation;
            	Invoke("Init_Screen", 0.3f);
                Debug.Log(Pb.BarValue);

            }
            if(collision.gameObject.tag == "Bullet")
            {
				Init_ColliderObject = collision.gameObject;
                Debug.Log("Damage");
                anim.Play("Damage");
				Pb.ProgressControl(5);

				GameObject.Find("RedScreen").transform.position = GameObject.Find("Main Camera").transform.position;
				GameObject.Find("RedScreen").transform.rotation = GameObject.Find("Main Camera").transform.rotation;
            	Invoke("Init_Screen", 0.3f);
                Debug.Log(Pb.BarValue);

            }
            if(collision.gameObject.tag == "Enemy")
            {
				Pb.ProgressControl(20);
            }
            if (collision.gameObject.tag == "Ice")
            {
                SceneManager.LoadScene("game_clear");
            }
            if (collision.gameObject.tag == "Heal")
            {
				Pb.ProgressControl(-20);
            }
			if(collision.gameObject.tag == "Die")
            {
                Debug.Log("Die");
                anim.Play("Die");
                SceneManager.LoadScene("game_over");
            }
        }
		void OnCollisionExit(Collision collision)
		{
			rb.velocity = new Vector3(0, 0, 0);
		}
		public void Init_Screen()
		{
			//멀리 보내기
			Vector3 go_away;
			go_away = GameObject.Find("RedScreen").transform.position;
			go_away.x += 10000;
			go_away.y += 10000;
			go_away.z += 10000;
			GameObject.Find("RedScreen").transform.position = go_away;
		}
		public void Init_Collider()
		{
			//함정이나 총알 컬라이더 리셋
			Init_ColliderObject.GetComponent<BoxCollider>().enabled = true;
		}

	}
}