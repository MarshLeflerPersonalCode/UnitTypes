#pragma once
#include "KCDefines.h"
//copyright 2000-... Marsh Lefler

enum class ETARRAY_GROW_BY_TYPES
{
	DOUBLE,				//doubles everytime it reaches max
	PREDEFINED			//predefined grow by value
};
#ifdef USING_UE4



#else

template<class T>
class KCTArray
{
public:
	KCTArray(uint32 iGrowBy = 0)
	{
		if (iGrowBy > 0)
		{
			setGrowBy(iGrowBy);
		}
	}
	KCTArray(const KCTArray &mArray)
	{
		*this = mArray;
	}

	~KCTArray() { clean(); }

	//deletes all the contents by calling DELETE_SAFELY
	void						deleteContents()
	{
		for (uint32 iIndex = 0; iIndex < m_iCountOfItems; iIndex++)
		{
			DELETE_SAFELY(m_Memory[iIndex]);
		}
		clean();

	}


	//assuming a memory copy - reserves the space needed and returns the memory. If bResetMemory is true, it'll do a clean first. If it's false, it'll append the memory copy
	T *							_attemptMemoryCopy(uint32 iIntendedMemorySize, bool bResetMemory = true)
	{
		if (bResetMemory)
		{
			clean();
		}
		uint32 iCurrentMemoryLocation = m_iCountOfItems;	//this will always be zero if bResetMemory is true. Else we'll use it to pass that memory location out so it can be append
		reserve(m_iCountOfItems + iIntendedMemorySize);
		m_iCountOfItems += iIntendedMemorySize;
		return &m_Memory[iCurrentMemoryLocation];
	}
	//assuming a memory copy - reserves the space needed, and copys the memory. If bResetMemory is true, it'll do a clean first. If it's false, it'll append the memory copy
	bool						_attemptMemoryCopy(const T *pMemoryToCopy, uint32 iIntendedMemorySize, bool bResetMemory = true)
	{
		T *pMemory = _attemptMemoryCopy(iIntendedMemorySize, bResetMemory);
		if (pMemory)
		{
			for (uint32 iIndex = 0; iIndex < iIntendedMemorySize; iIndex++)
			{
				pMemory[iIndex] = pMemoryToCopy[iIndex];
			}
			return true;
		}
		return false;

	}
	//strings have 0 at the end and length() or size() doesn't include it. So this is to help fix bugs when we copy strings
	bool						_attemptCopyString(const KCString &strString, bool bResetMemory = true)
	{
		uint32 iIntendedMemorySize = (uint32)(strString.size() + 1);
		T *pMemory = _attemptMemoryCopy(iIntendedMemorySize, bResetMemory);
		if (pMemory)
		{
			for (uint32 iIndex = 0; iIndex < iIntendedMemorySize; iIndex++)
			{
				pMemory[iIndex] = (T)strString.c_str()[iIndex];
			}
			return true;
		}
		return false;

	}
	//sets the grow by number
	void						setGrowBy(uint32 iGrowBy) { m_iGrowBy = iGrowBy; m_eGrowType = ETARRAY_GROW_BY_TYPES::PREDEFINED; }
	//returns the grow by value
	uint32						getGrowBy() const { return m_iGrowBy; }
	//returns the memory
	const T *					GetData() const { return m_Memory; }
	//returns the memory
	uint32						getMemoryCount() const { return m_iMemorySize; }
	//returns the amount of memory being taken up - does a sizeof(T) * getMemoryCount()
	size_t						GetAllocatedSize() const { return sizeof(T) * (size_t)m_iMemorySize; }

	//help support UE4
	FORCEINLINE int32			Num() const { return m_iCountOfItems; }

	//resets the contents but doesn't clear the memory
	FORCEINLINE void			reset() { m_iCountOfItems = 0; }
	//resets the contents but doesn't clear the memory - upper case to support UE4
	FORCEINLINE void			Reset() { m_iCountOfItems = 0; }
	//removes all the memory and resets the count - NOTE this will not delete the objects in it.
	FORCEINLINE void			clean() { DELETE_ARRAY_SAFELY(m_Memory); m_iMemorySize = 0; m_iCountOfItems = 0; }
	//removes all the memory and resets the count - NOTE this will not delete the objects in it. - UE4 support
	FORCEINLINE void			Empty() { clean(); }
	FORCEINLINE KCTArray&		operator=(const KCTArray &mValues)
	{
		clean();
		Reserve(mValues.m_iMemorySize);
		m_eGrowType = mValues.m_eGrowType;
		m_iGrowBy = mValues.m_iGrowBy;
		for (uint32 iCount = 0; iCount < mValues.m_iCountOfItems; iCount++)
		{
			add(mValues[iCount]);
		}
		return *this;
	}

