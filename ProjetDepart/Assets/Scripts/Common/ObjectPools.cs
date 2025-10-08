using UnityEngine;

// TODO : Ajoutez toutes les références à vos ObjectPools ici.
//        Basez-vous sur le code existant.
public class ObjectPools : MonoBehaviour
{
    [Header("Entities")]
    [SerializeField] private ObjectPool alien;
    [SerializeField] private ObjectPool bullet;

    [Header("Fx")]
    [SerializeField] private ObjectPool alienExplosion;

    // Entities
    public ObjectPool Alien => alien;
    public ObjectPool Bullet => bullet;

    // Fx
    public ObjectPool AlienExplosion => alienExplosion;
}