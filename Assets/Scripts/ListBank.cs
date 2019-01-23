/* Store the contents for ListBoxes to display.
 */
using UnityEngine;

public class ListBank : MonoBehaviour
{
	public static ListBank Instance;

	private int[] contents = {
		1, 2, 3, 4
	};

	void Awake()
	{
		Instance = this;
	}

	public string getListContent(int index)
	{
		return contents[index].ToString();
	}

	public int getListLength()
	{
		return contents.Length;
	}
}
