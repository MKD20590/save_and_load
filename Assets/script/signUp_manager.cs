using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class signUp_manager : MonoBehaviour
{
    [SerializeField] private SignUpData data;
    public string url;
    string openJSON;
    string path;

    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField re_password;
    [SerializeField] private TextMeshProUGUI warning;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "_SignUpData.txt";
        //ambil data json
        try
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            openJSON = reader.ReadToEnd();
            data = JsonUtility.FromJson<SignUpData>(openJSON);
/*            data.email = JsonUtility.FromJson<SignUpData>(openJSON).email;
            data.password = JsonUtility.FromJson<SignUpData>(openJSON).password;
            data.createDate = JsonUtility.FromJson<SignUpData>(openJSON).createDate;*/
            print(openJSON);
            reader.Close();
            stream.Close();
        }
        catch(Exception e)
        {
            Debug.LogError("An error occured when trying to load data from file\nor file does not exist\n" + e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void submit()
    {
        //validasi password
        if (email.text.Contains("@binus.ac.id"))
        {
            if (password.text.Any(char.IsDigit) && password.text.Any(char.IsUpper) && password.text.Any(char.IsSymbol) && password.text.Length >= 5)
            {
                if (re_password.text == password.text)
                {
                    warning.color = Color.green;
                    warning.text = "Sign up successful";

                    data.email = email.text;
                    data.password = password.text;
                    data.createDate = System.DateTime.Now.ToString();

                    //write to json file
                    FileStream stream = new FileStream(path, FileMode.Create);
                    StreamWriter writer = new StreamWriter(stream);

                    string JSONfile = JsonUtility.ToJson(data,true);
                    writer.Write(JSONfile);
                    print(JSONfile);
                    writer.Close();
                    stream.Close();

                    //save to database
                    StartCoroutine(saveData());

                    //reset input field
                    email.text = "";
                    password.text = "";
                    re_password.text = "";
                }
                else
                {
                    warning.color = Color.red;
                    warning.text = "Please re-enter your password";
                }
            }
            else
            {
                warning.color = Color.red;
                if (!password.text.Any(char.IsUpper))
                {
                    warning.text = "Password must include at least an uppercase character";
                }
                else if (!password.text.Any(char.IsSymbol))
                {
                    warning.text = "Password must include at least a symbol";
                }
                else if (!password.text.Any(char.IsDigit))
                {
                    warning.text = "Password must include at least a number";
                }
                else if (password.text.Length < 5)
                {
                    warning.text = "Password must include at least 5 characters";
                }
            }
        }
        else
        {
            warning.color = Color.red;
            warning.text = "Email must include @binus.ac.id";
        }
    }
    public IEnumerator saveData()
    {
        WWWForm form = new WWWForm();
        form.AddField("email", data.email);
        form.AddField("password", data.password);
        form.AddField("createDate", data.createDate);

        UnityWebRequest request = UnityWebRequest.Post(url,form);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

    }
    public void load()
    {
        if (data.email != null && data.password != null)
        {
            email.text = data.email;
            password.text = data.password;
        }
        else
        {
            warning.color = Color.red;
            warning.text = "There's no saved data yet";
        }
    }
}
