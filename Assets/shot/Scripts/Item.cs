using UnityEngine;

// 아이템 타입 열거형 (더미 데이터용)
public enum ItemType
{
    Mammalia, // 포유류
    Amphibia, // 양서류
    Birds     // 조류
}

// 아이템 데이터 클래스
[System.Serializable]
public class Item
{
    public int itemId; // 아이템 ID
    public int itemLv; // 동물 레벨
    public ItemType itemType; // 아이템 타입
    public string itemName; // 동물 이름
    public Sprite itemImage; // 동물 이미지

    public int price; // 아이템 가격

    public string spownArea; // 스폰 지역
    public int spownRange; // 스폰 거리

    public string itemText; // 인게임 설명
    public string itemRealText; // 현실 설명

    public bool getChecked = false; // 도감 획득 여부 플래그
}
