//------------------------------------------------------------------------------
// This code was generated by a tool.
//
//   Tool : MetaMask Unity SDK ABI Code Generator
//   Input filename:  Contracts.sol
//   Output filename: ContractsBacking.cs
//
// Changes to this file may cause incorrect behavior and will be lost when
// the code is regenerated.
// <auto-generated />
//------------------------------------------------------------------------------

#if UNITY_EDITOR || !ENABLE_MONO
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using evm.net;
using evm.net.Models;
using Nethereum.ABI;

namespace Contracts
{
	public class CustomContractBacking : Contract, CustomContract, IContract
	{ 
		public CustomContractBacking(IProvider provider, EvmAddress address, Type interfaceType) : base(provider, address, interfaceType)
		{
		}
		public Task<CustomContract> DeployNew()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<CustomContract>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "ChangeURI", View = false)]
		public Task<Transaction> ChangeURI(String newuri, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { newuri, options });
		}
		
		[EvmMethodInfo(Name = "_price", View = true)]
		public Task<BigInteger> _price()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "balanceOf", View = true)]
		public Task<BigInteger> BalanceOf([EvmParameterInfo(Type = "address", Name = "account")] string account, [EvmParameterInfo(Type = "uint256", Name = "id")] BigInteger id, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger>) InvokeMethod(method, new object[] { account, id, options });
		}
		
		[EvmMethodInfo(Name = "balanceOfBatch", View = true)]
		public Task<BigInteger[]> BalanceOfBatch([EvmParameterInfo(Type = "address", Name = "accounts")] string[] accounts, [EvmParameterInfo(Type = "uint256", Name = "ids")] BigInteger[] ids, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<BigInteger[]>) InvokeMethod(method, new object[] { accounts, ids, options });
		}
		
		[EvmMethodInfo(Name = "isApprovedForAll", View = true)]
		public Task<Boolean> IsApprovedForAll(String account, [EvmParameterInfo(Type = "address", Name = "operator")] EvmAddress @operator, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] { account, @operator, options });
		}
		
		[EvmMethodInfo(Name = "mintERC1155", View = false)]
		public Task<Transaction> MintERC1155()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "owner", View = true)]
		public Task<string> Owner()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<string>) InvokeMethod(method, new object[] {  });
		}
		
		[EvmMethodInfo(Name = "safeBatchTransferFrom", View = false)]
		public Task<Transaction> SafeBatchTransferFrom(EvmAddress from, EvmAddress to, BigInteger[] ids, BigInteger[] amounts, Byte[] data, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { from, to, ids, amounts, data, options });
		}
		
		[EvmMethodInfo(Name = "safeTransferFrom", View = false)]
		public Task<Transaction> SafeTransferFrom(EvmAddress from, EvmAddress to, BigInteger id, BigInteger amount, Byte[] data, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { from, to, id, amount, data, options });
		}
		
		[EvmMethodInfo(Name = "setApprovalForAll", View = false)]
		public Task<Transaction> SetApprovalForAll([EvmParameterInfo(Type = "address", Name = "operator")] EvmAddress @operator, Boolean approved, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { @operator, approved, options });
		}
		
		[EvmMethodInfo(Name = "supportsInterface", View = true)]
		public Task<Boolean> SupportsInterface([EvmParameterInfo(Type = "bytes4", Name = "interfaceId")] Byte[] interfaceId, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Boolean>) InvokeMethod(method, new object[] { interfaceId, options });
		}
		
		[EvmMethodInfo(Name = "transferBadge", View = false)]
		public Task<Transaction> TransferBadge([EvmParameterInfo(Type = "uint8", Name = "num")] UInt16 num, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] { num, options });
		}
		
		[EvmMethodInfo(Name = "uri", View = true)]
		public Task<String> Uri([EvmParameterInfo(Type = "uint256", Name = "")] BigInteger num, CallOptions options = default)
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<String>) InvokeMethod(method, new object[] { num, options });
		}
		
		[EvmMethodInfo(Name = "withdrawEther", View = false)]
		public Task<Transaction> WithdrawEther()
		{
			var method = System.Reflection.MethodBase.GetCurrentMethod();
			return (Task<Transaction>) InvokeMethod(method, new object[] {  });
		}
		
	}
}
#endif
