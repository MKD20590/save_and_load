using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveLoad_WebRequest : MonoBehaviour
{
    public GameData gameData;
    public string url = "https://binusmaya.binus.ac.id/";
    public string url2;
    public Texture2D texture;
    MeshRenderer meshRenderer;
    //buat kl butuh data dr luar device / internet, pake web request
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(GetData());
    }

    public IEnumerator GetData() //gk pake void biar gk mandek (kl void bakal ke pause buat donlot data, jadi ngehambat func. lain buat jln)
    {
        //ada get, post, put
        //get buat dapetin data
        //post kirim data ke server (login info)

        //isi url itu link ke web, ato IP/DNS jg bisa
        //data dr file github jg bs diakses lewat webrequest
        //tinggal buka filenya (HRS RAW kl di github/text kosongannya tanpa UI github, soalnya yg diliat itu bagian body di html-nya) trus copas linknya ke string url
        //ngambil php ato mysql juga bisa

        //ini buat post
/*        WWWForm form = new WWWForm();
        form.AddField("email", data.email);
        form.AddField("password", data.password);
        form.AddField("createDate", data.createDate);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }*/

        // ini buat get
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest(); //buat minta datanya

        //buat tracking download progress dr webnya (0.0 - 1.0)
        while(!request.isDone)
        {
            print(request.downloadProgress);
            yield return null;
        }
        //UnityWebRequestMultimedia GetAudioclip;
        //UnityWebRequestMultimedia GetVid;
        //UnityWebRequestAssetBundle bundle;        ambil semua assest / 3d model + texturenya

        //print(request.downloadHandler.text);
        //string result = request.downloadHandler.text;
        //gameData = JsonUtility.FromJson<GameData>(result);



        //dptin gambar, urlnya hrs yg blkgnya .png (yg copy img address)
        UnityWebRequest reqTexture = UnityWebRequestTexture.GetTexture(url2);
        yield return reqTexture.SendWebRequest();
        texture = DownloadHandlerTexture.GetContent(reqTexture); //gk ke simpen, cmn muncul pas runtime
        //dijadiin ke material
        meshRenderer.material.mainTexture = texture;
        print(reqTexture.result);
    }
}
