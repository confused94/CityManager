
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BuildPreset : ScriptableObject
{
    /*Bu script scriptableobject miras� alan ve t�m yap�lar�n �zelliklerini belirleyen soyut s�n�ft�r*/
    //---------------------------------------------------------------------//
    [Tooltip("Marketteki de�eri")]
    public int sales;
    [Tooltip("�retmeden kazan�lan para de�eri")]
    public int earning;
    [Tooltip("Harcan�lan su miktar�")]
    public int water;
    [Tooltip("Marketteki kaynak miktar�")]
    public int resource;
    [Tooltip("Harcan�lan kaynak de�eri")]
    public int useRes;
    [Tooltip("Harcan�lan enerji miktar�")]
    public int energy;
    [Tooltip("kazan�lan kaynak miktar�")]
    public int awardRes;
    [Tooltip("Kazan�lan su miktar�")]
    public int awardWater;
    [Tooltip("Kazan�lan enerji miktar�")]
    public int awardEnergy;
    [Tooltip("Kazan�lan enerji miktar�")]
    public GameObject awardsParticle;
    [Tooltip("Harcan�lan su miktar�")]
    public Sprite sprite;
    [Tooltip("Harcan�lan su miktar�")]
    public Sprite awardSprite;
   

    /*SelectObject scriptinde buton tetiklendi�inde transform bilgilerini g�nderir. G�nderilen bu bilgilerle
    Olu�turulan gameObjecti d�nd�r�r*/
    public abstract GameObject CreateObject(Vector3 position,Quaternion rotation);
    public abstract void ParticleEffectFunc(Vector3 position,Quaternion rotation);
    
}