	//returns the item at the index
	FORCEINLINE T&				operator[](uint32 iIndex)
	{
		if (iIndex >= m_iCountOfItems)
		{
			//we should crash here!
			return m_Memory[0];
		}
		return m_Memory[iIndex];
	}
	//returns the item at the index
	FORCEINLINE const T &		operator[](uint32 iIndex) const
	{
		if (iIndex >= m_iCountOfItems)
		{
			//we should crash here!
			return m_Memory[0];
		}
		return m_Memory[iIndex];
	}

	//returns the item at the index
	FORCEINLINE T &				get(uint32 iIndex)
	{
		if (iIndex >= m_iCountOfItems)
		{
			//we should crash here!
			return m_Memory[0];
		}
		return m_Memory[iIndex];
	}

	//returns the item at the index
	FORCEINLINE const T &		get(uint32 iIndex) const
	{
		if (iIndex >= m_iCountOfItems)
		{
			//we should crash here!
			return m_Memory[0];
		}
		return m_Memory[iIndex];
	}

	FORCEINLINE bool			removeLast()
	{
		if (m_iCountOfItems > 0)
		{
			m_iCountOfItems--;
			return true;
		}
		return false;
	}

	//returns the last item in the list
	FORCEINLINE T &				last()
	{

		return m_Memory[m_iCountOfItems - 1];
	}
	//returns the last item in the list - ue4 helper
	FORCEINLINE T &				Last() { return last(); }



	//returns the last item in the list
	FORCEINLINE const T &		last() const
	{

		return m_Memory[m_iCountOfItems - 1];
	}
	//returns the last item in the list - ue4 helper
	FORCEINLINE const T &		Last() const { return last(); }

	//returns if the item is found in the array
	FORCEINLINE bool			Find(T item, uint32 &iIndexOfItem)
	{
		for (uint32 iIndex = 0; iIndex < m_iCountOfItems; iIndex++)
		{
			if (m_Memory[iIndex] == item)
			{
				iIndexOfItem = iIndex;
				return true;
			}
		}
		iIndexOfItem = INVALID;
		return false;
	}

	//adds a unique item
	FORCEINLINE uint32			AddUnique(T item)
	{
		uint32 iIndex = INVALID;
		if (Find(item, iIndex))
		{
			return iIndex;
		}
		return add(item);
	}

	//Adds an object
	FORCEINLINE uint32			add(T item)
	{
		if (m_iCountOfItems >= m_iMemorySize)
		{
			_expandMemory();
		}
		m_Memory[m_iCountOfItems] = item;
		m_iCountOfItems++;
		return m_iCountOfItems - 1;
	}
	//adds an object - UE4 helper 
	FORCEINLINE uint32			Add(T item) { return add(item); }
	//removes an item, replace the last item in the index with it and returns that index. returns INVALID if not found.
	FORCEINLINE uint32			RemoveSwap(T item)
	{
		for (uint32 iIndex = 0; iIndex < m_iCountOfItems; iIndex++)
		{
			if (m_Memory[iIndex] == item)
			{
				m_iCountOfItems--;
				m_Memory[iIndex] = m_Memory[m_iCountOfItems];
				return iIndex;
			}
		}
		return INVALID;
	}
	//removes an item at an index, replace the last item in the index with it
	FORCEINLINE bool			RemoveAtSwap(T item, uint32 iIndex)
	{
		if (iIndex < m_iCountOfItems)
		{
			m_iCountOfItems--;
			m_Memory[iIndex] = m_Memory[m_iCountOfItems];
			return true;
		}
		return false;
	}
	//reserves the number of slots
	void						reserve(uint32 iSize)
	{
		_expandMemoryTo(iSize);
	}
	//reserves the number of slots. UE4 helper function
	void						Reserve(uint32 iSize) { reserve(iSize); }

private:
	void						 _expandMemory()
	{
		switch (m_eGrowType)
		{
		default:
		case ETARRAY_GROW_BY_TYPES::DOUBLE:
			_expandMemoryTo((m_iMemorySize == 0) ? 2 : m_iMemorySize + m_iMemorySize);
			break;
		case ETARRAY_GROW_BY_TYPES::PREDEFINED:
			_expandMemoryTo(m_iMemorySize + ((m_iGrowBy <= 0) ? 10 : m_iGrowBy));
			break;
		}
	}
	//expands the memory to a given size
	void						 _expandMemoryTo(uint32 iNewMemory)
	{
		if (iNewMemory <= m_iMemorySize)
		{
			return;
		}

		T *mMemoryTemp = KC_NEW T[iNewMemory];

		for (uint32 t = 0; t < m_iMemorySize; t++)
		{
			mMemoryTemp[t] = m_Memory[t];
		}

		DELETE_ARRAY_SAFELY(m_Memory);
		m_Memory = mMemoryTemp;
		m_iMemorySize = iNewMemory;
	}



	T							*m_Memory = nullptr;
	uint32						m_iMemorySize = 0;
	ETARRAY_GROW_BY_TYPES		m_eGrowType = ETARRAY_GROW_BY_TYPES::DOUBLE;
	uint32						m_iGrowBy = 0;
	uint32						m_iCountOfItems = 0;
};
#define TArray KCTArray
#endif
