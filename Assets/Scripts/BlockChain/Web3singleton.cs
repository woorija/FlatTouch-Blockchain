using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using ChainSafe.Gaming.UnityPackage;
using ChainSafe.Gaming.Web3.Build;
using ChainSafe.Gaming.Web3.Unity;
using ChainSafe.Gaming.Evm.JsonRpc;
using ChainSafe.Gaming.Wallets;
using ChainSafe.Gaming.Web3;
using Scenes;
using System;
using UnityEngine.SceneManagement;
using ChainSafe.Gaming.Evm.Providers;
using ChainSafe.Gaming.Evm.Contracts;
using System.Numerics;
using Scripts.EVM.Token;
using UnityEngine.UI;
using ChainSafe.Gaming.MultiCall;
using ChainSafe.Gaming.WalletConnect;
using ChainSafe.GamingSdk.Gelato;
using UnityEngine.Networking;
using Microsoft.IdentityModel.Tokens;
using ChainSafe.Gaming.Evm.Transactions;
using Nethereum.Hex.HexTypes;

public class Web3singleton : SingletonBehaviour<Web3singleton>
{
    public Web3 GlobalWeb3;
    [SerializeField] ErrorPopup errorPopup;

    public Sprite[] badgeUiSprites;
    [SerializeField] RawImage rawImage;

    public Contract erc1155Contract;
    public string contractAddress = "0x0c4f74549Ecf0564b01a6Ab9EcF43591bD12B731";
    public string abi = "[\r\n    {\r\n      \"inputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"constructor\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"approved\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"name\": \"ApprovalForAll\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"values\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"name\": \"TransferBatch\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"value\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"TransferSingle\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"string\",\r\n          \"name\": \"value\",\r\n          \"type\": \"string\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"URI\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"fallback\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"string\",\r\n          \"name\": \"newuri\",\r\n          \"type\": \"string\"\r\n        }\r\n      ],\r\n      \"name\": \"ChangeURI\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"_price\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"balanceOf\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address[]\",\r\n          \"name\": \"accounts\",\r\n          \"type\": \"address[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"name\": \"balanceOfBatch\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"isApprovedForAll\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"mintERC1155\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"owner\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"amounts\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"bytes\",\r\n          \"name\": \"data\",\r\n          \"type\": \"bytes\"\r\n        }\r\n      ],\r\n      \"name\": \"safeBatchTransferFrom\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"amount\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"internalType\": \"bytes\",\r\n          \"name\": \"data\",\r\n          \"type\": \"bytes\"\r\n        }\r\n      ],\r\n      \"name\": \"safeTransferFrom\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"approved\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"name\": \"setApprovalForAll\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"bytes4\",\r\n          \"name\": \"interfaceId\",\r\n          \"type\": \"bytes4\"\r\n        }\r\n      ],\r\n      \"name\": \"supportsInterface\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"uint8\",\r\n          \"name\": \"num\",\r\n          \"type\": \"uint8\"\r\n        }\r\n      ],\r\n      \"name\": \"transferBadge\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"uri\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"string\",\r\n          \"name\": \"\",\r\n          \"type\": \"string\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"withdrawEther\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"receive\"\r\n    }\r\n  ]";
    string uri = string.Empty;
    public bool[] hasBadge { get; private set; }

