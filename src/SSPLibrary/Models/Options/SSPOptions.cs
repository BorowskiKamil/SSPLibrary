using System;
using SSPLibrary.Models;

namespace SSPLibrary
{
	public sealed class SSPOptions
	{
		private static readonly Lazy<SSPOptions> _instance = new Lazy<SSPOptions>(() => new SSPOptions());

		public static SSPOptions Instance 
		{
			get 
			{ 
				return _instance.Value; 
			} 
		}

		public PagingOptions PagingOptions { get; set; } = new PagingOptions();

		private SSPOptions() {}
	}
}