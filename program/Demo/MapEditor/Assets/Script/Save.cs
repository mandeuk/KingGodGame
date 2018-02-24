using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Save : MonoBehaviour {

    void Start()
    {



    }

    // Use this for initialization

    string m_strPath = "Assets/Resources/";



    public void WriteData(string strData)
    {
        FileStream f = new FileStream(m_strPath + "Data.txt", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(f, System.Text.Encoding.Unicode);
        writer.WriteLine(strData);
        writer.Close();
    }

    public void Parse()
    {
        TextAsset data = Resources.Load("Data", typeof(TextAsset)) as TextAsset;
        StringReader sr = new StringReader(data.text);
        string source = sr.ReadLine();
        string[] values;

        while (source != null)

        {

            values = source.Split(',');  // 쉼표로 구분한다. 저장시에 쉼표로 구분하여 저장하였다.

            if (values.Length == 0)

            {

                sr.Close();

                return;

            }

            source = sr.ReadLine();    // 한줄 읽는다.

        }
    }


    void Update()

    {

    }
    
}
