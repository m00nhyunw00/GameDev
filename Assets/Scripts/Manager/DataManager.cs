using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerData {
    public float PlayerPosX;
    public float PlayerPosY;
    // public int[] item = new int[6];
}

/** [Manager.Data.*] **/
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // Manager.Data.nowPlayer.*
    public PlayerData nowPlayer = new PlayerData();

    private string data_path;
    private string filename = "save";
    private bool isLoad = false;

    /***
        DataManager.Instance.SaveData()
        데이터 저장
    ***/
    public void SaveData() {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(data_path + filename, data);
        Debug.Log(data_path + filename);
    }

    /***
        DataManager.Instance.LoadData()
        데이터 불러오기
    ***/
    public void LoadData() {
        try {
            string data = File.ReadAllText(data_path + filename);
            Debug.Log(data);

            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            isLoad = true;
            Debug.Log(data_path + filename);
        } catch(Exception e) {
            isLoad = false;
            Debug.Log(e);
        }
    }

    /**
        데이터 로딩 여부 반환
    **/
    public bool getIsLoad() {
        return isLoad;
    }
 
    /**
        데이터 저장 경로 설정
        Manager -> Awake()에서 설정
    **/
    public void setDataPath(string path) {
        data_path = path;
    }
}
