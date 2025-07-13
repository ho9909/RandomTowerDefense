
# 🛡️ 멀티플레이 디펜스 게임 (Photon / Unity)

Unity 기반의 멀티플레이 타워 디펜스 게임입니다.  
플레이어는 카드 유닛을 소환하고 배치하며, 적의 물결을 막아내야 합니다.  
Photon을 활용한 실시간 동기화와 드래그, 가챠, 카메라 조작 기능을 통해 몰입도 있는 전략 플레이를 제공합니다.

---


## 📌 Features

- 🎮 싱글 & 멀티플레이어 모드
- 🧱 타워 배치 및 업그레이드
- 🐱 가챠를 통한 유닛 소환
- 🧠 AI 적 유닛 스폰 및 경로 이동
- 🔄 씬 전환 및 페이드 효과
- 👀 카메라 줌 및 이동 기능
- 🚩 팀 전용 타워 (Red / Blue)

---


## 📸 데모

> ✨ 플레이 영상 / 스크린샷 추가 예정

---

## ⚙️ 기술 스택

- **Unity C#**
- **Photon PUN2** (멀티플레이 네트워크)
- **UI/UX**
  - 드래그 앤 드롭
  - 카드 기반 유닛 소환
- **확률 기반 가챠 시스템**
- **적 상태이상 (중독, 기절) 시스템**
- **라운드 및 전력 평가 알고리즘**
- **멀티 터치 카메라 이동 / 줌**

---

## 📁 폴더 구조

```plaintext
Assets/
├── Scripts/
│   ├── gameManager_Multi.cs
│   ├── Gacha_multi.cs
│   ├── DragController_multi.cs
│   ├── Enemy_Mulit.cs
│   ├── HandCardDragHover.cs
│   ├── PanZoom.cs
│   └── PhotoManager.cs
│   └── ....
├── Prefabs/
├── Scenes/
└── ...
```

---


## 🚀 Getting Started

1. Unity에서 프로젝트 열기
2. `MainMenu` 씬을 시작 씬으로 설정
3. 플레이 모드 실행 후 싱글 또는 멀티 모드 선택
4. 유닛 가챠 → 타워 설치 → 적의 웨이브 방어

---

## 💡 Development Stack

- Unity (2020+)
- C#
- Photon (Multiplayer support, 추정)
- 구조화된 객체지향 스크립트 구성

---

## 🧠 주요 코드 설명

### 🔧 DragController_multi.cs – 유닛 드래그 및 판매 처리

```csharp
if (dragging && Input.GetMouseButtonUp(0))
{
    dragging = false;
    Turret_multi turret = gameObject.GetComponent<Turret_multi>();
    if (isSell)
    {
        if (turret.tower_count == 1)
        {
            gameManager_Multi.Money += 3;
        }
        if (turret.tower_count == 2)
        {
            gameManager_Multi.Money += 5;
        }
        if (turret.tower_count == 3)
        {
            gameManager_Multi.Money += 7;
        }
        if (turret.tower_count == 4)
        {
            gameManager_Multi.Money += 9;
        }
        if (turret.tower_count == 5)
        {
            gameManager_Multi.Money += 15;
        }
        if (turret.tower_count == 6)
        {
            gameManager_Multi.Money += 30;
        }

        PhotonNetwork.Destroy(gameObject);
    }
}
```

- 드래그 중 마우스를 놓았을 때, 해당 유닛이 판매 구역에 있다면 `tower_count`에 따라 자금을 환급해주고 오브젝트를 파괴합니다.

---

### 💀 Enemy_Mulit.cs – 적 체력 관리 및 사망 처리

```csharp
public void TakeDamage(float amount)
{
    health -= amount;
    healthBar.fillAmount = health / startHealth;

    if (health <= 0 && !isDead)
    {
        Die();
    }
}

void Die()
{
    isDead = true;

    GameObject effect = PhotonNetwork.Instantiate(deathEffect.name, transform.position, Quaternion.identity);
    PhotonNetwork.Destroy(effect);

    PhotonNetwork.Destroy(gameObject);
    gameManager_Multi.EnemyCount--;
    gameManager_Multi.EnemyKillCount++;
    gameManager_Multi.Money += 5;
}
```

- 적이 데미지를 받아 죽는 과정을 처리합니다.
- 체력 UI 갱신, 이펙트 생성, 적 제거 및 게임 상태 업데이트 포함.

---

### 🎲 Gacha_multi.cs – 확률형 유닛 소환

