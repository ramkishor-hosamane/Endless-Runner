using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public bool jump=false;
    public bool slide=false;
    public GameObject trigger;
    public Button ResumeButton;
    public GameObject GameOverMenu;
    
    public GameObject PauseMenu;
    public Button QuitButton;

    public bool pause = false;
    public Animator anim;

    public Button PlayAgain;
    public Button Quit2;
    public float score = 0;

    public bool boost=false;
    public Rigidbody rbody;
	public CapsuleCollider myCollider;

    public int track = 0;
    public bool death=false;

    public bool sneakers = false;
    public Image gameOverImg;
    public Text bestScoreText;
    public Text scoreText;
    public Text finalScoreText;

    public float lastScore=0;
    // Start is called before the first frame update
    void Start()
    {
        pause=false;
       anim=GetComponent<Animator>(); 
       rbody = GetComponent<Rigidbody> ();
	   myCollider = GetComponent<CapsuleCollider> ();
       ResumeButton.onClick.AddListener(pauseToggle);
       QuitButton.onClick.AddListener(quit);
       Quit2.onClick.AddListener(quit);
       PlayAgain.onClick.AddListener(restart);
       lastScore = PlayerPrefs.GetFloat ("MyScore");
    }

void restart(){
Application.LoadLevel("Game");

}
    // Update is called once per frame
    void Update()
    {
        scoreText.text= "Score : " + score.ToString();
        if(Input.GetKeyUp(KeyCode.Escape)){
            Debug.Log("Pausing/Resume");
            pauseToggle();
        }
        if(pause)
            return;



        
        if (score > lastScore) {
			//bestScoreText.text ="Best Score : "+ score.ToString ();
		} else {
			//bestScoreText.text ="Your Score : "+ score.ToString ();	
		}

        if(death==true ){
            StartCoroutine(Die());
        }
        if(score>=100 && death !=true){
            transform.Translate(0,0,0.2f);
        }
        else if(score>200 && death !=true){
            transform.Translate(0,0,0.3f);
        }
        else if(death==true){
            transform.Translate(0,0,0);
        }
        else{
            transform.Translate(0,0,0.2f);

        }


        if (boost == true) {
			transform.Translate (0, 0, 0.5f);
            //myCollider.enabled = false;
	    	//rbody.isKinematic = true;

		} else {
			myCollider.enabled = true;
			rbody.isKinematic = false;
		}
        
        
        if(Input.GetKeyDown(KeyCode.W) && death!=true && jump!=true){
            jump=true;
            StartCoroutine(JumpControl());
            //Debug.Log("Up key presed");
        }

        if(death!=true)
        {
            if(Input.GetKey(KeyCode.A)){

                    transform.Translate(-0.1f, 0.0f, 0.0f);
                    if(transform.position.x<-1.4f)
                        transform.position = new Vector3(-1.4f, transform.position.y, transform.position.z);


            //Debug.Log("Left key presed");
            }   

        if(Input.GetKey(KeyCode.D)){

                transform.Translate(0.1f, 0, 0);
                if (transform.position.x > 0.9f)
                    transform.position = new Vector3(0.9f, transform.position.y, transform.position.z);
                    

            //Debug.Log("Right key presed");
            }

        if(Input.GetKey(KeyCode.S)){
            slide=true;
            StartCoroutine(SlideControl());
            }


        } 
        if(jump==true){
            anim.SetBool("isJump",jump);
            if(sneakers)
                transform.Translate(0,0.6f,0.4f);
            else
                transform.Translate(0,0.3f,0.1f);
            myCollider.height = 1.8f;
			//Debug.Log("Your Message Here");
        }
        else if(jump==false){
            anim.SetBool("isJump",jump);
            myCollider.height = 2.05f;
        }


        if(slide==true){
            anim.SetBool("isSlide",slide);
            transform.Translate(0,0,0.1f);
            myCollider.height = 1.8f;

        }
        else if(slide==false){
            anim.SetBool("isSlide",slide);
            myCollider.height = 2.05f;
            
        }
        trigger=GameObject.FindGameObjectWithTag("Obstacle");



    }
    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag=="PlayerTrigger"){
            Destroy(trigger.gameObject);

        }
        if(other.gameObject.tag=="Coin"){
            Destroy(other.gameObject,0.35f);
            score += 1f;
        }

        if(other.gameObject.tag=="DeathTag" || other.gameObject.tag=="DeathP" || other.gameObject.tag=="Deathfire" || other.gameObject.tag=="DeathStone"){
            if (boost != true)
            { 
            death=true;
            if(other.gameObject.tag=="DeathStone")
                {
                    anim.Play("Tripping");
                    anim.SetBool("isTrip", death);

                }
                else
                {
                 anim.Play("Falling Back Death");
                 anim.SetBool("isDeath", death);
                }

            transform.Translate(0,0,-0.16f);
            finalScoreText.text ="YOUR SCORE IS "+ score.ToString ();	
            if (score > lastScore) {
				PlayerPrefs.SetFloat ("MyScore", score);
			}
            }
        }
        

        if (other.gameObject.tag == "Boost") {
			Destroy (other.gameObject);
			StartCoroutine (BoostController ());
		}
        if (other.gameObject.tag == "Sneakers") {
			Destroy (other.gameObject);
			StartCoroutine (SneakerController ());
		}

    }
    
    IEnumerator BoostController(){
		boost = true;
		yield return new WaitForSeconds (3);
		boost = false;
	}
    IEnumerator SneakerController(){
		sneakers = true;
		yield return new WaitForSeconds (3);
		sneakers = false;
	}    
    IEnumerator Die()
    {

        yield return new WaitForSeconds(3);
        scoreText.gameObject.SetActive(false);
        GameOverMenu.SetActive(true);

        //gameOverImg.gameObject.SetActive(true);

    }
    
    IEnumerator SlideControl()
    {
        slide = true;
        yield return new WaitForSeconds(1f);
        slide = false;
    }

    IEnumerator JumpControl()
    {
        jump = true;

        yield return new WaitForSeconds(0.2f);
        jump = false;
    }

void pauseToggle(){
                pause = !pause;
            PauseMenu.SetActive(pause);
            anim.SetBool("isPause",pause);
}
void quit(){
        Application.LoadLevel("MainMenu");

}
}
