using CSUtilities.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSUtilities.Tests.Collections.Generic;

public class ObservableListTests
{
	[Fact]
	public void Constructor_Empty_CountIsZero()
	{
		var list = new ObservableList<int>();

		Assert.Empty(list);
		Assert.False(list.IsReadOnly);
	}

	[Fact]
	public void Constructor_Collection_CopiesItems()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });

		Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());
	}

	[Fact]
	public void Constructor_NullCollection_Throws()
	{
		Assert.Throws<ArgumentNullException>(() => new ObservableList<int>(null));
	}

	[Fact]
	public void Add_RaisesEventsInOrder()
	{
		var list = new ObservableList<string>();
		var calls = new List<string>();
		list.OnBeforeAdd += (s, e) => calls.Add($"before:{e.Item}:{e.Index}:{list.Count}");
		list.OnAdd += (s, e) => calls.Add($"after:{e.Item}:{e.Index}:{list.Count}");

		list.Add("a");

		Assert.Equal(new[] { "before:a:0:0", "after:a:0:1" }, calls);
		Assert.Equal("a", list[0]);
	}

	[Fact]
	public void Add_Cancelled_DoesNotAdd()
	{
		var list = new ObservableList<int>();
		bool added = false;
		list.OnBeforeAdd += (s, e) => e.Cancel = true;
		list.OnAdd += (s, e) => added = true;

		list.Add(1);

		Assert.Empty(list);
		Assert.False(added);
	}

	[Fact]
	public void Insert_InsertsAtIndex()
	{
		var list = new ObservableList<int>(new[] { 1, 3 });
		int index = -1;
		list.OnAdd += (s, e) => index = e.Index;

		list.Insert(1, 2);

		Assert.Equal(new[] { 1, 2, 3 }, list.ToArray());
		Assert.Equal(1, index);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Insert_InvalidIndex_Throws(int index)
	{
		var list = new ObservableList<int>(new[] { 1, 2 });

		Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(index, 0));
	}

	[Fact]
	public void Remove_ExistingItem_RaisesEvents()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		var calls = new List<string>();
		list.OnBeforeRemove += (s, e) => calls.Add($"before:{e.Item}:{e.Index}:{list.Count}");
		list.OnRemove += (s, e) => calls.Add($"after:{e.Item}:{e.Index}:{list.Count}");

		bool result = list.Remove(2);

		Assert.True(result);
		Assert.Equal(new[] { 1, 3 }, list.ToArray());
		Assert.Equal(new[] { "before:2:1:3", "after:2:1:2" }, calls);
	}

	[Fact]
	public void Remove_MissingItem_ReturnsFalse()
	{
		var list = new ObservableList<int>(new[] { 1 });
		bool raised = false;
		list.OnBeforeRemove += (s, e) => raised = true;

		Assert.False(list.Remove(5));
		Assert.False(raised);
	}

	[Fact]
	public void Remove_Cancelled_ReturnsFalse()
	{
		var list = new ObservableList<int>(new[] { 1 });
		bool removed = false;
		list.OnBeforeRemove += (s, e) => e.Cancel = true;
		list.OnRemove += (s, e) => removed = true;

		Assert.False(list.Remove(1));
		Assert.Single(list);
		Assert.False(removed);
	}

	[Fact]
	public void RemoveAt_RemovesItem()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		int item = 0;
		list.OnRemove += (s, e) => item = e.Item;

		list.RemoveAt(0);

		Assert.Equal(new[] { 2, 3 }, list.ToArray());
		Assert.Equal(1, item);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(2)]
	public void RemoveAt_InvalidIndex_Throws(int index)
	{
		var list = new ObservableList<int>(new[] { 1, 2 });

		Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(index));
	}

	[Fact]
	public void Clear_RemovesAllAndRaisesEvents()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		var removed = new List<int>();
		list.OnRemove += (s, e) => removed.Add(e.Item);

		list.Clear();

		Assert.Empty(list);
		Assert.Equal(new[] { 3, 2, 1 }, removed);
	}

	[Fact]
	public void Clear_PartiallyCancelled_KeepsCancelledItems()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		list.OnBeforeRemove += (s, e) => e.Cancel = e.Item == 2;

		list.Clear();

		Assert.Equal(new[] { 2 }, list.ToArray());
	}

	[Fact]
	public void Indexer_Set_RaisesRemoveAndAdd()
	{
		var list = new ObservableList<int>(new[] { 1, 2 });
		var calls = new List<string>();
		list.OnBeforeRemove += (s, e) => calls.Add($"beforeRemove:{e.Item}");
		list.OnBeforeAdd += (s, e) => calls.Add($"beforeAdd:{e.Item}");
		list.OnRemove += (s, e) => calls.Add($"remove:{e.Item}");
		list.OnAdd += (s, e) => calls.Add($"add:{e.Item}");

		list[1] = 5;

		Assert.Equal(5, list[1]);
		Assert.Equal(new[] { "beforeRemove:2", "beforeAdd:5", "remove:2", "add:5" }, calls);
	}

	[Fact]
	public void Indexer_Set_CancelledRemove_KeepsValue()
	{
		var list = new ObservableList<int>(new[] { 1 });
		bool beforeAdd = false;
		list.OnBeforeRemove += (s, e) => e.Cancel = true;
		list.OnBeforeAdd += (s, e) => beforeAdd = true;

		list[0] = 9;

		Assert.Equal(1, list[0]);
		Assert.False(beforeAdd);
	}

	[Fact]
	public void Indexer_Set_CancelledAdd_KeepsValue()
	{
		var list = new ObservableList<int>(new[] { 1 });
		list.OnBeforeAdd += (s, e) => e.Cancel = true;

		list[0] = 9;

		Assert.Equal(1, list[0]);
	}

	[Fact]
	public void Indexer_InvalidIndex_Throws()
	{
		var list = new ObservableList<int>();

		Assert.Throws<ArgumentOutOfRangeException>(() => list[0]);
		Assert.Throws<ArgumentOutOfRangeException>(() => list[0] = 1);
	}

	[Fact]
	public void Contains_IndexOf_CopyTo_Work()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		var array = new int[4];

		list.CopyTo(array, 1);

		Assert.True(list.Contains(2));
		Assert.False(list.Contains(4));
		Assert.Equal(2, list.IndexOf(3));
		Assert.Equal(-1, list.IndexOf(4));
		Assert.Equal(new[] { 0, 1, 2, 3 }, array);
	}

	[Fact]
	public void GetEnumerator_EnumeratesItems()
	{
		var list = new ObservableList<int>(new[] { 1, 2, 3 });
		var items = new List<int>();

		foreach (var item in list)
		{
			items.Add(item);
		}

		Assert.Equal(new[] { 1, 2, 3 }, items);
	}
}
