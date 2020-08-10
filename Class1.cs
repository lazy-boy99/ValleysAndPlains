using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System;
using System.Linq;

class Exercise
{

	public class Holl {
		public int y;
		public int shift;
		public bool isActive;
		public List<int> neighbours;
		public Holl(int _y) {
			y = _y;
			isActive = true;
			shift = 0;
			neighbours = new List<int>();
		}
	}

	static void plainsAndValleys(List<List<int>> land)
	{

		var holls = new List<List<Holl>>(land[0].Count);
		for (int i = 0; i < land[0].Count; i++)
			holls.Add(new List<Holl>());
		for (int j = 0; j < land[0].Count; j++)
		{
			for (int i = 0; i < land.Count; i++)
			{
				if (land[i][j] == 0)
				{
					var holl = new Holl(i);
					holl.shift--;
					while (i < land.Count && land[i][j] != 1)
					{
						holl.shift++;
						i++;
					}
					holls[j].Add(holl);
				}

			}

		}

		for (int i = 1; i < holls.Count; i++)
		{
			for (int k = 0; k < holls[i].Count; k++)
			{
				int shift = 0;
				for (int j = shift; j < holls[i - 1].Count; j++)
				{
					var cur = holls[i][k];
					var left = holls[i - 1][j];
					if (cur.y > left.y && (left.y + left.shift) >= cur.y
						|| left.y > cur.y && (cur.y + cur.shift) >= left.y
						|| cur.y == left.y)
					{
						holls[i - 1][j].neighbours.Add(k);
						holls[i][k].isActive = false;
					}
					if (cur.y >= left.y + left.shift)
					{
						shift = j + 1;
					}
				}
			}
		}

		var result = new List<int>();
		for (int i = 0; i < holls.Count; i++)
		{
			for (int j = 0; j < holls[i].Count; j++)
			{
				if (holls[i][j].isActive)
				{
					int res = 0;
					f(ref res,holls, holls[i][j],i+1);
					if (res != 0)
						result.Add(res);
				}
			}
		}
		result.Sort();
		result.ToArray();
		result.ForEach(n=>Console.Write(n+" "));
	}
		static void f(ref int res,List<List<Holl>> holls,Holl holl, int index) {
			res += holl.shift + 1;
			foreach (var peak in holl.neighbours)
			{
				f(ref res,holls, holls[index][peak],index+1);
			}
		}

    static void Main(string[] args)
	{
		Console.WriteLine("press n");
		int n=Int32.Parse(Console.ReadLine());
		var list = new List<List<int>>();
		for (int i = 0; i < n; i++)
		{
			var ls = Console.ReadLine().Split(' ').ToList().Select(j=>int.Parse(j)).ToList();
			list.Add(ls);
		}
		plainsAndValleys(list);
	}

}