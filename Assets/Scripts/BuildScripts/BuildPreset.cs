
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BuildPreset : ScriptableObject
{
    /*Bu script scriptableobject mirasý alan ve tüm yapýlarýn özelliklerini belirleyen soyut sýnýftýr*/
    //---------------------------------------------------------------------//
    [Tooltip("Marketteki deðeri")]
    public int sales;
    [Tooltip("Üretmeden kazanýlan para deðeri")]
    public int earning;
    [Tooltip("Harcanýlan su miktarý")]
    public int water;
    [Tooltip("Marketteki kaynak miktarý")]
    public int resource;
    [Tooltip("Harcanýlan kaynak deðeri")]
    public int useRes;
    [Tooltip("Harcanýlan enerji miktarý")]
    public int energy;
    [Tooltip("kazanýlan kaynak miktarý")]
    public int awardRes;
    [Tooltip("Kazanýlan su miktarý")]
    public int awardWater;
    [Tooltip("Kazanýlan enerji miktarý")]
    public int awardEnergy;
    [Tooltip("Kazanýlan enerji miktarý")]
    public GameObject awardsParticle;
    [Tooltip("Harcanýlan su miktarý")]
    public Sprite sprite;
    [Tooltip("Harcanýlan su miktarý")]
    public Sprite awardSprite;
   

    /*SelectObject scriptinde buton tetiklendiðinde transform bilgilerini gönderir. GÖnderilen bu bilgilerle
    Oluþturulan gameObjecti döndürür*/
    public abstract GameObject CreateObject(Vector3 position,Quaternion rotation);
    public abstract void ParticleEffectFunc(Vector3 position,Quaternion rotation);
    
}
