using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the infotainment part. 
/// Changes from music menu to map menu.
/// </summary>
public class Infotainment : MonoBehaviour
{
    public Drivable Drivable;
    public StartUp StartUp;
    public GameObject[] carousel = new GameObject[2];
    private int carouselIndex = 0;

    void Start()
    {
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatedData(object sender, Drivable.DrivableArgs e){
        const int carouselIndexMaxLimit = 1;
        const int carouselIndexMinLimit = 0;

        if(StartUp.getScreenOnFlag()){
            if(e.Buttons[7]){
                if(carouselIndex > carouselIndexMinLimit){
                    carousel[carouselIndex].SetActive(false);
                    carouselIndex -= 1;
                    carousel[carouselIndex].SetActive(true);
                } 
                //carousel[carouselIndex].SetActive(false);
            }
            if(e.Buttons[6]){
                if(carouselIndex < carouselIndexMaxLimit){
                    carousel[carouselIndex].SetActive(false);
                    carouselIndex += 1;
                    carousel[carouselIndex].SetActive(true);
                } 
            }
        }
    }   
}