```csharp
int Choose(float[] probs)
{
    total = 0;

    foreach (float elem in probs)
    {
        total += elem;
    }

    float randomPoint = Random.value * total;

    for (int i = 0; i < probs.Length; i++)
    {
        if (randomPoint < probs[i])
        {
            return i;
        }
        else
        {
            randomPoint -= probs[i];
        }
    }
    return probs.Length - 1;
}
```

- 가챠 확률에 따라 유닛을 랜덤 선택하는 핵심 로직입니다.

---

### 📊 gameManager_Multi.cs – 라운드 유닛 집계

```csharp
public void check()
{
    int Grade_cat = 0;
    int Grade_dog = 0;
    int Grade_mouse = 0;
    var turret = new List<GameObject>();

    GameObject[] cat = GameObject.FindGameObjectsWithTag("cat");
    GameObject[] dog = GameObject.FindGameObjectsWithTag("dog");
    GameObject[] mouse = GameObject.FindGameObjectsWithTag("mouse");

    turret.AddRange(cat);
    turret.AddRange(dog);
    turret.AddRange(mouse);

    foreach (GameObject t in turret)
    {
        if (t == null) break;

        if (t.tag == "cat")
        {
            Grade_cat += t.GetComponent<Turret_multi>().tower_count;
        }
        else if (t.tag == "dog")
        {
            Grade_dog += t.GetComponent<Turret_multi>().tower_count;
        }
        else if (t.tag == "mouse")
        {
            Grade_mouse += t.GetComponent<Turret_multi>().tower_count;
        }
    }

    if (turret.Count != 0)
    {
        WaveSpawner_Multi.turretCounts[0] = Grade_cat;
        WaveSpawner_Multi.turretCounts[1] = Grade_dog;
        WaveSpawner_Multi.turretCounts[2] = Grade_mouse;
    }
}
```

- 배치된 유닛의 종류와 레벨을 수치로 집계하여 난이도 반영.

---

### 🃏 HandCardDragHover.cs – 카드 드래그 및 유닛 배치

```csharp
public void OnEndDrag(PointerEventData eventData)
{
    ...
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
        v4.x = hit.point.x;
        v4.y = 0.63f;
        v4.z = hit.point.z;
        obj.GetComponent<spawn>().spawnarea(v4);
    }
    ...
}
```

- 드래그한 카드를 필드에 배치하고, 해당 위치에 유닛을 생성합니다.

---

### 🗺️ PanZoom.cs – 카메라 이동 및 줌

```csharp
public void OnDrag()
{
    int touchCount = Input.touchCount;

    if (touchCount == 1 && drag_check == false)
    {
        if (prevPos == Vector2.zero)
        {
            prevPos = Input.GetTouch(0).position;
            return;
        }
        Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
        Vector3 vec = new Vector3(dir.x, 0, dir.y);

        cam.position -= vec * moveSpped * Time.deltaTime;
        prevPos = Input.GetTouch(0).position;
    }

    else if (touchCount == 2)
    {
        if (prevDistance == 0)
        {
            prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            return;
        }
        float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        float move = prevDistance - curDistance;

        Vector3 pos = cam.position;

        if (move < 0)
        {
            pos.y -= moveSpped * Time.deltaTime;
        }
        else if (move > 0)
        {
            pos.y += moveSpped * Time.deltaTime;
        }

        cam.position = pos;
        prevDistance = curDistance;
    }
}
```

- 1손가락으로 드래그 이동, 2손가락으로 확대/축소 조작.

---

### 🌐 PhotoManager.cs – Photon 연결 초기화

```csharp
private void Awake()
{
    PhotonNetwork.AutomaticallySyncScene = true;
    PhotonNetwork.GameVersion = version;

    Debug.Log(PhotonNetwork.SendRate);
    PhotonNetwork.ConnectUsingSettings();
}
```

- 멀티플레이 기능 활성화를 위한 Photon 초기 설정.

---

## ✅ 실행 방법

1. Unity 2021.3 이상에서 프로젝트 열기
2. Photon PUN 2 패키지 임포트
3. Main 씬 실행
4. 2명 이상 접속 시 자동 게임 시작

---

## 🙋‍♂️ 제작자

| 이름 | 역할 |
|------|------|
| [Jo Seongin](https://github.com/ho9909) | 클라이언트 프로그래밍, Photon 멀티플레이, UI/UX 구현, 게임 기획 및 밸런싱 |

---

## 📜 라이선스

본 프로젝트는 개인 포트폴리오 용도로 제작되었으며, 상업적 사용은 금지됩니다.
