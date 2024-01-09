using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Data;

[System.Serializable]
public class Chatting
{
    public string query;
}

public class ChatbotManager : MonoBehaviour
{
    public InputField temp;
    public Text Answer;


    public void Receivedata()
    {
        //str로 json파싱
        //JObject json = JObject.Parse(player);
        /*WWWForm form = new WWWForm();
        form.AddField("Data", Text.text);
        string Data = JsonConvert.SerializeObject(form);      
        StartCoroutine(PostData(Data));*/
        Chatting chat = new Chatting
        {
            query = temp.text
        };
        string json = JsonUtility.ToJson(chat);
        StartCoroutine(PostData(json));
    }




    IEnumerator PostData(string json)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1:8000/query/TEST", json))
        {
            //www.certificateHandler = new CertificateHandlerPublicKey();
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) //불러오기 실패 시
            {
                Debug.Log(www.error);
            }
            else //불러오기 성공시
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("UpLoad Success!");
                JObject jsondata = JObject.Parse(www.downloadHandler.text);
                Answer.text = jsondata["Answer"].ToString();
                // Answer.text = www.downloadHandler.text; // Answer 값
            }
        }
    }
    IEnumerator GetData()
    {
        string GetDataUrl = "http://192.168.35.212:8000/query/TEST";//리퀘스트할 URL


        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {

            //www.certificateHandler = new CertificateHandlerPublicKey();

            yield return www.SendWebRequest();
            //Receivedata(www.downloadHandler.text);
            if (www.isNetworkError || www.isHttpError) //불러오기 실패 시
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

        }
    }
    
}

