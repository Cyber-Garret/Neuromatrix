using Newtonsoft.Json;

using System.Collections.Generic;

namespace BungieAPI
{
	public class DictionaryComponentResponseOfInt64<T>
	{
		[JsonProperty(PropertyName = "data")]
		public IDictionary<long, T> Data { get; set; } = null;
		// privacy
	}
}