using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DBStruct
{
    [Serializable]
    public class WaveData
    {
        public int wave;
        public int[] num = new int[3];

        public WaveData(string[] data)
        {
            int count = 0;
            this.wave = int.Parse(data[count++]);
            for(int i = 0; i < num.Length; i++)
            {
                this.num[i] = int.Parse(data[count++]);
            }
        }
    }
}

public class DBManager : MonoBehaviour
{
    public static DBManager instance = null;
    public List<DBStruct.WaveData> waveDatas = new List<DBStruct.WaveData>();
    private const string path = "/WaveDB.txt";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadNextWave(int fileLine)
    {
        string[] waveDB = File.ReadAllLines(Application.streamingAssetsPath + path);
        waveDatas.Add(new DBStruct.WaveData(waveDB[fileLine].Split(',')));
    }
}
