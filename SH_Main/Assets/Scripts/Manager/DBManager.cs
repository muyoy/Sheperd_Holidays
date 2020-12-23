using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DBStruct
{
    public enum AttackType { Single, Multiple };

    [Serializable]
    public class WaveData
    {
        public int wave;
        public int[] num = new int[3];

        public WaveData(string[] data)
        {
            int count = 0;
            this.wave = int.Parse(data[count++]);
            for (int i = 0; i < num.Length; i++)
            {
                this.num[i] = int.Parse(data[count++]);
            }
        }
    }

    [Serializable]
    public class SheepData
    {
        public string name;
        public float atkRange;
        public int cost;
        public float waitingTime;
        public AttackType type;
        public float hp;
        public float atk;
        public float rangeAtk;
        public float atkDelay;
        public float moveSpeed;

        public SheepData(string[] data)
        {
            int count = 0;
            this.name = data[count++];
            this.atkRange = float.Parse(data[count++]);
            this.cost = int.Parse(data[count++]);
            this.waitingTime = float.Parse(data[count++]);
            this.type = (AttackType)Enum.Parse(typeof(AttackType), data[count++]);
            this.hp = float.Parse(data[count++]);
            this.atk = float.Parse(data[count++]);
            this.rangeAtk = float.Parse(data[count++]);
            this.atkDelay = float.Parse(data[count++]);
            this.moveSpeed = float.Parse(data[count++]);
        }
    }

    [Serializable]
    public class WolfData
    {
        public int num;
        public string name;
        public float atkRange;
        public AttackType type;
        public float hp;
        public float atk;
        public float atkDelay;
        public float moveSpeed;
        public WolfData(string[] data)
        {
            int count = 0;
            this.num =int.Parse(data[count++]);
            this.name = data[count++];
            this.atkRange = float.Parse(data[count++]);
            this.type = (AttackType)Enum.Parse(typeof(AttackType), data[count++]);
            this.hp = float.Parse(data[count++]);
            this.atk = float.Parse(data[count++]);
            this.atkDelay = float.Parse(data[count++]);
            this.moveSpeed = float.Parse(data[count++]);
        }
    }
}

public class DBManager : MonoBehaviour
{
    public static DBManager instance = null;
    public List<DBStruct.WaveData> waveDatas = new List<DBStruct.WaveData>();
    public List<DBStruct.SheepData> SheepDatas = new List<DBStruct.SheepData>();
    public List<DBStruct.WolfData> WolfDatas = new List<DBStruct.WolfData>();
    private const string wavePath = "/WaveDB.txt";
    private const string sheepUnitPath = "/SheepUnitDB.txt";
    private const string wolfUnitPath = "/WolfUnitDB.txt";

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
        LoadWolfDB(wolfUnitPath);
    }

    private void Start()
    {
        LoadSheepDB(sheepUnitPath);
    }

    private void LoadSheepDB(string path)
    {
        string[] sheepUnitDB = File.ReadAllLines(Application.streamingAssetsPath + path);

        for (int i = 1; i < sheepUnitDB.Length; i++)
        {
            SheepDatas.Add(new DBStruct.SheepData(sheepUnitDB[i].Split(',')));
        }
    }
    private void LoadWolfDB(string path)
    {
        string[] wolfUnitDB = File.ReadAllLines(Application.streamingAssetsPath + path);

        for (int i = 1; i < wolfUnitDB.Length; i++)
        {
            WolfDatas.Add(new DBStruct.WolfData(wolfUnitDB[i].Split(',')));
        }
    }
    public void LoadNextWave(int fileLine)
    {
        string[] waveDB = File.ReadAllLines(Application.streamingAssetsPath + wavePath);
        waveDatas.Add(new DBStruct.WaveData(waveDB[fileLine].Split(',')));
    }
}
