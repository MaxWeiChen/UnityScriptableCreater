using System.Reflection;
using System.Linq;

namespace MWUtil
{
	static public class EditorHelper
	{
		/// <summary>
		/// 找出Assembly裡class, 所以不能放在Plugin Folder內，要放在Assets裡
		/// </summary>
		static public System.Type[] FindDerivedTypes<T>(string filterNamespace) where T : class
		{
			var baseType = typeof(T);

			return Assembly.GetExecutingAssembly()
						   .GetTypes()
				           .Where(x => x.IsSubclassOf(baseType) &&
				                  (string.IsNullOrEmpty(filterNamespace) || x.Namespace == filterNamespace)
				                 )
						   .ToArray();
		}
	}
}