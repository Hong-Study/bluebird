# 각 인원 파트에 따른 폴더 정리

## 김정도
- ServerPlatform : 웹 플랫폼입니다.
- Libraries


## 임우영
- ServerNpc : 방의 장애물 들의 움직임을 담당하는 서버 입니다.
- GameClient : 로비와 게임 클라이언트 코드입니다.
- Libraries


## 홍지현
- [ServerCore](https://github.com/Hong-Study/bluebird/tree/main/src/ServerCore) : ServerGame과 ServerMatch의 코어 부분(네트워크)를 담당하는 코드가 들어가 있는 폴더입니다.
- [ServerGame](https://github.com/Hong-Study/bluebird/tree/main/src/ServerGame) : 메인 게임 로직을 처리하는 서버로, 매치로 들어오게 된 유저들을 전달 받아 방 생성 및 게임 진행을 담당합니다. 
			   추가적으로 ServerNPC 와의 통신으로 방의 움직이는 장애물의 움직임 정보를 받아와 브로드캐스팅하는 역활도 담당하고 있습니다.

- [ServerMatch](https://github.com/Hong-Study/bluebird/tree/main/src/ServerMatch) : 매치를 담당하는 서버로, ServerGame과 ServerPlatform과의 통신을 통해 유저를 매칭 및 전달하는 역활을 하는 서버입니다.
- [DummyClient](https://github.com/Hong-Study/bluebird/tree/main/src/DummyClient) : 테스트 클라이언트 코드입니다.
- Libraries : Protobuf 라이브러리와 Redis 라이브러리가 들어가 있는 폴더입니다.

※ ServerGame, ServerMath 같은 경우 폴더 내의 ReadMe 존재


## 김민관
- GameClient
- Libraries
