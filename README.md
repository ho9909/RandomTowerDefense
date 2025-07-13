# Multiplayer Tower Defense Game (Unity / C#)

Unity 기반의 멀티플레이 타워 디펜스 게임입니다. 가챠 시스템, 타워 업그레이드, 웨이브 생성, 멀티플레이어 기능 등을 포함하며, 구조화된 스크립트로 각 기능이 모듈화되어 있습니다.

## 📌 Features

- 🎮 싱글 & 멀티플레이어 모드
- 🧱 타워 배치 및 업그레이드
- 🐱 가챠를 통한 유닛 소환
- 🧠 AI 적 유닛 스폰 및 경로 이동
- 🔄 씬 전환 및 페이드 효과
- 👀 카메라 줌 및 이동 기능
- 🚩 팀 전용 타워 (Red / Blue)

---

## 📁 Folder Structure
📦Script/
<br />
┣ 📜BuildManager.cs
<br />
┣ 📜Bullet.cs
<br />
┣ 📜Cat_upgrade.cs
<br />
┣ 📜Check.cs
<br />
┣ 📜CompleteLevel.cs
<br />
┣ 📜countdown_multi.cs
<br />
┣ 📜DragController_multi.cs
<br />
┣ 📜Enemy.cs
<br />
┣ 📜Enemy_Mulit.cs
<br />
┣ 📜Gacha.cs
<br />
┣ 📜LevelSelector.cs
<br />
┣ 📜MainMenu.cs
<br />
┣ 📜Mouse_upgrade_multi.cs
<br />
┣ 📜Movement_multi.cs
<br />
┣ 📜node.cs
<br />
┣ 📜PanZoom.cs
<br />
┣ 📜PlayerController.cs
<br />
┣ 📜SceneFader.cs
<br />
┣ 📜spawnTurret.cs
<br />
┣ 📜Turret.cs
<br />
┣ 📜Turret_blue.cs
<br />
┣ 📜Turret_red.cs
<br />
┣ 📜upgrade_Turret.cs
<br />
┣ 📜WaveSpawner.cs
<br />
┗ 📜WaveSpawner_multi.cs
<br />

---

## 🚀 Getting Started

1. Unity에서 프로젝트 열기
2. `MainMenu` 씬을 시작 씬으로 설정
3. 플레이 모드 실행 후 싱글 또는 멀티 모드 선택
4. 유닛 가챠 → 타워 설치 → 적의 웨이브 방어

---

## 🛠️ Scripts Overview

| Script | Description |
|--------|-------------|
| `BuildManager.cs` | 타워 설치 매니저 |
| `Bullet.cs` | 총알 이동 및 충돌 처리 |
| `Gacha.cs` | 유닛 소환 가챠 시스템 |
| `Cat_upgrade.cs`, `Mouse_upgrade_multi.cs` | 유닛 강화 |
| `Turret.cs`, `Turret_red.cs`, `Turret_blue.cs` | 타워 공격 로직 및 팀별 타워 |
| `WaveSpawner.cs`, `WaveSpawner_multi.cs` | 적 유닛 웨이브 스폰 |
| `Enemy.cs`, `Enemy_Mulit.cs` | 적 유닛 이동 및 체력 시스템 |
| `DragController_multi.cs`, `Movement_multi.cs` | 멀티플레이 조작 기능 |
| `SceneFader.cs`, `MainMenu.cs`, `LevelSelector.cs` | UI 및 씬 관리 |
| `PanZoom.cs` | 카메라 줌/이동 제어 |

---

## 💡 Development Stack

- Unity (2020+)
- C#
- Photon (Multiplayer support, 추정)
- 구조화된 객체지향 스크립트 구성

---

## 📌 Notes

- `*_multi.cs` 접미사는 멀티플레이어 전용 스크립트입니다.
- `Turret_red.cs`, `Turret_blue.cs`는 팀 구분 기반으로 동작합니다.
- `Gacha.cs`는 확률 기반 유닛 소환 시스템입니다.

---

## 👤 Author

- Github: [ho9909](https://github.com/ho9909)

