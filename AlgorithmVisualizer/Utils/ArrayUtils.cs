
namespace AlgorithmVisualizer.Utils
{
	static class ArrayUtils
	{
		// fill array with given value
		public static void Fill<T>(this T[] arr, T value)
		{
			for (int i = 0; i < arr.Length; i++) arr[i] = value;
		}
	}
}
