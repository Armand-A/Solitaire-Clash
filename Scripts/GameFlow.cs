using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow GF;

    public GameObject deck, deckSparks, text1, text2, card1, card2, cardSparks, sequence;
    
    public GameObject gradient, popupSparks, drawAceLogo, congratsLogo, dragVisual, endcard;

    public AudioSource drawSFX, winSFX;

    TapHandler deck_TH, card2_TH;

    Animator sequence_ANM;

    public int state = 0;

    void Start()
    {
        GF = this; 

        deck_TH = deck.GetComponent<TapHandler>();
        card2_TH = card2.GetComponent<TapHandler>();

        sequence_ANM = sequence.GetComponent<Animator>();

        StartCoroutine(bypassIntro());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Vector2 c = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (state == 6){
                Luna.Unity.Playable.InstallFullGame();
            } else if (state == 0){
                closeIntro();
            } else if (state == 1 && deck_TH.checkTouch(c)){
                drawSFX.Play();
                card1.SetActive(true);
                state = 2;
            } else if (state == 2 && deck_TH.checkTouch(c)){
                deckSparks.SetActive(false);
                drawSFX.Play();
                card2.SetActive(true);
                cardSparks.SetActive(true);
                text1.SetActive(false);
                state = 3;
                StartCoroutine(startDragAnim());
            } else if (state == 4 && card2_TH.checkTouch(c)){
                card2.GetComponent<Draggable>().PickUp();
                gradient.SetActive(false);
                dragVisual.SetActive(false);
            }
        }
    }

    void closeIntro(){
        state = 1;
        text1.SetActive(true);
        deckSparks.SetActive(true);
        gradient.SetActive(false);
        popupSparks.SetActive(false);
        drawAceLogo.SetActive(false);
    }

    public void reenableDragAnim(){
        gradient.SetActive(true);
        dragVisual.SetActive(true);
    }

    public void finalSequence(){
        card2.SetActive(false);
        text2.SetActive(false);
        sequence_ANM.Play("Sequence_Anim", 0, 0);
        winSFX.Play();
        gradient.SetActive(true);
        popupSparks.SetActive(true);
        congratsLogo.SetActive(true);
        state = 5;
        StartCoroutine(startCTA());
    }

    IEnumerator bypassIntro(){
        yield return new WaitForSeconds(1.75f);
        if (state == 0){
            closeIntro();
        }
    }

    IEnumerator startDragAnim(){
        yield return new WaitForSeconds(0.5f);
        cardSparks.SetActive(false);
        text2.SetActive(true);
        gradient.SetActive(true);
        dragVisual.SetActive(true);
        state = 4;
    }

    IEnumerator startCTA(){
        yield return new WaitForSeconds(2f);
        endcard.SetActive(true);
        //yield return new WaitForSeconds(0.25f);
        state = 6;
    }
}
