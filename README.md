![Image](https://github.com/user-attachments/assets/8b2754c5-5fa6-4f12-a96c-6e1ddc3ed631)

# Vampire Survivor: Colosseum [<img src="https://github.com/user-attachments/assets/a6a32091-a55b-4721-adbb-38c79cea22f3"  width="40" height="30"/>](https://youtu.be/6krxvdsdVLs)

## 1. Project Overview [프로젝트 개요]

**1인 개발 Unity 2D Personal Project** - Vampire Survivor 모작

개발 기간: 24.06.13 ~ 24.06.21


## 2. Key Features [주요 기능]

#### 2.1 Battle & Weapon
- 다양한 무기 구현 (ScriptableObject로 설계된 무기 속성)
- 쿨타임 기반 자동 공격
- 몬스터 위치를 계산하여 무기 발사 방향 설정

#### 2.2 Monster
- Object Pooling 기반 몬스터 생성 및 재배치
- 일정 시간마다 새로운 몬스터 출현 및 난이도 증가
- 몬스터와의 충돌 및 피해 계산 처리

#### 2.3 Object
- 몬스터 및 무기 오브젝트의 반복 생성/삭제 최적화
- PoolManager를 통한 통합 관리 구조

#### 2.4 Manager
- GameManager에서 게임 시작/종료, 게임 시간, 플레이어 레벨 관리
- AudioManager에서 BGM, SFX 관리
- PoolManager에서 무기, 몬스터 풀링 관리

## 3. Tech Stack [기술 스택]
- C#
- Unity 2020.3.36
- ScriptableObject (무기 데이터)
- Object Pooling (몬스터, 무기)
- GameManager (게임 흐름 및 상태 관리)
- Fork + GitHub (형상 관리)
