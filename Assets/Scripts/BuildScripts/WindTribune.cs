
using UnityEngine;

[CreateAssetMenu(fileName ="WindTribune",menuName ="Windtribune")]
public class WindTribune : BuildPreset
{
    public GameObject obj;
    public override GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        GameObject newObject = Instantiate(obj, position, rotation);
        return newObject;


    }

    public override void ParticleEffectFunc(Vector3 position, Quaternion rotation)
    {
        GameObject newParticle = Instantiate(awardsParticle, position, rotation);
        Destroy(newParticle, 0.3f);
    }
}
