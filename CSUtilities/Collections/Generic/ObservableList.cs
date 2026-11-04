using System;
using System.Collections;
using System.Collections.Generic;

namespace CSUtilities.Collections.Generic;

/// <summary>
/// Represents a generic list that raises notifications before and after items are added or removed.
/// </summary>
/// <remarks>Implements IList<T> and preserves list semantics while providing cancellable pre-change events and
/// post-change events for add and remove operations, including replacement through the indexer.</remarks>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class ObservableList<T> : IList<T>
{
	/// <summary>
	/// Occurs when an item is added to the observable list.
	/// </summary>
	public event EventHandler<ObservableListEventArgs<T>> OnAdd;

	/// <summary>
	/// Occurs before an item is added to the observable list.
	/// </summary>
	public event EventHandler<ObservableListEventArgs<T>> OnBeforeAdd;

	/// <summary>
	/// Occurs before an item is removed from the collection.
	/// </summary>
	public event EventHandler<ObservableListEventArgs<T>> OnBeforeRemove;

	/// <summary>
	/// Occurs when an item is removed from the collection.
	/// </summary>
	public event EventHandler<ObservableListEventArgs<T>> OnRemove;

	/// <inheritdoc/>
	public int Count { get { return _list.Count; } }

	/// <inheritdoc/>
	public bool IsReadOnly { get { return ((ICollection<T>)_list).IsReadOnly; } }

	private readonly List<T> _list;

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
	/// </summary>
	public ObservableList()
	{
		_list = new List<T>();
	}

	/// <summary>
	/// Initializes a new instance of the <c>ObservableList{T}</c> class that contains elements copied from the specified
	/// collection.
	/// </summary>
	/// <remarks>Throws <see cref="System.ArgumentNullException"/> if <paramref name="collection"/> is <see
	/// langword="null"/>.</remarks>
	/// <param name="collection">The collection whose elements are copied to the new list.</param>
	public ObservableList(IEnumerable<T> collection)
	{
		_list = new List<T>(collection);
	}

	/// <inheritdoc/>
	public void Add(T item)
	{
		this.Insert(_list.Count, item);
	}

	/// <inheritdoc/>
	public void Clear()
	{
		for (int i = _list.Count - 1; i >= 0; i--)
		{
			this.RemoveAt(i);
		}
	}

	/// <inheritdoc/>
	public bool Contains(T item)
	{
		return _list.Contains(item);
	}

	/// <inheritdoc/>
	public void CopyTo(T[] array, int arrayIndex)
	{
		_list.CopyTo(array, arrayIndex);
	}

	/// <inheritdoc/>
	public IEnumerator<T> GetEnumerator()
	{
		return _list.GetEnumerator();
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	/// <inheritdoc/>
	public int IndexOf(T item)
	{
		return _list.IndexOf(item);
	}

	/// <inheritdoc/>
	public void Insert(int index, T item)
	{
		if (index < 0 || index > _list.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		if (!this.beforeAdd(item, index))
			return;

		_list.Insert(index, item);

		this.OnAdd?.Invoke(this, new ObservableListEventArgs<T>(item, index));
	}

	/// <inheritdoc/>
	public bool Remove(T item)
	{
		int index = _list.IndexOf(item);
		if (index < 0)
			return false;

		return this.removeAt(index);
	}

	/// <inheritdoc/>
	public void RemoveAt(int index)
	{
		if (index < 0 || index >= _list.Count)
			throw new ArgumentOutOfRangeException(nameof(index));

		this.removeAt(index);
	}

	private bool beforeAdd(T item, int index)
	{
		var args = new ObservableListEventArgs<T>(item, index);
		this.OnBeforeAdd?.Invoke(this, args);
		return !args.Cancel;
	}

	private bool beforeRemove(T item, int index)
	{
		var args = new ObservableListEventArgs<T>(item, index);
		this.OnBeforeRemove?.Invoke(this, args);
		return !args.Cancel;
	}

	private bool removeAt(int index)
	{
		T item = _list[index];
		if (!this.beforeRemove(item, index))
			return false;

		_list.RemoveAt(index);

		this.OnRemove?.Invoke(this, new ObservableListEventArgs<T>(item, index));
		return true;
	}

	/// <inheritdoc/>
	public T this[int index]
	{
		get { return _list[index]; }
		set
		{
			T old = _list[index];
			if (!this.beforeRemove(old, index) || !this.beforeAdd(value, index))
				return;

			_list[index] = value;

			this.OnRemove?.Invoke(this, new ObservableListEventArgs<T>(old, index));
			this.OnAdd?.Invoke(this, new ObservableListEventArgs<T>(value, index));
		}
	}
}