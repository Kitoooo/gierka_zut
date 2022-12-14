using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D m_Body;
    public float range = 1000.0f;
    [SerializeField] 
    public float damage = 10f;
    [SerializeField] 
    public float speed = 300;
    [SerializeField]
    protected GameObject m_ProjectileContactBehaviourPrefab;

    private string[] m_TagsToIgnore = { "Player","PlayerProjectile","Weapon" };
    public string[] TagsToIgnore { get { return m_TagsToIgnore; } }
   
    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(Vector2 direction)
    {
        m_Body.AddForce(direction * speed);
    }

    //check if not owner
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with: "+ other.gameObject.tag);
        if (m_TagsToIgnore.Contains(other.gameObject.tag))
        {
            return;
        }
        else 
        {
            GameObject projectileContactBehaviour = Instantiate(m_ProjectileContactBehaviourPrefab, transform.position, Quaternion.identity);

            ProjectileContact contactBehaviour = projectileContactBehaviour.GetComponent<ProjectileContact>();
            contactBehaviour.OnContact(this, other);
        }
    }
}
