# FlatTouch-Blockchain

## 프로젝트 소개
<img src = "https://github.com/woorija/FlatTouch-Blockchain/assets/77769870/02ee6fcb-a72c-4d11-a025-e79c502e2408" width="30%" height="30%"></img>
<img src = "https://github.com/woorija/FlatTouch-Blockchain/assets/77769870/97cb6f89-46d3-4334-930f-2c0a4e0b3749" width="30%" height="30%"></img>
 - 작품명: **Flat Touch with BlockChain**

 - 플랫폼: Windows
 - 개발 기간: 2023.11.04~2023.12.08
 - 개발 환경: Unity 2021.3.19f1 (urp)
 - 기술 스택
   - 언어: C# , Solidity
   - SDK 및 라이브러리: Web3 Unity SDK, Hardhat, OpenZeppelin
 - 소개 : 기존에 작업했던 프로젝트에 블록체인을 접목한 사이드 프로젝트입니다.</br> 클리어한 스테이지 만큼 ERC-1155 토큰을 민팅할 수 있게 하여 민팅한 토큰 개수에 따라 스테이지를 해금할 수 있는 기능을 구현하였습니다.
 - [블록체인 연동 시연 영상](https://youtu.be/AXMxPCZP2v8)
 - [프로젝트 개발 과정 - 노션](https://woorija.notion.site/Flat-Touch-with-BlockChain-3a76c34ae3bd4c059c391d97c110b7a2)
 - 프로젝트에 사용된 솔리디티 [이더스캔 주소](https://sepolia.etherscan.io/address/0x0c4f74549ecf0564b01a6ab9ecf43591bd12b731#code)

## 개발 환경 세팅
### 사전 준비 사항
 - [Unity Hub](https://unity.com/kr/download)
 - [Wallet Connect](https://cloud.walletconnect.com/sign-in) 계정 생성
 - Web3 Unity SDK
   - [Getting Started](https://docs.gaming.chainsafe.io/current/getting-started)
   - [Project ID Registration](https://docs.gaming.chainsafe.io/current/project-id-registration)
   - [Setting Up An RPC Node For web3.unity](https://docs.gaming.chainsafe.io/current/setting-up-an-rpc-node)
### 설치 방법
1. [Unity Hub](https://unity.com/kr/download)를 설치한다.
2. `Unity 2021.3.19f1`버전의 에디터를 설치한다.
3. 저장소를 클론한다.
   ```bash
   git clone https://github.com/woorija/FlatTouch-Blockchain.git
   ```
4. 아래 정보를 입력한다.
   - WalletConnect에 Project ID 입력하기</br>![캡처_2023_12_12_18_32_46_944](https://github.com/woorija/FlatTouch-Blockchain/assets/77769870/16f032cb-1f5e-4de9-aec9-d5953f243ba8)

   - ChainSafeServerSettings에 Project ID와 RPC 입력하기</br>![캡처_2023_12_12_18_33_18_996](https://github.com/woorija/FlatTouch-Blockchain/assets/77769870/ac5dd062-dc43-46a5-95c8-aca00f349f45)

5. Unity에서 프로젝트 실행 후 빌드한다.

### 주의 사항
 - Web3 Unity SDK가 열리지 않을 시 [SDK 공식 문서](https://docs.gaming.chainsafe.io/)를 찾아 설치하기

## 게임 실행
 1. [다운로드](https://drive.google.com/drive/folders/1lGUqq3UsQlfZixgAlJqm1OFKKWVZaPjQ)
 2. zip 파일을 압축 해제하고 `flat touch.exe` 파일을 실행하면 게임을 플레이할 수 있습니다.
   - !!!주의!!! 폴더 내 다른 파일,폴더를 옮겨서는 안됩니다.

## 게임 조작법
 - 클릭을 통해 조작이 가능합니다.
 - PC의 경우 qweasdzxc 혹은 키패드789456123을 사용하여 Flat을 터치할 수 있습니다.

