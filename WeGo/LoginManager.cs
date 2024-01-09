using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginManager : MonoBehaviour
{
    List<PlayerData> playerdata;

    int id;
    string pw;
    public string Scene;
    private new string name;
    public InputField text;
    public InputField text1;
    
    public class PlayerData
    {
        public int id { get; set; }
        public string pw { get; set; }
        public string value { get; set; }

        public PlayerData()
        {

        }

        public PlayerData(int _Id, string _Pw, string _Name)
        {
            id = _Id;
            pw = _Pw;
            value = _Name;
        }
    }

    public class Inputdata
    {
        public int id { get; set; }
        public string pw { get; set; }
        public string value { get; set; }

    }

    public void Receivedata(string player)
    {
        //str로 json파싱
        //JObject json = JObject.Parse(player);

        playerdata = JsonConvert.DeserializeObject<List<PlayerData>>(player);
        LoginFind();
    }

    public void Login()
    {
        WWWForm form = new WWWForm();

        form.AddField("id", int.Parse(text.text));
        form.AddField("pw", text1.text);
        

        StartCoroutine(GetData());
    }

    public void LoginFind()
    {
        PlayerData findid = null;
        int id = int.Parse(text.text);
        findid = IdFind(id);

        if (findid == null)
        {
            Debug.Log("실패!!");
        }
        else
        {
            Debug.Log("성공!!");
            Debug.Log(text1.text);

            PlayerPrefs.SetString("PlayerName", text1.text);
            PlayerPrefs.Save();

            SceneManager.LoadScene(Scene);
            PhotonNetwork.Disconnect();
        }

    }

    public PlayerData IdFind(int number)
    {
        PlayerData players = null;
        players = playerdata.Find(data => data.id == number);
        return players;
    }


    IEnumerator GetData()
    {
        string GetDataUrl = "https://211.199.144.148:5001/api/Players";//리퀘스트할 URL


        using (UnityWebRequest www = UnityWebRequest.Get(GetDataUrl))
        {

            www.certificateHandler = new CertificateHandlerPublicKey();

            yield return www.SendWebRequest();
            //Receivedata(www.downloadHandler.text);
            if (www.isNetworkError || www.isHttpError) //불러오기 실패 시
            {
                Debug.Log(www.error);
            }
            else
            {
                
                Debug.Log(www.downloadHandler.text);
                string result = www.downloadHandler.text;
                Receivedata(result);
                //Receivedata(www.downloadHandler.text);
            }

        }
    }

    public class CertificateHandlerPublicKey : CertificateHandler
    {
        //public Text textpk;

        // Encoded RSAPublicKey
        private static string PUB_KEY = "3082010a0282010100d3c7e2e2c0782a28c2a3a87e55e56f6fa81df46c90ef2b4f6bc302c59a19d00e73b5695182293259f0e4be8af1a04b2475c654b06ad135ce5ad599f7c26d3f5a72df891db9fd5191c928ac6f5735f56b38a97043dadffe1055bedc4695bf205dec32fce3183def633b2a7920695cefa32fd998897adfe7ee21f5b360da9e746231d29bdc4d164df298f74bd2550e92140524c963881af83c5197532d8a0c2bd053f38ec264301da93331a740226ade14a6ff6ae3d02a47cca151fcbb2cfdf5f274757e5eeb90f2abc9c587ed9b3cb06bbbf88e886deb137440804ee2caa330976169419ee37f4239588b34946eb7c4928006c2ede8997f507f3b7835f712fdc50203010001";
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            X509Certificate2 certificate = new X509Certificate2(certificateData);
            string pk = certificate.GetPublicKeyString();
            Debug.Log(pk);
            if (pk.ToLower().Equals(PUB_KEY.ToLower()))
                return true;
            return false;
        }

    }


    
}
