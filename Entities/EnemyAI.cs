using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour 
{
    public AudioClip[] Audios;
    public LayerMask playerLayer;
    public AudioSource source;
    private NavMeshAgent agent;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start() {
        StartCoroutine(GetCloser(3.5f));
    }   

    IEnumerator GetCloser(float s)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach(var player in players)
        {            
            if(target == null)
                target = player.transform;

            float dist = Vector3.Distance(transform.position, player.transform.position);
            float distToTarget = Vector3.Distance(transform.position, target.position);

            target = dist < distToTarget ? player.transform : target;
        }

        yield return new WaitForSeconds(s);        
        StartCoroutine(GetCloser(s));
    }

    Transform target;

    private void Update() {
        if(target != null)
            agent.destination = target.position;
        else
            agent.destination = transform.position;

        var players  = Physics.OverlapSphere(transform.position, 10, playerLayer);
        
        if(players.Length > 0)
        {
            if(source.isPlaying == false)
            {
                source.PlayOneShot(Audios[Random.Range(0, Audios.Length)]);
                isNear = true;
            }

            float oDist = 0;

            foreach(var player in players)
            {
                float dist = Vector3.Distance(player.transform.position, transform.position);
                if(dist <= oDist || dist == 0)
                {
                    oDist = dist;
                    target = player.transform;
                }
            }
        }
        else
        {
            isNear = false;
        }

        if(isNear == false) target = null;
    }

    [SerializeField] bool isNear = false;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.transform.tag);

        if(other.transform.CompareTag("Player"))
        {
            var player = other.gameObject;
            player.GetComponent<Rigidbody>().AddForce(Vector3.up*15, ForceMode.Force);
        }
    }
}