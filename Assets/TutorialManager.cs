using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{


    //want to add a timer for switching between states

    public float ObjectiveTime = 2;

    public int stage = 0;

    public LevelManager levelManager;

    public SpriteRenderer countdown;
    public Sprite[] numbers;

    [Header("Stage 1")]
    [Header("A Variables")]

    public GameObject a;
    public GameObject aCover;
    public GameObject aCheck;
    public float aProgress;
    private Vector2 aStart;

    [Header("D Variables")]

    public GameObject d;
    public GameObject dCover;
    public GameObject dCheck;
    public float dProgress;
    private Vector2 dStart;

    [Header("W Variables")]

    public GameObject w;
    public GameObject wCover;
    public GameObject wCheck;
    public float wProgress;
    private Vector2 wStart;


    [Header("Stage 2")]

    [Header("Player Reference")]
    public Transform player;

    [Header("Edges")]
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    [Header("SpriteSwaps")]
    public Sprite check;

    [Header("Stage 3")]
    [Header("Space")]
    public GameObject space;
    public GameObject spaceCover;
    public GameObject SpaceCheck;
    public float spaceProgress;
    private Vector2 spaceStart;

    [Header("dummies")]
    public GameObject shipDummies;
    public GameObject dummyDummy;

    private bool lockStage = false;

    // Start is called before the first frame update
    void Start()
    {
        aStart = aCover.transform.position;
        dStart = dCover.transform.position;
        wStart = wCover.transform.position;

        spaceStart = spaceCover.transform.position;

        countdown.sprite = null;
    }

    public IEnumerator ChangeStage()
    {
        lockStage = true;
        for (int i = 0; i < numbers.Length; i++)
        {
            countdown.sprite = numbers[i];
            yield return new WaitForSeconds(1);
        }

        stage++;

        countdown.sprite = null;
        lockStage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lockStage)
        {
            if (stage == 0)
            {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    aProgress += Time.deltaTime / ObjectiveTime;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    dProgress += Time.deltaTime / ObjectiveTime;
                }

                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    wProgress += Time.deltaTime / ObjectiveTime;
                }

                aCover.transform.position = Vector2.Lerp(aStart, a.transform.position, aProgress);
                dCover.transform.position = Vector2.Lerp(dStart, d.transform.position, dProgress);
                wCover.transform.position = Vector2.Lerp(wStart, w.transform.position, wProgress);

                if (aProgress >= 1)
                {
                    aCheck.SetActive(true);
                }

                if (dProgress >= 1)
                {
                    dCheck.SetActive(true);
                }

                if (wProgress >= 1)
                {
                    wCheck.SetActive(true);
                }


                if (aCheck.activeSelf && dCheck.activeSelf && wCheck.activeSelf)
                {

                    a.SetActive(false);
                    d.SetActive(false);
                    w.SetActive(false);

                    StartCoroutine(ChangeStage());
                }

            }
            else if (stage == 1)
            {
                bool vertDone;
                bool horDone;

                top.SetActive(true);
                bottom.SetActive(true);
                left.SetActive(true);
                right.SetActive(true);

                SpriteRenderer topSprite  = top.GetComponent<SpriteRenderer>();
                SpriteRenderer bottomSprite = bottom.GetComponent<SpriteRenderer>();
                SpriteRenderer leftSprite = left.GetComponent<SpriteRenderer>();
                SpriteRenderer rightSprite = right.GetComponent<SpriteRenderer>();

                //detect player going through edge, and replace edge sprite with check mark
                if (player.transform.position.y > top.transform.position.y + 1)
                {
                    topSprite.sprite = check;
                }

                if (player.transform.position.y < bottom.transform.position.y - 1)
                {
                    bottomSprite.sprite = check;
                }

                if (player.transform.position.x < left.transform.position.x - 1)
                {
                    leftSprite.sprite = check;
                }

                if (player.transform.position.x > right.transform.position.x + 1)
                {
                    rightSprite.sprite = check;
                }

                vertDone = topSprite.sprite == check && bottomSprite.sprite == check;
                horDone = leftSprite.sprite == check && bottomSprite.sprite == check;

                if (vertDone && horDone)
                {

                    top.SetActive(false);
                    bottom.SetActive(false);
                    left.SetActive(false);
                    right.SetActive(false);

                    StartCoroutine(ChangeStage());
                }


            }
            else if (stage == 2)
            {
                int count = levelManager.getEnemies() - 1;

                space.SetActive(true);
                SpaceCheck.SetActive(false);
                shipDummies.SetActive(true);

                levelManager.enemies = GameObject.FindGameObjectsWithTag("Enemy");

                spaceProgress = 1 - ((float)count / 8);

                spaceCover.transform.position = Vector2.Lerp(spaceStart, space.transform.position, spaceProgress);

                if (spaceProgress >= 1)
                {
                    SpaceCheck.SetActive(true);
                }

                if (count == 0)
                {
                    space.SetActive(false);
                    Destroy(dummyDummy);
                    StartCoroutine(ChangeStage());
                }


            }else if (stage == 3)
            {
                lockStage = true;
                levelManager.triggerUpdate();
            }
        }

        

    }
}
