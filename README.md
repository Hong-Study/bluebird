# camp2022_bluebird
스마일게이트 개발 캠프 2022 - 윈터 개발 캠프 2기 - Blue Bird
(프로젝트 세부 코드 및 담당 코드는 /src 파일에 존재합니다)
The project has been initialized.

## 목차
1. [프로젝트 개요](#프로젝트-개요)
	1. [시스템 아키텍처](#시스템-아키텍처)
	2. [기술 스택](#각-스택)

2. [테스트 방법](#테스트-순서-및-실행-방식)
3. [프로젝트별 폴더 설정](#프로젝트-폴더-설정)


# 프로젝트 개요
- 모두가 흥미롭게 개발할 수 있는 공통된 게임 분야이며 개개인의 도전 목표가 적절히 녹아 있는 폴 가이즈의 클론 프로젝트

### 시스템 아키텍처
![image](https://user-images.githubusercontent.com/48054868/221210118-c19a660d-947e-46a3-a76d-bee3a08def3e.png)

### 각 스택 및 담당
- 게임 클라이언트 : Unity (팀원, 팀원), **Socket(홍지현)**

**- 게임 서버 및 매치메이킹 서버 : C++ - IOCP (홍지현)**
- 웹 플랫폼 : Python - FastAPI, Socket-TCP/IP (팀원)
- NPC 서버 : C++ - Boost::Asio Library (팀원)
- DB : MySQL, Redis (팀원)

# 테스트 순서 및 실행 방식
- 게임서버 -> NPC서버 -> 매치서버 -> 웹 서버
1. 유저는 웹에서 회원가입 또는 로그인을 하여 클라를 실행.
2. 로비에 들어온 유저는 해당 모드에 맞춰 매치 입장
3. 로비 서버는 들어온 유저의 정보를 매치서버에 전송
4. 매치 서버는 유저가 선택한 모드에 따른 매치에 입장 시킴.
5. 이후 매치에서 특정 모드에 맞는 인원이 될 시 매치 서버는 정보를 모아 게임서버와 로비서버에 전송
  2.1 전송받은 게임서버는 방을 생성하고 정보를 NPC서버에 전송
  2.2 전송받은 로비서버는 해당 유저를 게임에 접속시킴
6. 게임 서버는 방 생성 정보를 NPC 서버에게 전송.
7. 유저는 매치 이후 게임서버에 접속하여 세션 등록
8. 게임 서버는 NPC 서버의 장애물 정보와 게임 인원을 확인하여 게임을 실행 및 대기시킴.
9. 게임 실행 이후 종료되면 클라는 로비로 Sceen 전환, 게임 서버는 룸 정보 정리
10. LOOP

## 포트 설정 및 IP, 패스워드
### PortNumber
- 3306 : NpcServer - MySQL
- 5000 : Server - Client
- 6000 : Loby - Match
- 6379 : Redis Server
- 7000 : Server - Match
- 8000 : Server - Npc

### Redis Server Address
- 34.84.148.50
- 임시 passward : a12b34

### MySQL Server Address(Npc Server)
- 121.145.229.137
- 임시 passward : a12b34

# 프로젝트 폴더 설정
- /src : 전체 소스코드가 들어가 있는 폴더입니다.
- /binary : 실행 파일들이 들어가는 폴더입니다.
- /config : 설정 파일들이 들어가는 폴더입니다.
- /docs : 문서가 들어가는 폴더입니다.
