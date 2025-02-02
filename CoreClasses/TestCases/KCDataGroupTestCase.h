//copyright Marsh Lefler 2000-...
#pragma once
#include "Systems/DataGroup/KCDataGroup.h"
#include "Systems/DataGroup/FileTypes/KCDataGroupStringWriter.h"
#include "Systems/DataGroup/FileTypes/KCDataGroupStringParser.h"
#include "Systems/DataGroup/FileTypes/KCDataGroupBinaryWriter.h"
#include "Systems/DataGroup/FileTypes/KCDataGroupBinaryReader.h"
#include "KCIncludes.h"



static void				testDataGroupStringWriter(const WCHAR *strPath)
{
	KCDataGroup mDataGroup("Root");
	mDataGroup.setProperty("TEST_BOOL", true);
	mDataGroup.setProperty("TEST_INT", 13);
	mDataGroup.setProperty("TEST_FLOAT", 13.0f);
	mDataGroup.setProperty("TEST_INT64", 13);
	KCDataGroup &mChild1 = mDataGroup.getOrCreateChildGroup("CHILD1");
	mChild1.setProperty("CHILD1_TEST_BOOL", true);
	mChild1.setProperty("CHILD1_TEST_INT", 13);
	mChild1.setProperty("CHILD1_TEST_FLOAT", 13.0f);
	mChild1.setProperty("CHILD1_TEST_INT64", 13);
	KCDataGroup &mChild2 = mDataGroup.getOrCreateChildGroup("CHILD2");
	mChild2.setProperty("CHILD2_TEST_BOOL", true);
	mChild2.setProperty("CHILD2_TEST_INT", 13);
	mChild2.setProperty("CHILD2_TEST_FLOAT", 13.0f);
	mChild2.setProperty("CHILD2_TEST_INT64", 13);

	
	int32 iValue = mDataGroup.getProperty("TEST_INT", -1);
	const KCDataGroup *pChild1 = mDataGroup.getChildGroupWithInhertance("CHILD1");

	KCEnsureAlways( KCDataGroupStringWriter::writeDataGroupToFile(strPath, mDataGroup) );
	std::wstring strAddBinaryHack(strPath);
	strAddBinaryHack = strAddBinaryHack + L".bin";
	KCEnsureAlways(KCDataGroupBinaryWriter::writeDataGroupToFile(strAddBinaryHack.c_str(), mDataGroup));
}


static void				testDataGroupStringParser(const WCHAR *strPath)
{
	KCDataGroup mDataGroup;
	KCEnsureAlwaysReturn(KCDataGroupStringParser::parseDataGroupFromFile(strPath, mDataGroup));
	KCString strOutput = mDataGroup.getStringRepresentingDataGroup();
	//std::cout << strOutput << std::endl;
	int32 iValue = mDataGroup.getProperty("TEST_INT", -1);
	const KCDataGroup *pChild1 = mDataGroup.getChildGroupWithInhertance("CHILD1");
	float fChildValue = (pChild1)?pChild1->getProperty("CHILD1_TEST_FLOAT", -1.0f):-1.0f;
	bool bWOrked = ( iValue == 13 && pChild1 && fChildValue == 13.0f)?true:false;
	KCEnsureAlways(bWOrked);


	KCDataGroup mDataGroupBinary;
	std::wstring strAddBinaryHack(strPath);
	strAddBinaryHack = strAddBinaryHack + L".bin";
	KCEnsureAlwaysReturn(KCDataGroupBinaryReader::parseDataGroupFromFile(strAddBinaryHack.c_str(), mDataGroupBinary));
	KCString strOutputBinary = mDataGroup.getStringRepresentingDataGroup();
	//std::cout << strOutputBinary << std::endl;
	bool bBinaryWorked = (strOutputBinary == strOutput) ? true : false;
	if (bBinaryWorked)
	{
		std::cout << "Binary load worked. Same as text version." << std::endl;
	}
	KCEnsureAlways(bBinaryWorked);
}


static void				testDataGroupSavingAndLoad(const WCHAR *strPath)
{
	testDataGroupStringWriter(strPath);
	testDataGroupStringParser(strPath);
}