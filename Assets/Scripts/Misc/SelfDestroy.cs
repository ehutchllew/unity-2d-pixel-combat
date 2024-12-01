using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    public void DestroySelfAnimEvent()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (ps && !ps.IsAlive())
        {
            DestroySelfAnimEvent();
        }
    }
}
