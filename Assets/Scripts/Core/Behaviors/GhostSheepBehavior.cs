using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GhostSheepBehavior : AgentBehaviour
{
    public Boolean isAttacking;
    public Player playerBeingChased;
    
    private const float MIN_TIME_SHEEP = 14f;
    private const float MAX_TIME_SHEEP = 15f;
    private const float MIN_TIME_GHOST = 10f;
    private const float MAX_TIME_GHOST = 15f;
    private const float MIN_DIST_FOR_REACTION_GHOST = 20f;
    private const float MIN_DIST_FOR_REACTION_SHEEP = 6f;

    private float MIN_DIST_FOR_REACTION = 6f;

    public int player1Score;
    public int player2Score;

    public AudioSource source;
    public AudioClip beeeehhhhh;
    public AudioClip wooooofff;
    public AudioClip getAttacked;
    
    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;

    
    public GameObject GetPlayer1 => _player1; 
    public GameObject GetPlayer2 => _player2;

    public CelluloAgentRigidBody agent1;
    public CelluloAgentRigidBody agent2;
    
    public void Start()
    {
        gameObject.tag = "GhostSheep";
        
        isAttacking = false;
        MIN_DIST_FOR_REACTION = MIN_DIST_FOR_REACTION_SHEEP;
        float time = UnityEngine.Random.Range(MIN_TIME_SHEEP, MAX_TIME_SHEEP);
        agent.SetVisualEffect(0, Color.green, 100);
    }

    private void transformIntoGhost()
    {
        //enabling move on stone feeling for the 2 players 
        agent1.MoveOnStone();
        agent2.MoveOnStone();
        
        isAttacking = true;
        float time = UnityEngine.Random.Range(MIN_TIME_GHOST, MAX_TIME_GHOST);
        agent.SetVisualEffect(0, Color.red, 100);
        source.PlayOneShot(wooooofff);
        
        Invoke("transformIntoSheep",time);
    }

    private void transformIntoSheep()
    {
        if(!isAttacking)
            return;
        //gets rid of move on ice feeling from ghost mode
        agent1.ClearHapticFeedback();
        agent2.ClearHapticFeedback();
        //enables back drivability for the 2 players
        agent1.SetCasualBackdriveAssistEnabled(true);
        agent2.SetCasualBackdriveAssistEnabled(true);

        isAttacking = false;
//        MIN_DIST_FOR_REACTION = MIN_DIST_FOR_REACTION_SHEEP;
  //      float time = UnityEngine.Random.Range(MIN_TIME_SHEEP, MAX_TIME_SHEEP);
        this.agent.SetVisualEffect(0, Color.green, 100);
        source.PlayOneShot(beeeehhhhh);
        //Invoke("transformIntoGhost",time);
        
    }
    
    /*
     * public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dog");
        Vector3 movementVector3 = new Vector3(0f, 0f, 0f);
        
        players = players.Where(p =>
                Vector3.Distance(p.transform.position, transform.position) < MIN_DIST_FOR_REACTION)
            .ToArray();
        //if there's no players around = just stay in the same point
        if (players.Length == 0)
            return steering;
        //the sheep is running away
        if (!isAttacking)
            foreach (GameObject player in players)
                movementVector3 += transform.position - player.transform.position;
        //sheep following closest to it player
        else
        {
            //implementation of some shit code to find closest player
            float minDist = players.Min(p => Vector3.Distance(p.transform.position , transform.position));
            players = players.Where(p =>
                    Vector3.Distance(p.transform.position , transform.position) <= minDist)
                .ToArray();
            movementVector3 = players[0].transform.position - transform.position;
        }

        steering.linear = (movementVector3 /(float)players.Length) * (agent.maxAccel / 3f) ; //
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear , agent.maxAccel)) ;
        return steering;
    }

*/
    
    void OnCollisionEnter(Collision col)
    {
        if (isAttacking)
        {
            if (col.gameObject.name == _player1.name && playerBeingChased == Player.player1)
            {
                --player1Score;
                source.PlayOneShot(getAttacked);
                transformIntoSheep();
            }
            else if (col.gameObject.name == _player2.name && playerBeingChased == Player.player2)
            {
                --player2Score;
                source.PlayOneShot(getAttacked);
                transformIntoSheep();
            }
        }
    }

    public GameObject FindClosestPlayer()
    {
        Vector3 pos1 = _player1.transform.position;
        Vector3 pos2 = _player2.transform.position;
        Vector3 posGS = this.transform.position;
        GameObject closestPlayer;
        if ((posGS - pos1).magnitude <= (posGS - pos2).magnitude) closestPlayer = _player1;
        else closestPlayer = _player2;
        
        return closestPlayer;
    }

    public void chasePlayer(Player player)
    {
        transformIntoGhost();
        playerBeingChased = player == Player.player1 ? Player.player2 : Player.player1;
    }
}
