using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class manages the music menu and its behavior.
/// </summary>
public class Music : MonoBehaviour
{
    public Drivable Drivable;
    public StartUp StartUp;
    public Sprite[] sprites = new Sprite[4];
    public TextMeshProUGUI songName;
    public TextMeshProUGUI songAuthor;
    private String[] songs = {"Eventually", "Sola (Remix)", "X 100PRE", "Thnks fr th Mmrs"};
    private String[] authors = {"Tame Impala", "Anuel AA", "Bad Bunny", "Fall Out Boy"};

    private int musicIndex = 0;

    void Start()
    {
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void UpdatedData(object sender, Drivable.DrivableArgs e){
        const int musicIndexMaxLimit = 3;
        const int musicIndexMinLimit = 0;

        if(StartUp.getScreenOnFlag()){
            if(e.Buttons[2]){
                if(musicIndex < musicIndexMaxLimit) musicIndex += 1;
                gameObject.GetComponent<Image>().sprite = sprites[musicIndex];
                songName.text = songs[musicIndex];
                songAuthor.text = authors[musicIndex];

            }
            if(e.Buttons[1]){
                if(musicIndex > musicIndexMinLimit) musicIndex -= 1;
                gameObject.GetComponent<Image>().sprite = sprites[musicIndex];
                songName.text = songs[musicIndex];
                songAuthor.text = authors[musicIndex];

            }
        }
    }
}
