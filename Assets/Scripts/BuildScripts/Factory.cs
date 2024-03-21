
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName ="Factory",menuName ="Factory")]
public class Factory : BuildPreset
{
    public GameObject obj;
    
    public override GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        GameObject newObject=Instantiate(obj, position, rotation);
        return newObject;
    }

    public override void ParticleEffectFunc(Vector3 position, Quaternion rotation)
    {
        GameObject newObject=Instantiate(awardsParticle, position, rotation);
        Destroy(newObject, 0.3f);
    }
}


