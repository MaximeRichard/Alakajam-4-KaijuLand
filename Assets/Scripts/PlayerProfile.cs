using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerProfile : MonoBehaviour
{

    public static PlayerProfile Instance;

    public static int CurrentLevel { get; internal set; }
    //public enum AATypes
    //{
    //    none,
    //    x2,
    //    x4,
    //    x8,
    //    x16
        
    //}
    public PostProcessProfile postProfile;
    public int Money { get; set; }
    private bool bloomEnabled;
    public bool BloomEnabled
    {
        get
        {
            return bloomEnabled;
        }
        set
        {
            bloomEnabled = value;
            postProfile.GetSetting<Bloom>().enabled.value = value;
        }
    }

    private bool ambiantOclusionEnabled;
    public bool AmbiantOclusionEnabled
    {
        get
        {
            return ambiantOclusionEnabled;
        }
        set
        {
            ambiantOclusionEnabled = value;
            postProfile.GetSetting<AmbientOcclusion>().enabled.value = value;
        }
    }
    //private AATypes currentAAType;
    //public AATypes CurrentAAType
    //{
    //    get
    //    {
    //        return currentAAType;
    //    }
    //    set
    //    {
    //        postProfile.GetSetting<FastApproximateAntialiasing>().enabled.value = value != AATypes.none;
    //    }
    //}

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Load();
    }

    public void Load()
    {
        CurrentLevel = PlayerPrefs.GetInt("money", 0);
        CurrentLevel = PlayerPrefs.GetInt("currentLevel", 0);
        BloomEnabled = PlayerPrefs.GetInt("bloomEnabled", 1) == 1;
        AmbiantOclusionEnabled = PlayerPrefs.GetInt("aoEnabled", 1) == 1;
    }

    private void OnDestroy()
    {
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("money", Money);
        PlayerPrefs.SetInt("currentLevel", CurrentLevel);
        PlayerPrefs.GetInt("bloomEnabled", BloomEnabled ? 1 : 0);
        PlayerPrefs.GetInt("aoEnabled", AmbiantOclusionEnabled ? 1 : 0);
    }

    //public void SetAA(AATypes type)
    //{
    //    currentAAType = type;

    //}

    public void SetBloom(bool b)
    {
        BloomEnabled = b;

    }

    public void SetAmbiant(bool b)
    {
        AmbiantOclusionEnabled = b;

    }
}
