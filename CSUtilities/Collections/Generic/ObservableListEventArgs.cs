using System;

namespace CSUtilities.Collections.Generic;

/// <summary>
/// Provides data for events from an observable list, including the affected item, its index, and whether the operation
/// is canceled.
/// </summary>
/// <remarks>Set the cancellation flag in an event handler to request that the associated list operation not
/// proceed.</remarks>
/// <typeparam name="T">The type of item in the observable list.</typeparam>
public class ObservableListEventArgs<T> : EventArgs
{
	/// <summary>
	/// Gets the item.
	/// </summary>
	public T Item { get; }

	/// <summary>
	/// Gets the index.
	/// </summary>
	public int Index { get; }

	/// <summary>
	/// Gets or sets a value indicating whether to cancel the operation.
	/// </summary>
	public bool Cancel { get; set; }

	/// <summary>
	/// Initializes a new instance of the ObservableListEventArgs{T} class with the specified item and index.
	/// </summary>
	/// <param name="item">Item associated with the list event.</param>
	/// <param name="index">Zero-based index of the item in the list.</param>
	public ObservableListEventArgs(T item, int index)
	{
		this.Item = item;
		this.Index = index;
	}
}
