using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] kunai;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //pool kunai

        //every time the player attacks, we reset the kunai's position
        kunai[FindKunai()].transform.position = firePoint.position;
        kunai[FindKunai()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindKunai()
    {   
        for(int i = 0; i < kunai.Length; i++)
        {
            if(!kunai[i].activeInHierarchy) // if the kunai[i] is not active, we can use it // "hey, kunai nr 3 is not active so you can use it"
                return i;
        }
        return 0;
    }
}
 