// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Engine/GameInstance.h"
#include "KCGameInstance.generated.h"

/**
 * 
 */
UCLASS()
class CORETEST_API UKCGameInstance : public UGameInstance
{
	GENERATED_BODY()
public:


protected:
	/** virtual function to allow custom GameInstances an opportunity to set up what it needs */
	virtual void						Init() override;
};
