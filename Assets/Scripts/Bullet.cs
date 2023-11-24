using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision objectWeHit)
    {

        if (objectWeHit.gameObject.CompareTag("GreenTarget"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            gameManager.AddPoints(500);
            Destroy(objectWeHit.gameObject);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("RedTarget"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            gameManager.AddPoints(50);
            Destroy(objectWeHit.gameObject);
            Destroy(gameObject);
        }


        if (objectWeHit.gameObject.CompareTag("MilitaryTarget"))
        {
            print("hit " + objectWeHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectWeHit);
            gameManager.AddPoints(50);
            Destroy(objectWeHit.gameObject);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall!");
            CreateBulletImpactEffect(objectWeHit);
            Destroy(gameObject);
        }

        if (objectWeHit.gameObject.CompareTag("Beer"))
        {
            print("hit a beer!");
            gameManager.AddPoints(100);
            objectWeHit.gameObject.GetComponent<BeerBottle>().Shatter();
        }


        if (objectWeHit.gameObject.CompareTag("Zombie"))
        {
            gameManager.AddPoints(500);
            objectWeHit.gameObject.GetComponent<Enemy>().TakeDamage(30);

        }
    }

    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point,
            Quaternion.LookRotation(contact.normal)
            );

        hole.transform.SetParent(objectWeHit.gameObject.transform);

    }
}
