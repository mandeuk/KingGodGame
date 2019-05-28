using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public int itemType;
    public bool magnetic = false;
    public float magneticspeed;

    // Use this for initialization
    void Awake () {
        //SetupItem(Random.Range(1, 9));
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if(magnetic)
        {
            Vector3 playerpos = PlayerBase.instance.transform.position;
            playerpos.y = 1.0f;
            Vector3 movevector = (playerpos - this.gameObject.transform.position).normalized;//플레이어 방향으로 정규화(normalized)된 벡터를 저장(길이 1.0)

            this.gameObject.GetComponent<Rigidbody>().AddForce(movevector * magneticspeed);

            if (magneticspeed < 700.0f)
            {
                magneticspeed += (Time.deltaTime*50);
            }
        }
    }

    // 함수이름 : void ItemEffect()
    // 기능 : 획득한 아이템의 효과를 적용하는 함수, ItemManager와 PlayerStatus에 접근합니다.
    private void ItemEffect()
    {
        //PlayerStatus.instance.attackPower += 10;
        Itemtable.Instance.ApplyItem(itemType);
        ItemManager.Instance.GetItem(gameObject);
    }

    // 함수이름 : void OnTriggerEnter(Collider other)
    // 기능 : 아이템과 충돌한 물체가 Player일 경우 아이템을 획득하도록 하는 기능을 가지고 있습니다
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemEffect();
            gameObject.SetActive(false);//충돌한 Item 오브젝트를 비활성화
            magneticspeed = 10.0f;
            magnetic = false;
        }
        //기획 변경으로 마그네틱기능 보류
        /*
        if(other.CompareTag("ItemMagnet"))
        {
            magnetic = true;
            //Debug.Log("아이템획득");
        }
        */
    }
    

    public void SetupItem(int rcvtype)
    {
        itemType = rcvtype;
        switch(itemType)
        {
            case 0: case 1: case 2: case 3: case 4:case 5:case 6:case 7://0~7 정수계열 아이템
                {
                    if (gameObject.GetComponent<ParticleSystem>() == null)
                    {
                        gameObject.AddComponent<ParticleSystem>();
                    }

                    if (gameObject.GetComponent<ParticleSystem>() != null)
                    {
                        ParticleSystem.MainModule myPMain;
                        myPMain = gameObject.GetComponent<ParticleSystem>().main;
                        //myPMain.startColor = Itemtable.Instance.SetItemColor(itemType);
                        myPMain.startSpeed = 0;
                        myPMain.maxParticles = 1;

                        ParticleSystem.ShapeModule myShape;
                        myShape = gameObject.GetComponent<ParticleSystem>().shape;
                        myShape.enabled = false;

                        Renderer myRenderer = gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>();
                        myRenderer.material = Resources.Load<Material>("Materials/EffectsMat/glow_firefly");
                    }
                    break;
                }
            case 100://LotusOfAbyss아이템
                {
                    if (gameObject.GetComponent<SpriteRenderer>() == null)
                    {
                        gameObject.AddComponent<SpriteRenderer>();
                        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Sprite/twitchlogo");
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Image/Sprite/twitchlogo");
                    }
                    
                    break;
                }
                
        }
        magneticspeed = 10.0f;
        magnetic = false;
    }



    void DisableOtherComponent(int currentitemtype)
    {
        if (currentitemtype > 7)//정수가 아닐때
        {
            if (gameObject.GetComponent<ParticleSystem>() != null)//파티클시스템이 존재한다면
            {
                ParticleSystem.MainModule myPMain = gameObject.GetComponent<ParticleSystem>().main;
                myPMain.maxParticles = 0;//파티클을 0으로 만들어 파티클이펙트가 보이지 않도록한다.
            }
        }

        if(currentitemtype != 100)//LotusOfAbyss가 아닐때
        {
            if(gameObject.GetComponent<SpriteRenderer>() != null)//스프라이트 랜더러가 존재한다면
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = null;
            }
        }

    }
}