    public Action onMintERC1155;
    public Action onIsOwner;
    public Action<int> onLock;
    public Action<int> onUnlock;
    public Action<int> onAlready;
    public Action<Sprite, int> onSetImage;
    protected override void Awake()
    {
        base.Awake();
        badgeUiSprites = new Sprite[7];
        hasBadge = new bool[7];
    }
    public void ConfigureCommonServices(IWeb3ServiceCollection services)
    {
        services
            .UseUnityEnvironment()
            .UseMultiCall()
            .UseRpcProvider()
            .ConfigureRegisteredContracts(contracts =>
            {
                var abi = "[\r\n    {\r\n      \"inputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"constructor\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"approved\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"name\": \"ApprovalForAll\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"values\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"name\": \"TransferBatch\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"value\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"TransferSingle\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"anonymous\": false,\r\n      \"inputs\": [\r\n        {\r\n          \"indexed\": false,\r\n          \"internalType\": \"string\",\r\n          \"name\": \"value\",\r\n          \"type\": \"string\"\r\n        },\r\n        {\r\n          \"indexed\": true,\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"URI\",\r\n      \"type\": \"event\"\r\n    },\r\n    {\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"fallback\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"string\",\r\n          \"name\": \"newuri\",\r\n          \"type\": \"string\"\r\n        }\r\n      ],\r\n      \"name\": \"ChangeURI\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"_price\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"balanceOf\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address[]\",\r\n          \"name\": \"accounts\",\r\n          \"type\": \"address[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"name\": \"balanceOfBatch\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256[]\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"account\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"name\": \"isApprovedForAll\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"mintERC1155\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"owner\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"\",\r\n          \"type\": \"address\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"ids\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256[]\",\r\n          \"name\": \"amounts\",\r\n          \"type\": \"uint256[]\"\r\n        },\r\n        {\r\n          \"internalType\": \"bytes\",\r\n          \"name\": \"data\",\r\n          \"type\": \"bytes\"\r\n        }\r\n      ],\r\n      \"name\": \"safeBatchTransferFrom\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"from\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"to\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"id\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"amount\",\r\n          \"type\": \"uint256\"\r\n        },\r\n        {\r\n          \"internalType\": \"bytes\",\r\n          \"name\": \"data\",\r\n          \"type\": \"bytes\"\r\n        }\r\n      ],\r\n      \"name\": \"safeTransferFrom\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"address\",\r\n          \"name\": \"operator\",\r\n          \"type\": \"address\"\r\n        },\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"approved\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"name\": \"setApprovalForAll\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"bytes4\",\r\n          \"name\": \"interfaceId\",\r\n          \"type\": \"bytes4\"\r\n        }\r\n      ],\r\n      \"name\": \"supportsInterface\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"bool\",\r\n          \"name\": \"\",\r\n          \"type\": \"bool\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"uint8\",\r\n          \"name\": \"num\",\r\n          \"type\": \"uint8\"\r\n        }\r\n      ],\r\n      \"name\": \"transferBadge\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [\r\n        {\r\n          \"internalType\": \"uint256\",\r\n          \"name\": \"\",\r\n          \"type\": \"uint256\"\r\n        }\r\n      ],\r\n      \"name\": \"uri\",\r\n      \"outputs\": [\r\n        {\r\n          \"internalType\": \"string\",\r\n          \"name\": \"\",\r\n          \"type\": \"string\"\r\n        }\r\n      ],\r\n      \"stateMutability\": \"view\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"inputs\": [],\r\n      \"name\": \"withdrawEther\",\r\n      \"outputs\": [],\r\n      \"stateMutability\": \"nonpayable\",\r\n      \"type\": \"function\"\r\n    },\r\n    {\r\n      \"stateMutability\": \"payable\",\r\n      \"type\": \"receive\"\r\n    }\r\n  ]";
                var address = "0x0c4f74549Ecf0564b01a6Ab9EcF43591bD12B731";
                contracts.RegisterContract("ERC1155", abi, address);
            });
    }
    public async Task GetERC1155URI()
    {
        if (uri == string.Empty)
        {
            var originuri = await GetUri();
            uri = originuri.ToString().Substring(0, originuri.ToString().LastIndexOf("{id}.json"));
        }
    }
    public async void GetBadgeSprites()
    {
        await GetERC1155URI();
        for(int i = 0; i < 7; i++)
        {
            if (badgeUiSprites[i] == null)
            {
                rawImage.texture = await ImportBadgeTexture((i + 1).ToString());
                badgeUiSprites[i] = Sprite.Create((Texture2D)rawImage.texture, new Rect(0, 0, rawImage.texture.width, rawImage.texture.height), new UnityEngine.Vector2(0.5f, 0.5f));
            }
        }
    }
    public async void BadgeCheck()
    {
        await ProcessBalanceOfBatch();
    }

    private async Task ProcessBalanceOfBatch()
    {
        var contract = "0x0c4f74549Ecf0564b01a6Ab9EcF43591bD12B731";
        var account = PlayerPrefs.GetString("Address");
        string[] accounts = { account, account, account, account, account, account, account };
        string[] tokenIds = { "1", "2", "3", "4", "5", "6", "7" };

        List<BigInteger> batchBalances = await Erc1155.BalanceOfBatch(GlobalWeb3, contract, accounts, tokenIds);
        for (int i = 0; i < batchBalances.Count; i++)
        {
            if (batchBalances[i] != 0)
            {
                hasBadge[i] = true;
            }
            else
            {
                hasBadge[i] = false;
            }
            Debug.Log(batchBalances[i]);  
        }
        for (int i = 0;i < GameManager.Instance.stageCleared; i++)
        {
            if (batchBalances[i] == 0)
            {
                onUnlock(i);
            }
            else
            {
                onAlready(i);
            }
        }
        await SetBadgesImage();
    }
    public async Task SetBadgesImage()
    {
        for(int i=0;i < hasBadge.Length;i++)
        {
            if (hasBadge[i])
            {
                if (badgeUiSprites[i] != null)
                {
                    onSetImage(badgeUiSprites[i], i);
                }
                else
                {
                    await Task.Delay(1000);
                    i--;
                    continue;
                }
            }
        }
    }
    public void SetBadgeImage()
    {

    }
    public async void SendMintAllBadge()
    {
            await erc1155Contract.Send("mintERC1155");
    }
    public async void SendWithdrawEther()
    {
        try
        {
            await erc1155Contract.Send("withdrawEther");
        }
        catch(Web3Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public async void SendTransferBadge(int _index)
    {
        var transactionRequest = new TransactionRequest { Value = new HexBigInteger(10000000000000000) }; // 0.01eth
        await erc1155Contract.Send("transferBadge", new object[] { _index }, transactionRequest);
    }
    public async Task<string> GetOwner()
    {
        var response = await erc1155Contract.Call("owner");
        return response[0].ToString();
    }
    public async Task<string> GetUri()
    {
        //const string ipfsPath = "https://ipfs.io/ipfs/";
        var contractData = await erc1155Contract.Call("uri", new object[] { "0" });
        return contractData[0].ToString();
    }
    public async Task<Texture2D> ImportBadgeTexture(string tokenId)
    {
        // fetch uri from chain
        string _uri = uri + tokenId + ".json";
        // fetch json from uri
        UnityWebRequest webRequest = UnityWebRequest.Get(_uri);
        await webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            throw new System.Exception(webRequest.error);
        }
        // Deserialize the data into the response class
        Response data =
            JsonUtility.FromJson<Response>(System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        // parse json to get image uri
        string imageUri = data.image;
        Debug.Log("imageUri: " + imageUri);
        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
        Debug.Log("Revised URI: " + imageUri);
        // fetch image and display in game
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);
        await textureRequest.SendWebRequest();
        var response = ((DownloadHandlerTexture)textureRequest.downloadHandler).texture;
        return response;
    }
    public class Response
    {
        public string image;
    }
}